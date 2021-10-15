using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using ASPNETCOREFULL.DataAccess.Concrete.Context;
using ASPNETCOREFULL.DataAccess.Concrete.Identity;
using ASPNETCOREFULL.DataAccess.Concrete.Repository.EFRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETCOREFULL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddRazorPages();
            services.AddControllersWithViews(); //MVC modulunu servislere eklememize yarýyor.

            #region dependecyinjection
            services.AddDbContext<FullContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<IProductRepository, EfProductRepository>();
            #endregion

            services.AddSession(); //Session örnegýnde bunu ekledýk.

            #region Identity
            // services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<FullContext>(); 
            //1.Identity notunda bunu ekledik.

            //Identity 2 de bunu ekledik.
            //Default ayarlarý deðiþtirdik.
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<FullContext>();

            #endregion
        }

        //MIDDLEWARE
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) //IdentityInitializer sýnfýýný kullanmak için parameteryle user ve role manger aldýk. Identity 2.
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            IdentityInitializer.CreateAdminUserAndAdminRole(userManager, roleManager);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseStaticFiles(
            //    new StaticFileOptions() {
            //     FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"node_modules")),
            //     RequestPath="/content"               
            //    });

            app.UseRouting();

            app.UseSession();//Session örnegýnde bunu ekledýk.
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "startupRootu",
                    pattern: "/startup",//Bu istek geldiðinden git
                    defaults: new { Controller = "StartupRoot", Action = "Index" }
                    );
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{Action=Index}/{id?}");
            });  //MVC modulunu kullanmamýza yarýyor.
        }
    }
}
