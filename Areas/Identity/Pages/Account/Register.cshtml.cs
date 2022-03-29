using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using ProjectItiTeam.Models.Identity;

namespace ProjectItiTeam.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager, IWebHostEnvironment WebHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.roleManager = roleManager;
            webHostEnvironment = WebHostEnvironment;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Phone")]
            public string PhoneNumber { get; set; }
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

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                string imagesPAth = @"\PhotoUser\default.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    Guid d = Guid.NewGuid();

                    string host = webHostEnvironment.WebRootPath;
                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(host, "PhotoUser", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    imagesPAth = @"\PhotoUser\" + d + ImageName;
                }
                    var user = new ApplicationUser
                { 
                    UserName = Input.Email,
                    Email = Input.Email ,
                    Name=Input.Name,
                    PhoneNumber=Input.PhoneNumber,
                    City=Input.City,
                    address=Input.address,
                    picture=imagesPAth,
                    Isactive =Input.Isactive,
                    statue=Input.statue
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await roleManager.RoleExistsAsync(SD.Admin))
                    {
                        await roleManager.CreateAsync(new IdentityRole(SD.Admin));
                        await roleManager.CreateAsync(new IdentityRole(SD.Manager));
                        await roleManager.CreateAsync(new IdentityRole(SD.user));
                    }

                    string role = Request.Form["rdUserRole"].ToString();
                    if (role == SD.Admin)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Admin);
                    }
                    else if (role == SD.Manager)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Manager);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.user);
                    }
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please Confirm your account by <br/> <a style='background-color:blue;color:white;padding:8px;' href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
