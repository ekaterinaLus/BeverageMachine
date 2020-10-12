using System.Collections.Generic;
using System.Linq;

namespace BeverageMachine.Entity
{
    public class ShoppingBasket
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<PurchasedGood> Goods { get; set; }

        public ShoppingBasket()
        {
            Goods = new List<PurchasedGood>();
        }
        public ShoppingBasket(List<PurchasedGood> _goods)
        {
            Goods = _goods;
        }
        //public ShoppingBasketViewModel AddItem(ShoppingBasketsRepository shoppingRepository, DrinkViewModel drink, int quantity)
        //{
        //    var goods = new List<PurchasedGoodViewModel>();
        //    PurchasedGoodViewModel line = goods
        //        .Where(x => x.Drink.Id == drink.Id)
        //        .FirstOrDefault();
        //    if (line == null)
        //    {
        //        line = new PurchasedGoodViewModel
        //        {
        //            Quantity = quantity,
        //            Drink = drink
        //        };
        //        goods.Add(line);
        //    }
        //    var basket = shoppingRepository.Get(9);
        //    basket.Goods.Add(line);
        //    return basket;
        //}
        public ShoppingBasket AddItem(Drink drink, int quantity)
        {
            var el = new ShoppingBasket();
            var goods = new List<PurchasedGood>();
            PurchasedGood line = goods
                .Where(x => x.Drink.Id == drink.Id)
                .FirstOrDefault();
            if (line == null)
            {
                line = new PurchasedGood
                {
                    Quantity = quantity,
                    Drink = drink
                };
                goods.Add(line);
                el.Goods.Add(line);
            }
            //var basket = shoppingRepository.Get(9);
            //basket.Goods.Add(line);
            return el;
        }

        public void Remove(Drink drink)
        {
            Goods.RemoveAll(x => x.Drink.Id == drink.Id);
        }
    }
}
