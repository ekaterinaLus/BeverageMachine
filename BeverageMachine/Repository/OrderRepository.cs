using BeverageMachine.Entity;
using BeverageMachine.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeverageMachine.Repository
{
    public class OrderRepository : IOrderRepository
    {
        protected ApplicationContext DbContext { get; set; }
        public OrderRepository(ApplicationContext context)
        {
            DbContext = context;
        }
        public void Create(Order element)
        {
            DbContext.Orders.Add(element);
        }

        public void Delete(Order element)
        {
            DbContext.Orders.Remove(element);
        }

        public Order Get(int id)
        {
            return DbContext.Orders.Find(id);
        }

        public List<Order> GetAll()
        {
            return DbContext.Orders.ToList();
        }
    }
}
