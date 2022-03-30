using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using System.Linq;

namespace ProjectItiTeam.Models.Identity.USerr
{
    public class admainstrator : Iadmainstrator
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public admainstrator(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Inilations()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (System.Exception)
            {

                throw;
            }
            if (_context.Roles.Any(r => r.Name == SD.Admin)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.user)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Manager)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "bob@gmail.com",
                Email = "bob@gmail.com",
                EmailConfirmed = true,
                Name = "beshoy"
            }, "OPOP@OP123").GetAwaiter().GetResult();

            ApplicationUser user = _context.ApplicationUsers.Where(u => u.Email == "bob@gmail.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, SD.Admin).GetAwaiter().GetResult();
        }
    }
}
