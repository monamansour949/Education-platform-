using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectItiTeam.Hubs;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Controllers
{
    public class RateController : Controller
    {
        IRateRepository RateRepository;//=new RateRepository();
        ICourseRepository CourseRepository;//=new CourseRepository();
        private readonly IHubContext<ProductHub> productHub;

        public RateController(IRateRepository rateRepo,ICourseRepository courseRepo, IHubContext<ProductHub> productHub)
        {
            RateRepository = rateRepo;
            CourseRepository = courseRepo;
            this.productHub = productHub;
        }

        public IActionResult testGuid()
        {

            ViewBag.id = RateRepository.id;
            return View();
        }
        //Index
        public IActionResult Index()
        {
            List<Rate> rate = RateRepository.GetAll();
            return View("Index", rate);
        }
        //New
        public IActionResult New()
        {
            return View(new Rate());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Rate rate)
        {
            if (ModelState.IsValid)
            {
                RateRepository.Insert(rate);
                return RedirectToAction("Index");
            }
            return View("New", rate);
        }



        //Edit
        public IActionResult Edit(int id)
        {
            Rate rate = RateRepository.GetById(id);
            List<Course> courses = CourseRepository.GetAll();
            ViewBag.crs = courses.ToList();
            return View("Edit", rate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit([FromRoute] int id, Rate rate)
        {
            if (ModelState.IsValid)
            {
                RateRepository.update(id, rate);
                return RedirectToAction("GetAll");
            }
            List<Course> course = CourseRepository.GetAll();
            ViewBag.course = course.ToList();
            return View("Edit", rate);
        }

        //Delete
        public IActionResult Delete(int id)
        {
            Rate rate = RateRepository.GetById(id);
            return View(rate);
        }

        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            RateRepository.delete(id);
            return RedirectToAction("Index");

        }
    }
}

