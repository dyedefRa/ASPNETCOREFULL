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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-STGF1UC;database=COREFULL;integrated security=true;");
        }

        public DbSet<Category> Categories { get; }

        public DbSet<Product> Products { get; }
    }
}
