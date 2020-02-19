using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using WebTaskReports.DAL.Context;
using WebTaskReports.Domain.Entities.Identity;

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

            // Контекст базы данных
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            // зачем? почитать
            //services.AddTransient<WebStoreContextInitializer>();

            // Службы юзеров и ролей
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

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

            // для куки
            // https://docs.microsoft.com/ru-ru/ASPNET/Core/fundamentals/app-state?view=aspnetcore-3.1
            //services.ConfigureApplicationCookie(opt =>
            //{
            //    opt.Cookie.Name = "WebStore-Identity";
            //    opt.Cookie.HttpOnly = true;
            //    opt.Cookie.Expiration = TimeSpan.FromDays(150);

            //    opt.LoginPath = "/Account/Login";
            //    opt.LogoutPath = "/Account/Logout";
            //    opt.AccessDeniedPath = "/Account/AccessDenided";

            //    opt.SlidingExpiration = true;
            //});
            //services.AddSession();



            // Net core 3.0 не требует указания services.AddMvc(), 
            // взамен указывается типы контроллеров и Razor
            // https://docs.microsoft.com/ru-ru/aspnet/core/migration/22-to-30?view=aspnetcore-2.2&tabs=visual-studio
            //
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
            //app.UseSession();

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
