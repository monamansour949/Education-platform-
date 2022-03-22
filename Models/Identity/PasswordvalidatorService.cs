
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ProjectItiTeam.Models.Identity
{
    public class PasswordvalidatorService : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
        {
            if (password.Contains("ANYTHING YOU DON`T WANT"))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                   Code="Error with password",
                   Description="password is too simple"
                }));

            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
