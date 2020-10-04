using BeverageMachine.Models;
using BeverageMachine.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                var userId = await EnsureUserCreated(_context, serviceProvider, "1234", "llirik@gmail.com");
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
            await context.SaveChangesAsync();
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
            await context.SaveChangesAsync();
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
        public static void AddDrink(this ApplicationContext context, DrinkViewModel drink, int quantity, string userId)
        {
            List<ShoppingBasketViewModel> basketList = context.ShoppingBaskets.ToList();
            ShoppingBasketViewModel basket = basketList.Where(x => x.UserId == userId).FirstOrDefault();
            var purchased = basket != null ? context.PurchasedGoods.ToList().
                Where(x => x.ShoppingBasketViewModelId == basket.Id & x.DrinkId == drink.Id).FirstOrDefault() : null;

            if (basket == null)
            {
                basket = new ShoppingBasketViewModel()
                {
                    UserId = userId
                };
            }

            if (purchased != null)
            {
                purchased.Quantity += quantity;
            }

            if (purchased == null)
            {
                purchased = new PurchasedGoodViewModel { DrinkId = drink.Id, Quantity = quantity, ShoppingBasketViewModel = basket, ShoppingBasketViewModelId = basket.Id };
            }

            basket.Goods.Add(purchased);
            //context.PurchasedGoods.AddRange(new List<PurchasedGoodViewModel>() { purchased });
            context.SaveChanges();
        }
        public static void AddOrder(this ApplicationContext context, ShoppingBasketViewModel basket, decimal sum)
        {
            OrderViewModel order = context.Orders.Where(x => x.Basket.Id == basket.Id).FirstOrDefault();
            if (order == null)
            {
                order = new OrderViewModel() { Basket = basket, Amount = sum};
                context.Orders.Add(order);
                context.SaveChanges();
            }
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

        public static ShoppingBasketViewModel GetBasket(this ApplicationContext context,  string userName)
        {
            var id = context.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            var basket = context.ShoppingBaskets.Where(x => x.UserId == id).FirstOrDefault();
            return basket;
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


