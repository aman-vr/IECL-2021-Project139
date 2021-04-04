using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Paycompute.Persistence
{
    public static class DataSeedingInitializer
    {
        public static async Task UserAndRoleSeedAsync(UserManager<IdentityUser> userManager, 
                                                RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach(var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Create Admin user
            if(userManager.FindByEmailAsync("av@ifelsecloud.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "av@ifelsecloud.com",
                    Email = "av@ifelsecloud.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "0@IfElse").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }


            //Create Manager user
            if (userManager.FindByEmailAsync("manager@ifelsecloud.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "manager@ifelsecloud.com",
                    Email = "manager@ifelsecloud.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "0@IfElse").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            //Create Staff user
            if (userManager.FindByEmailAsync("johncena@ifelsecloud.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "johncena@ifelsecloud.com",
                    Email = "johncena@ifelsecloud.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "0@IfElse").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }
            }

            //Create No Role user
            if (userManager.FindByEmailAsync("dog@ifelsecloud.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "dog@ifelsecloud.com",
                    Email = "dog@ifelsecloud.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "0@IfElse").Result;
                //NO ROLE ASSIGNED to a Dog. It just spreads love.
            }
        }
    }
}
