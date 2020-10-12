using BeverageMachine.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine
{
    public static class DbHelper
    {
        private static readonly Dictionary<Type, dynamic> _comparersList = new Dictionary<Type, dynamic>
        {
            { typeof(Drink), new GenericCompare<Drink> { GetComparableField = x => x.Name.ToLower() } }
        };
        public static async Task AddUniqueElementsAsync<T>(this DbSet<T> @this, IEnumerable<T> addingElements)
        where T : class
        {
            IEqualityComparer<T> comparer = _comparersList[typeof(T)];
            IEnumerable<T> elementsInDb = await @this.AsNoTracking().ToArrayAsync();
            await @this.AddRangeAsync(addingElements.Except(elementsInDb.Intersect(addingElements, comparer), comparer));
        }
    }
}
