using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.App.Data.SeedData;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;

                    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                    context.Database.EnsureDeleted();

                    if (context.Database.EnsureCreated())
                    {
                        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

                        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                        IdentityDataInitializer.SeedData(userManager, roleManager);
                        DataInitializer.SeedData(context, userManager);
                    }
                }
            }
            catch (Exception /**ex**/)
            {

            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
