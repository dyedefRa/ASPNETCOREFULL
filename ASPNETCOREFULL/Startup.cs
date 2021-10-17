using ASPNETCOREFULL.DataAccess.Abstract.IRepository;
using ASPNETCOREFULL.DataAccess.Concrete.Context;
using ASPNETCOREFULL.DataAccess.Concrete.Identity;
using ASPNETCOREFULL.DataAccess.Concrete.Repository.EFRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddRazorPages();
            services.AddControllersWithViews(); //MVC modulunu servislere eklememize yar�yor.

            #region Identity
            // services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<FullContext>(); 
            //1.Identity notunda bunu ekledik.

            //Identity 2 de bunu ekledik.
            //Default ayarlar� de�i�tirdik.
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<FullContext>();

            #endregion

            #region Authention Areas
            //Identity  cookie kullanuor ve bunun defaultu deg�st�rel�m ; (Buras� Identity regionnudan altta olmas� laz�m bu methodun �c�nde)
            services.AddAuthentication(); //a1 Areaslar i�in bunu kulland�k.
            
            //Defaultu /Account/Login
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/GirisYap"); //Authorize olmam�� bir kullan�c� giri� yapt�g�nda hangi url e yonlens�n.
                opt.Cookie.Name = "AspnetCoreFull";
                opt.Cookie.HttpOnly = true; //Javascript ile �ekilmesin!

                //opt.Cookie.SameSite = SameSiteMode.Lax; //D�� kaynaklar cookie kullanab�l�r.Sub doma�nden tut ba�ka doma�nlerde kullar
                opt.Cookie.SameSite = SameSiteMode.Strict; //Hi� bir  alt domain ve d�� domainler kullanamaz.
                opt.ExpireTimeSpan = TimeSpan.FromDays(1);
            });

            #endregion

            #region dependecyinjection
            services.AddDbContext<FullContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<IProductRepository, EfProductRepository>();
            #endregion

            services.AddSession(); //Session kullanmak i�in bunu ekledik.

       


        }

        //MIDDLEWARE
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager) //IdentityInitializer s�nf��n� kullanmak i�in parameteryle user ve role manger ald�k. Identity 2.
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

            app.UseSession();//Session �rneg�nde bunu ekled�k.


            app.UseAuthentication(); //a2 Areaslar i�in bunu kulland�k.(Giri� yapt� m� yapmad� m�?)
            app.UseAuthorization(); //a3 Areaslar i�in bunu kulland�k.(Kullanc� yetki ve rol)


            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "startupRootu",
                    pattern: "/startup",//Bu istek geldi�inden git
                    defaults: new { Controller = "StartupRoot", Action = "Index" }
                    );
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{Controller=Home}/{Action=Index}/{id?}");
            });  //MVC modulunu kullanmam�za yar�yor.


            //Area i�in ekledik.
            //https://localhost:44304/Admin/Home/Index
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}"
                    );
            });

        }
    }
}
