using BeverageMachine.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public interface IApplicationContext
    { 
        DbSet<Drink> Drinks { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<PurchasedGood> PurchasedGoods { get; set; }
        DbSet<ShoppingBasket> ShoppingBaskets { get; set; }
        public int SaveChanges();
        public EntityEntry<TEntity> Add<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}