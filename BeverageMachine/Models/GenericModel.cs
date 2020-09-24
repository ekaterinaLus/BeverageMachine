using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public abstract class GenericModel<TRepository, TViewModel>
        where TRepository: class
        where TViewModel: class
    {
        public TRepository Repository { get; set; }
    }
}
