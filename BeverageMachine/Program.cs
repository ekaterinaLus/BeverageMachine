using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BeverageMachine
{
    public class Program
    {
        public static bool NeedUpdateDb { get => true; }
        public static async Task Main(string[] args)
        {
            var hostWeb = CreateWebHostBuilder(args).Build();
            if (NeedUpdateDb)
            {
                using (var scope = hostWeb.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    //var context = services.GetRequiredService<BusinessUniversityContext>();
                    //context.Database.Migrate();

                    try
                    {
                        await SeedData.Initialize(services);
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Error");
                        throw ex;
                    }
                }
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>();
    }
}
