using BeverageMachine.Entity;
using BeverageMachine.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeverageMachine.Repository
{
    public class ShoppingBasketsRepository: IShoppingBasketsRepository
    {
        protected ApplicationContext DbContext { get; set; }
        public ShoppingBasketsRepository(ApplicationContext context)
        {
            DbContext = context;
        }
        public void Create(ShoppingBasket element)
        {
            DbContext.ShoppingBaskets.Add(element);
        }

        public void Delete(ShoppingBasket element)
  
        {
            DbContext.ShoppingBaskets.Remove(element);
        }

        public ShoppingBasket Get(int id)
        {
            var t = DbContext.ShoppingBaskets.Find(id);
            return t;
        }

        public List<ShoppingBasket> GetAll()
        {
            return DbContext.ShoppingBaskets.ToList();
        }
    }
}
