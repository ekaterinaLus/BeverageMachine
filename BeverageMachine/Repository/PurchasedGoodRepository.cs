using BeverageMachine.Entity;
using BeverageMachine.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeverageMachine.Repository
{
    public class PurchasedGoodRepository : IPurchasedGoodRepository
    {
        protected ApplicationContext DbContext { get; set; }
        public PurchasedGoodRepository(ApplicationContext context)
        {
            DbContext = context;
        }
        public void Create(PurchasedGood element)
        {
            DbContext.PurchasedGoods.Add(element);
        }

        public void Delete(PurchasedGood element)
        {
            DbContext.PurchasedGoods.Remove(element);
        }

        public PurchasedGood Get(int id)
        {
            return DbContext.PurchasedGoods.Find(id);
        }

        public List<PurchasedGood> GetAll()
        {
            return DbContext.PurchasedGoods.ToList();
        }
    }
}
