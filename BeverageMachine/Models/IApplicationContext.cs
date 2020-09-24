using BeverageMachine.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public interface IApplicationContext
    { 
        DbSet<DrinkViewModel> Drinks { get; set; }
        DbSet<OrderViewModel> Orders { get; set; }
        DbSet<PurchasedGoodViewModel> PurchasedGoods { get; set; }
        DbSet<ShoppingBasketViewModel> ShoppingBaskets { get; set; }
        public int SaveChanges();
        public EntityEntry<TEntity> Add<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}