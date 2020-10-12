using BeverageMachine.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Objects;

namespace BeverageMachine.Models
{
    public class ApplicationContext : IdentityDbContext<User, IdentityRole, string>
    {
        public enum RoleName
        { 
            Admin,
            User
        }
        public Microsoft.EntityFrameworkCore.DbSet<Drink> Drinks { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<ShoppingBasket> ShoppingBaskets { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PurchasedGood> PurchasedGoods { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }
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
