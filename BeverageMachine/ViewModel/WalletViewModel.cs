using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public class WalletViewModel
    {
        public int IdGood { get; set; }
        public decimal Cost { get; set; }
        public decimal Pay { get; set; }

        public WalletViewModel()
        {            
        }
        public WalletViewModel(decimal _cost, decimal _pay)
        {
            Cost = _cost;
            Pay = _pay;
        }
    }
}
