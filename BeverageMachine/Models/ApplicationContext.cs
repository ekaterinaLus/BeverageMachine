using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BeverageMachine.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {     
            public System.Data.Entity.DbSet<DrinkModel> Drinks { get; set; }
            
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
