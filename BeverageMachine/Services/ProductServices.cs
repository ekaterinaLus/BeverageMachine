using BeverageMachine.Entity;
using BeverageMachine.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Services
{
    public static class ProductServices
    {
        public static async Task CreateDrink(this Drink drink, ApplicationContext context)
        {
            context.Add(drink);
            await context.SaveChangesAsync();
        }

        public static void AddDrink(this ApplicationContext context, Drink drink, int quantity, string userId)
        {
            ShoppingBasket basket = new ShoppingBasket();
            PurchasedGood purchase = new PurchasedGood();

            if (context.ShoppingBaskets.Count() != 0)
            {
                List<ShoppingBasket> basketList = context.ShoppingBaskets.ToList();
                basket = basketList.Where(x => x.UserId == userId).FirstOrDefault();
                purchase = basket != null ? context.PurchasedGoods.ToList().
                Where(x => x.ShoppingBasketViewModelId == basket.Id & x.DrinkId == drink.Id).FirstOrDefault() : null;
            }

            if (basket == null)
            {
                basket = new ShoppingBasket()
                {
                    UserId = userId
                };
            }

            if (purchase != null)
            {
                purchase.Quantity += quantity;
            }

            if (purchase == null)
            {
                purchase = new PurchasedGood { DrinkId = drink.Id, Quantity = quantity, ShoppingBasketViewModel = basket, ShoppingBasketViewModelId = basket.Id };
            }

            basket.Goods.Add(purchase);
            context.SaveChangesAsync();
        }

        public static void AddOrder(this ApplicationContext context, ShoppingBasket basket, decimal sum)
        {
            Order order = context.Orders.Where(x => x.Basket.Id == basket.Id).FirstOrDefault();
            if (order == null)
            {
                order = new Order() { Basket = basket, Amount = sum };
                context.Orders.Add(order);
                context.SaveChangesAsync();
            }
        }
        public static Goods GetGoods(this ApplicationContext context, string userId)//To Do: когда пользователь не внес данные в корзину, выходит ошибка
        {
            Goods model = new Goods();
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

        public static ShoppingBasket GetBasket(this ApplicationContext context, string userName)
        {
            var id = context.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            var basket = context.ShoppingBaskets.Where(x => x.UserId == id).FirstOrDefault();
            return basket;
        }
        public static dynamic GetChange(this Order order, decimal clientMoney)
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

        public static decimal GetTotal(this List<PurchasedGood> basket)
        {
            return basket.Sum(x => x.Drink.Amount * x.Quantity);
        }
    }
}
