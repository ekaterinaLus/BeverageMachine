using BeverageMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext DbContext { get; set; }
        public GenericRepository(ApplicationContext context)
        {
            DbContext = context;
        }

        public void Create(T element)
        {
            DbContext.Set<T>().Add(element);
        }

        public void Delete(T element)
        {
            DbContext.Set<T>().Remove(element);
        }

        public T Get(int id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return DbContext.Set<T>().ToList();
        }
    }
}
