using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public class DrinkModel
    {
            private double value = 0.5;
            //public double Value { get { return value; } }
            //public List<double> value = new List<double>() { 0.5, 1.0, 1.5 };
            public int Id { get; set; }
            public string Name { get; set; }
            //public double Value { get; set; }
            public decimal Amount { get; set; }
    }
}
