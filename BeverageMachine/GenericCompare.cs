using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine
{
    public class GenericCompare<TItem> : IEqualityComparer<TItem>
    {
        public delegate dynamic Field(TItem obj);
        public Field GetComparableField { get; set; }

        public bool Equals(TItem x, TItem y)
        {
            return GetComparableField(x) == GetComparableField(y);
        }

        public int GetHashCode(TItem obj)
        {
            return GetComparableField(obj).GetHashCode();
        }
    }
}
