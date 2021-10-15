using ASPNETCOREFULL.DataAccess.Concrete.Identity;
using ASPNETCOREFULL.Entity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.DataAccess.Concrete.Context
{
    //public class FullContext : DbContext //Normal core için bu
    public class FullContext : IdentityDbContext<AppUser> //Identity kullanmak için bunu kullandık.
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
