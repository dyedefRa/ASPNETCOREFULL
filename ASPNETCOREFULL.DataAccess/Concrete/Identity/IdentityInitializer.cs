using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.DataAccess.Concrete.Identity
{
    public class IdentityInitializer
    {
        //UserManagerdaki default sınıfı degıstırıp AppUser yaptıgımız ıcın UserManager a AppUser verdık.
        public static void CreateAdminUserAndAdminRole(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null && roleManager.FindByNameAsync("admin").Result == null)
            {
                AppUser adminUser = new AppUser();
                adminUser.UserName = "admin";
                var user = userManager.CreateAsync(adminUser, "123").Result;

                IdentityRole adminRole = new IdentityRole();
                adminRole.Name = "admin";
                var role = roleManager.CreateAsync(adminRole).Result;

                var userManagers = userManager.AddToRoleAsync(adminUser, adminRole.Name).Result;

            }
        }
    }
}
