using BeverageMachine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Repository
{
    interface IOrderRepository: IGenericRepository<OrderViewModel>
    {
    }
}
