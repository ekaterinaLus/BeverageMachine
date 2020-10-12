using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeverageMachine.Models;
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
        public static bool NeedUpdateDb => true;
        public static async Task Main(string[] args)
        {
            var hostWeb = CreateWebHostBuilder(args).Build();
            if (NeedUpdateDb)
            {
                using (var scope = hostWeb.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        await SeedData.Initialize(services);
                    }
                    catch (Exception ex)
                    {
                        var logger = services.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Error");
                        throw;
                    }
                }
            }
            hostWeb.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
               .UseStartup<Startup>();
    }
}
