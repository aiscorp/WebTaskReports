using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

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
            // Net core 3.0 �� ������� �������� services.AddMvc(), 
            // ������ ����������� ���� ������������ � Razor
            // https://docs.microsoft.com/ru-ru/aspnet/core/migration/22-to-30?view=aspnetcore-2.2&tabs=visual-studio
            //
            services.AddControllersWithViews();

            services.AddRazorPages();

            // ������� ��� ���?
            // ��������� ���������� �� ����� ���������� � ������� ASP.NET Core 3,0
            // ����� Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            // services.AddControllersWithViews().AddRazorRuntimeCompilation();    

            // �������� ���� ������
            //        services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection")));

            //        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //            .AddEntityFrameworkStores<ApplicationDbContext>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // ������ ���� ������
                // app.UseDatabaseErrorPage();

                // ������������� ��������
                // ???
            }
            else
            {
                // ��� ������ Production
                app.UseExceptionHandler("/Home/Error");

                // HTTP Strict Transport Security Protocol (HSTS)
                // https://metanit.com/sharp/aspnet5/18.6.php
                // https://aka.ms/aspnetcore-hsts 
                // app.UseHsts();
            }

            // ���� ���� SSL ��� Https - ��������
            // ���������� �����������
            // https://letsencrypt.org/ru/
            // app.UseHttpsRedirection();

            app.UseStaticFiles();


            app.UseRouting();

            // ����� ��������� �������
            //app.UseAuthentication();
            //app.UseAuthorization();


            // Core 2.2 = app.UseMvc(routes =>
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
