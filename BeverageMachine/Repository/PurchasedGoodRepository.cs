using BeverageMachine.Models;
using BeverageMachine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Repository
{
    public class PurchasedGoodRepository : IPurchasedGoodRepository
    {
        protected ApplicationContext DbContext { get; set; }
        public PurchasedGoodRepository(ApplicationContext context)
        {
            DbContext = context;
        }
        public void Create(PurchasedGoodViewModel element)
        {
            DbContext.PurchasedGoods.Add(element);
        }

        public void Delete(PurchasedGoodViewModel element)
        {
            DbContext.PurchasedGoods.Remove(element);
        }

        public PurchasedGoodViewModel Get(int id)
        {
            return DbContext.PurchasedGoods.Find(id);
        }

        public List<PurchasedGoodViewModel> GetAll()
        {
            return DbContext.PurchasedGoods.ToList();
        }
    }
}
