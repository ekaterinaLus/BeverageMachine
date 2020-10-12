using BeverageMachine.Entity;
using BeverageMachine.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeverageMachine
{
    //creating users and roles when starting the program, adding entities to the database
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>()))
            {
                var adminId = await EnsureUserCreated(context, serviceProvider, "1234", "ekaterinatimofeeva20@gmail.com");
                var userId = await EnsureUserCreated(context, serviceProvider, "1234", "llirik@gmail.com");
                await EnsureRoleCreated(context, serviceProvider, adminId, nameof(ApplicationContext.RoleName.Admin));
                await EnsureRoleCreated(context, serviceProvider, userId, nameof(ApplicationContext.RoleName.User));
                await AddDrinks(context);
            }
        }

        public static async Task<string> EnsureUserCreated(ApplicationContext context, IServiceProvider provider, string password, string name)
        {
            var userManager = provider.GetService<UserManager<User>>();
            User user = await userManager.FindByNameAsync(name);
            if (user == null)
            {
                user = new User { UserName = name, Email = name };
                await userManager.CreateAsync(user, password);
                await context.SaveChangesAsync();
            }

            return user.Id;
        }

        public static async Task<IdentityResult> EnsureRoleCreated(ApplicationContext context, IServiceProvider provider, string userID, string role)
        {
            IdentityResult idenRes = null;
            var roleManager = provider.GetService<RoleManager<IdentityRole>>();
            var userManager = provider.GetService<UserManager<User>>();
            var user = await userManager.FindByIdAsync(userID);
            if (!await roleManager.RoleExistsAsync(role))
            {
                idenRes = await roleManager.CreateAsync(new IdentityRole(role));
            }
            if (user != null)
            {
                idenRes = await userManager.AddToRoleAsync(user, role);
            }
            await context.SaveChangesAsync();
            return idenRes;
        }

        private static async Task AddDrinks(ApplicationContext context)
        {
            List<Drink> items = new List<Drink>
            {
                new Drink { Name = "Mojito peach", Amount = 12, Quantity = 10},
                new Drink { Name = "Mojito strawberry", Amount = 12, Quantity = 10},
                new Drink { Name = "Watermelon tea", Amount = 24, Quantity = 10},
                new Drink { Name = "Lemonade with ginger", Amount = 13, Quantity = 6},
                new Drink { Name = "Lemonade surprise", Amount = 11, Quantity = 8},
                new Drink { Name = "Harry Potter lemonade", Amount = 25, Quantity = 3}
            };

            await context.Drinks. AddUniqueElementsAsync(items);
            await context.SaveChangesAsync();
        }  
    }
}


