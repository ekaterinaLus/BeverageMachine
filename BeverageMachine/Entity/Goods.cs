using System.Collections.Generic;

namespace BeverageMachine.Entity
{
    public class Goods
    {
        public IEnumerable<PurchasedGood> PurchasedGoods { get; set; }
        public ShoppingBasket Basket { get; set; }
    }
}
