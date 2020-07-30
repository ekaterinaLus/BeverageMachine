using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeverageMachine.Models
{
    public class ApplicationContext : DbContext
    {     
            public DbSet<DrinkModel> Drinks { get; set; }

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
                optionsBuilder.UseSqlServer("Server=DESKTOP-JCNQMTB\\SQLEXPRESS;Database=GoodsStore;Trusted_Connection=True;");
            }
    }
}
