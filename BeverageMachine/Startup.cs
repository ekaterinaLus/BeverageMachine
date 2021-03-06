using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeverageMachine.Models;
using BeverageMachine.Repository;
using BeverageMachine.Services;
using Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeverageMachine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                  migration => migration.MigrationsAssembly(Configuration.GetValue<string>("MigrationsAssembly"))));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();

            //services.AddScoped<ApplicationContext>();
           
            services.AddScoped<IDrinkRepository, DrinkRepository>();

            services.AddScoped<IShoppingBasketsRepository, ShoppingBasketsRepository>();

            services.AddSession();

            services.AddTransient<IEmailService, EmailService>();

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Events.OnRedirectToLogin = (context) =>
            //    {
            //        context.Response.Clear();
            //        context.Response.StatusCode = 401;//StatusCodes.Status401Unauthorized;
            //        return Task.CompletedTask;
            //    };

            //    options.Events.OnRedirectToAccessDenied = (context) =>
            //    {
            //        context.Response.Clear();
            //        context.Response.StatusCode = 401;//StatusCodes.Status403Forbidden;
            //        return Task.CompletedTask;
            //    };
            //});

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}