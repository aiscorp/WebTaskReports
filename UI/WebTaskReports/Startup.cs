using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.IO;
using WebTaskReports.DAL.Context;
using WebTaskReports.Domain.Entities.Identity;
using WebTaskReports.Interfaces.Services;
using WebTaskReports.Services.Product.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebTaskReports
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Полезно или нет?
            // Включение компиляции во время выполнения в проекте ASP.NET Core 3,0
            // Пакет Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            // services.AddControllersWithViews().AddRazorRuntimeCompilation();    

            #region Контекст базы данных
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            // зачем? почитать
            //services.AddTransient<WebStoreContextInitializer>();
            #endregion

            #region Служба Identity <юзеры и роли>
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
            //.AddDefaultUI()
            .AddDefaultTokenProviders(); //enable two-factor authentication

            services.Configure<IdentityOptions>(
                opt =>
                {
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredUniqueChars = 3;

                    opt.Lockout.AllowedForNewUsers = true;
                    opt.Lockout.MaxFailedAccessAttempts = 10;
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                    //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABC123";
                    opt.User.RequireUniqueEmail = false; // Грабли - на этапе отладки при попытке регистрации двух пользователей без email
                });
            #endregion

            //services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailService, EmailService>();

            #region Куки (не активно и не настроено)
            // https://docs.microsoft.com/ru-ru/ASPNET/Core/fundamentals/app-state?view=aspnetcore-3.1

            services.AddDistributedMemoryCache();

            //services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.Cookie.Name = "WebTaskReports";
            //    opt.Cookie.HttpOnly = true;
            //    opt.Cookie.Expiration = TimeSpan.FromMinutes(5);//TimeSpan.FromDays(150); 
            //    //opt.LoginPath = "/Account/Login";
            //    //opt.LogoutPath = "/Account/Logout";
            //    //opt.AccessDeniedPath = "/Account/AccessDenided";
            //    opt.SlidingExpiration = true;
            //});

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Identity/Account/Login");
            });

            services.ConfigureExternalCookie(opt =>
            {
                opt.LoginPath = new PathString("/Identity/Account/Login");                
            });

            services.AddSession(opt =>
            {
                opt.Cookie.Name = "WebTaskReports.Sessions";
                opt.IdleTimeout = TimeSpan.FromMinutes(1);
                opt.Cookie.IsEssential = true;                
            });
            #endregion

            #region Внешняя авторизация через Google
            services.AddAuthentication()
                .AddOpenIdConnect("Google", "Google",
                    o =>
                    {
                        //IConfigurationSection googleAuthNSection =
                        //    Configuration.GetSection("Authentication:Google");
                        o.ClientId = Configuration["Authentication:Google:ClientId"];
                        o.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                        o.Authority = "https://accounts.google.com";
                        o.ResponseType = OpenIdConnectResponseType.Code;
                        o.CallbackPath = "/signin-google";
                        //o.RemoteSignOutPath = "https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://localhost:5001/";
                        
                    });
            // .AddIdentityServerJwt();

            //Внешняя авторизация
            // https://habr.com/ru/post/461433/
            // https://developers.google.com/identity/sign-in/web/sign-in#before_you_begin
            //https://localhost:5001/
            //{ "web":{ 
            //  "client_id":"1045950316770-rm034ka9i6cs25fiursdhomnfg6kcinl.apps.googleusercontent.com","project_id":"webtaskreports-1582143905079",
            //  "auth_uri":"https://accounts.google.com/o/oauth2/auth",
            //  "token_uri":"https://oauth2.googleapis.com/token",
            //  "auth_provider_x509_cert_url":"https://www.googleapis.com/oauth2/v1/certs",
            //  "client_secret":"htNHxae7ewLoTH9-SwpPgpfs",
            //  "redirect_uris":["https://localhost:5001/signin-google"]
            //    }}
            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                // Автоматизация миграций
            }
            else
            {
                // Для стадии Production
                app.UseExceptionHandler("/Home/Error");

                // HTTP Strict Transport Security Protocol (HSTS)
                // https://metanit.com/sharp/aspnet5/18.6.php
                // https://aka.ms/aspnetcore-hsts 
                // app.UseHsts();
            }

            // Если есть SSL для Https - включить
            // Бесплатные сертификаты
            // https://letsencrypt.org/ru/
            // app.UseHttpsRedirection();

            app.UseStaticFiles();

            // для куки            
            //app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
