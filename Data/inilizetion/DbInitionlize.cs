using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data.Seed;
using ProjectItiTeam.Models.Identity;
using System.Linq;

namespace ProjectItiTeam.Data.inilizetion
{
    public class DbInitionlize : Inlizations
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitionlize(UserManager<IdentityUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.context = context;
            this.roleManager = roleManager;
        }

        public void Intialize()
        {
            try
            {
                if (context.Database.GetPendingMigrations().Count() >0)
                {
                    context.Database.Migrate();
                }
            }
            catch (System.Exception)
            {
            }
            if (context.Roles.Any(r => r.Name == Seed.SD.Admin)) return;
            roleManager.CreateAsync(new IdentityRole(Seed.SD.Admin)).GetAwaiter().GetResult();
            roleManager.CreateAsync(new IdentityRole(Seed.SD.Client)).GetAwaiter().GetResult();

            userManager.CreateAsync(new ApplicationUser
            {
                UserName = "Admin@gmail.com",
                Email = "Admin@gmail.com",
                EmailConfirmed = true,
                Name="Admain"
            }, "Admin@123").GetAwaiter().GetResult();

            ApplicationUser user =context.ApplicationUsers.Where(u=>u.Email== "Admin@gmail.com").FirstOrDefault();
            userManager.AddToRoleAsync(user, Seed.SD.Admin).GetAwaiter().GetResult();
        }
    }
}
