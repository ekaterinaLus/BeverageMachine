using BeverageMachine.Models;
using BeverageMachine.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.ViewModel
{
    public class ShoppingBasketViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<PurchasedGoodViewModel> Goods { get; set; }

        public ShoppingBasketViewModel()
        {
            Goods = new List<PurchasedGoodViewModel>();
        }
        public ShoppingBasketViewModel(List<PurchasedGoodViewModel> _goods)
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
        public ShoppingBasketViewModel AddItem(DrinkViewModel drink, int quantity)
        {
            var el = new ShoppingBasketViewModel();
            var goods = new List<PurchasedGoodViewModel>();
            PurchasedGoodViewModel line = goods
                .Where(x => x.Drink.Id == drink.Id)
                .FirstOrDefault();
            if (line == null)
            {
                line = new PurchasedGoodViewModel
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

        public void Remove(DrinkViewModel drink)
        {
            //HttpSessionState
            Goods.RemoveAll(x => x.Drink.Id == drink.Id);
        }
    }
}
