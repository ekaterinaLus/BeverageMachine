using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public class Wallet
    {
        public int IdGood { get; set; }
        public decimal Cost { get; set; }
        public decimal Pay { get; set; }

        public Wallet()
        {            
        }
        public Wallet(decimal _cost, decimal _pay)
        {
            Cost = _cost;
            Pay = _pay;
        }
    }
}
