using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ProjectItiTeam.Models.Identity
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "User Name")]
        public string Name { get; set; }

        [Display(Name = "Image User")]
        public string picture { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        public string statue { get; set; }
        public bool Isactive { get; set; }
    }
}
