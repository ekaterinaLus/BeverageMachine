using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.ViewModel
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public ShoppingBasketViewModel Basket { get; set; }
        public decimal Amount { get; set; }
    }
}
