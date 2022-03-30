using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.Identity.Repositery;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectItiTeam.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        ICourseRepository CourseRepository;
        private readonly ILevelRepository LevelRepository;
        private readonly IRepositery repositery;
        private readonly IRateRepository rateRepo;
        ICommentRepository commentRepository;
        public CoursesController(ICourseRepository courseRepository,ILevelRepository repo, IRepositery repositery,
            IRateRepository rateRepo , ICommentRepository commentRepos )
        {
            this.CourseRepository = courseRepository;
            this.LevelRepository = repo;
            this.repositery = repositery;
            this.rateRepo = rateRepo;
            this.commentRepository = commentRepos;
        }
        public IActionResult Index()
        {
            List<Course> courses = CourseRepository.GetAll();

            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Levels = LevelRepository.GetAll();
            return View(new Course());
        }
        [HttpPost]
        public async Task<ActionResult> Create(Course course)
        {
            if (course.Name != null && course.Level_Id != 0)
            {
                var clamidentity = (ClaimsIdentity)this.User.Identity;
                var clams = clamidentity.FindFirst(ClaimTypes.NameIdentifier);

                var data =await repositery.GetById(clams.Value);
                course.User_Id = data.Name;
                CourseRepository.Insert(course);
                ViewBag.Levels = LevelRepository.GetAll();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        public IActionResult Edit(int id)
        {
            Course course = CourseRepository.GetById(id);
            ViewBag.LevelList = LevelRepository.GetAll();

            return View(course);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Course newcourse)
        {
            if (ModelState.IsValid == true)
            {
                CourseRepository.Update(id, newcourse);
                return RedirectToAction("Index");
            }


            return View("Edit", newcourse);
        }

        public IActionResult Details(int id)
        {
            dataTab_detVM model = new dataTab_detVM();
            model.courses = CourseRepository.GetById(id);
            model.coursesLi = CourseRepository.GetAll();
            model.Level = LevelRepository.GetAll();
            Rate data = rateRepo.GetByIdCourse(id);
            if (data == null)
            {
                Rate rate = new Rate();
                rate.Stars = 0;
                rate.dislike = 0;
                ViewBag.dataa = rate;
            }
            else
            {
                ViewBag.dataa = data;
            }
            ViewBag.Comments = commentRepository.GetAll(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            CourseRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
