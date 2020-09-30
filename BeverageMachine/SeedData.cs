using BeverageMachine.Models;
using BeverageMachine.Repository;
using BeverageMachine.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BeverageMachine
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new ApplicationContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>()))
            {
                //var adminId = await EnsureUserCreated(_context, serviceProvider, "1234", "ekaterinatimofeeva20@gmail.com");
                var userId = await EnsureUserCreated(_context, serviceProvider, "1234", "lirik@gmail.com");
                //await EnsureRoleCreated(_context, serviceProvider, adminId, ApplicationContext.RoleName.Admin.ToString());
                await EnsureRoleCreated(_context, serviceProvider, userId, ApplicationContext.RoleName.User.ToString());
            }
        }

        public static async Task<string> EnsureUserCreated(ApplicationContext context, IServiceProvider provider, string password, string name)
        {
            var userManager = provider.GetService<UserManager<UserViewModel>>();
            UserViewModel user = await userManager.FindByNameAsync(name);
            if (user == null)
            {
                user = new UserViewModel { UserName = name, Email = name };
                await userManager.CreateAsync(user, password);
            }
            //context.SaveChanges();
            return user.Id;
        }

        public static async Task<IdentityResult> EnsureRoleCreated(ApplicationContext context, IServiceProvider provider, string userID, string role)
        {
            IdentityResult idenRes = null;
            var roleManager = provider.GetService<RoleManager<IdentityRole>>();
            var userManager = provider.GetService<UserManager<UserViewModel>>();
            var user = await userManager.FindByIdAsync(userID);
            if (!await roleManager.RoleExistsAsync(role))
            {
                idenRes = await roleManager.CreateAsync(new IdentityRole(role));
            }
            if (user != null)
            {
                idenRes = await userManager.AddToRoleAsync(user, role);
            }
            //context.SaveChanges();
            return idenRes;
        }

        public static async Task CreateDrink(this DrinkViewModel drink, ApplicationContext context)
        {
            context.Add(drink);
            await context.SaveChangesAsync();
        }
        public static decimal Summa(this List<PurchasedGoodViewModel> basket)
        {
            return basket.Sum(x => x.Drink.Amount * x.Quantity);
        }
        public static void AddDrink(this ApplicationContext context, DrinkViewModel drink, int quantity)
        {
            var goods = new List<PurchasedGoodViewModel>();
            PurchasedGoodViewModel line = goods
                .Where(x => x.Drink.Id == drink.Id)
                .FirstOrDefault();
            var p = context.ShoppingBaskets.ToList();
            var l = p.Where(x => x.UserId == "c95t").FirstOrDefault();
            if (line == null)
            {
                line = new PurchasedGoodViewModel
                {
                    Quantity = quantity,
                    Drink = drink,
                    ShoppingBasketViewModel = l
                };
                l.Goods.Add(line);
            }
            context.PurchasedGoods.AddRange(new List<PurchasedGoodViewModel>() { line});
            context.SaveChanges();
        }
        public static void AddOrder(this ApplicationContext context, ShoppingBasketViewModel basket, decimal sum)
        {
            OrderViewModel order = new OrderViewModel();
            order.Basket = basket;
            order.Amount = sum;
            context.Orders.Add(order);
            context.SaveChanges();
        }
        public static GoodsViewModel GetGoods(this ApplicationContext context, string userId)//To Do: когда пользователь не внес данные в корзину, выходит ошибка
        {
            GoodsViewModel model = new GoodsViewModel();
            model.Basket = context.ShoppingBaskets.Where(x => x.UserId == userId).FirstOrDefault();
            if (model.Basket != null)
            {
                var purchaseds = context.PurchasedGoods.Where(x => x.ShoppingBasketViewModelId == model.Basket.Id).ToList();
                foreach (var element in purchaseds)
                {
                    element.Drink = context.Drinks.Where(x => x.Id == element.DrinkId).FirstOrDefault();
                }
                model.PurchasedGoods = purchaseds;
            }
            return model;
        }
        public static dynamic GetChange(this OrderViewModel order, decimal clientMoney)
        {
            if (order.Amount < clientMoney || order.Amount == clientMoney)
            {
                decimal change = clientMoney - order.Amount;
                return change;
            }
            else 
            {
                string error = "Внесенная сумма меньше требуемой";
                return error;
            }
        }
    }
}


