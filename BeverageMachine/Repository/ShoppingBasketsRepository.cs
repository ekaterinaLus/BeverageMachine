 using BeverageMachine.Migrations;
using BeverageMachine.Models;
using BeverageMachine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Repository
{
    public class ShoppingBasketsRepository: IShoppingBasketsRepository
    {
        protected ApplicationContext DbContext { get; set; }
        public ShoppingBasketsRepository(ApplicationContext context)
        {
            DbContext = context;
        }
        public void Create(ShoppingBasketViewModel element)
        {
            DbContext.ShoppingBaskets.Add(element);
        }

        public void Delete(ShoppingBasketViewModel element)
  
        {
            DbContext.ShoppingBaskets.Remove(element);
        }

        public ShoppingBasketViewModel Get(int id)
        {
            var t = DbContext.ShoppingBaskets.Find(id);
            return t;
        }

        public List<ShoppingBasketViewModel> GetAll()
        {
            return DbContext.ShoppingBaskets.ToList();
        }
    }
}
