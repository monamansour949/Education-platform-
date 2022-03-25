using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Data.Seed;
using ProjectItiTeam.Models.Identity.Repositery;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectItiTeam.Controllers
{
    [Authorize(Roles = SD.Admin)]
    public class UserController : Controller
    {
        private readonly Models.Identity.Repositery.IRepositery repositery;
        private readonly IBAox repo;

        public UserController(IRepositery repositery, IBAox repo)
        {
            this.repositery = repositery;
            this.repo = repo;
        }
        public async Task<ActionResult> Index()
        {
            var clamidentity=(ClaimsIdentity)this.User.Identity;
            var clams = clamidentity.FindFirst(ClaimTypes.NameIdentifier);
             
            string userId = clams.Value;
            var data =await repositery.GetByIDTolist(userId);

            return View(data);
        }
        public IActionResult Lock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            repositery.LockUser(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult unLock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            repositery.UNLockUser(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddBs(Table model)
        {
            if (ModelState.IsValid)
            {
                 
                try
                {
                    var clamidentity = (ClaimsIdentity)this.User.Identity;
                    var clams = clamidentity.FindFirst(ClaimTypes.NameIdentifier);

                    model.ApplicationUserID = clams.Value;
                    
                    repo.Insert(model);
                }
                catch (System.Exception)
                {
                    return View();
                }
            }
            return View();

        }
    }
}
