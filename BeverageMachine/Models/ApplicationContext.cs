using BeverageMachine.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Objects;

namespace BeverageMachine.Models
{
    public class ApplicationContext : IdentityDbContext<UserViewModel, IdentityRole, string>
    {
        public enum RoleName
        { 
            Admin,
            User
        }
        public Microsoft.EntityFrameworkCore.DbSet<DrinkViewModel> Drinks { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<ShoppingBasketViewModel> ShoppingBaskets { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PurchasedGoodViewModel> PurchasedGoods { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<OrderViewModel> Orders { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
        {
        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-JCNQMTB\\SQLEXPRESS;Database=GoodsStoreNew;Trusted_Connection=True;");
        }

        //public static void ReloadEntity<TEntity>(
        //this IdentityDbContext context, TEntity entity)
        //where TEntity : class
        //{
        //    context.Entry(entity).Reload();
        //}

    }
}
