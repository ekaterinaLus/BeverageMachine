using BeverageMachine.Entity;
using BeverageMachine.Models;
using System.Collections.Generic;
using System.Linq;

namespace BeverageMachine.Repository
{
    public class DrinkRepository : IDrinkRepository
    {
        protected ApplicationContext DbContext { get; set; }
        public DrinkRepository(ApplicationContext context)
        {
            DbContext = context;
        }
        public void Create(Drink element)
        {
            DbContext.Drinks.Add(element);
        }
        public void Delete(Drink element)
        {
            DbContext.Drinks.Remove(element);
        }

        public Drink Get(int id)
        {
            return DbContext.Drinks.Find(id);
        }
        public List<Drink> GetAll()
        {
            return DbContext.Drinks.ToList();
        }
    }
}
