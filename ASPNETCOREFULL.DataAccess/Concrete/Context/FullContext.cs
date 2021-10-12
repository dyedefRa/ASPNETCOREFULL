using ASPNETCOREFULL.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.DataAccess.Concrete.Context
{
    public class FullContext : DbContext
    {

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-STGF1UC;database=COREFULL;integrated security=true;");
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
        public FullContext(DbContextOptions<FullContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
