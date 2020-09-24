using BeverageMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.ViewModel
{
    public class GoodsViewModel
    {
        public IEnumerable<PurchasedGoodViewModel> PurchasedGoods { get; set; }
        public ShoppingBasketViewModel Basket { get; set; }
    }
}
