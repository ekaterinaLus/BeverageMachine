using BeverageMachine.Models;
using BeverageMachine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Repository
{
    public class OrderRepository : IOrderRepository
    {
        protected ApplicationContext DbContext { get; set; }
        public OrderRepository(ApplicationContext context)
        {
            DbContext = context;
        }
        public void Create(OrderViewModel element)
        {
            DbContext.Orders.Add(element);
        }

        public void Delete(OrderViewModel element)
        {
            DbContext.Orders.Remove(element);
        }

        public OrderViewModel Get(int id)
        {
            return DbContext.Orders.Find(id);
        }

        public List<OrderViewModel> GetAll()
        {
            return DbContext.Orders.ToList();
        }
    }
}
