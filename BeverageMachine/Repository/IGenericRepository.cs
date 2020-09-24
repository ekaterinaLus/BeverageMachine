using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Repository
{
    interface IGenericRepository<T> where T: class
    {
        public void Create(T element);
        public void Delete(T element);
        public List<T> GetAll(); 
        public T Get(int id);
    }
}
