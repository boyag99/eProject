using System;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eProject.App.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseMySql(
                    configuration["ConnectionSetting:MySQLSettings:ConnectionStrings"]));
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.LoginPath = "/Admin/Account/Index";
            //        options.LogoutPath = "/Admin/Account/Logout";
            //        options.AccessDeniedPath = "/Admin/Account/AccessDenied";
            //    });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Login");
                options.AccessDeniedPath = new PathString("/");
                options.LogoutPath = new PathString("/Logout");
            });
        }
    }
}
