using BeverageMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Repository
{
    public class DrinkRepository : IDrinkRepository
    {
        protected ApplicationContext DbContext { get; set; }
        public DrinkRepository(ApplicationContext context)
        {
            DbContext = context;
        }
        public void Create(DrinkViewModel element)
        {
            DbContext.Drinks.Add(element);
        }
        public void Delete(DrinkViewModel element)
        {
            DbContext.Drinks.Remove(element);
        }
        public DrinkViewModel Get(int id)
        {
            return DbContext.Drinks.Find(id);
        }
        public List<DrinkViewModel> GetAll()
        {
            return DbContext.Drinks.ToList();
        }
    }
}
