using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.Identity.Repositery;
using ProjectItiTeam.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectItiTeam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositery repositery;

        public HomeController(ILogger<HomeController> logger, IRepositery repositery)
        {
            _logger = logger;
            this.repositery = repositery;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<ActionResult> add_favourty(Table table)
        {
            if (ModelState.IsValid)
            {

                table.Date = DateTime.Now;
                var clamidentity = (ClaimsIdentity)this.User.Identity;
                var clams = clamidentity.FindFirst(ClaimTypes.NameIdentifier);

                var data = await repositery.GetById(clams.Value);
                table.ApplicationUserID = data.Name;
                return Json("0");
            }
            return Json("1");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
