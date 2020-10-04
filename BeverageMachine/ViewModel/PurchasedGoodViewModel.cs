using BeverageMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.ViewModel
{
    public class PurchasedGoodViewModel
    { 
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int DrinkId { get; set; }
        public DrinkViewModel Drink { get; set; }
        public int ShoppingBasketViewModelId { get; set; }
        public ShoppingBasketViewModel ShoppingBasketViewModel { get; set; }
    }
}
