using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.Identity.Repositery;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectItiTeam.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        ICourseRepository CourseRepository;
        private readonly ILevelRepository LevelRepository;
        private readonly IRepositery repositery;
        private readonly IRateRepository rateRepo;
        ICommentRepository commentRepository;
        public CoursesController(IWebHostEnvironment WebHostEnvironment, ICourseRepository courseRepository,ILevelRepository repo, IRepositery repositery,
            IRateRepository rateRepo , ICommentRepository commentRepos )
        {
            webHostEnvironment = WebHostEnvironment;
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
            if (ModelState.IsValid)
            {
                string photoPAth = @"\PhotoCourse\default.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    Guid d = Guid.NewGuid();

                    string host = webHostEnvironment.WebRootPath;
                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(host, "PhotoCourse", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    photoPAth = @"\PhotoCourse\" + d + ImageName;
                }
                course.Image = photoPAth;
            }

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
                string photoPAth = @"\PhotoCourse\default.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    Guid d = Guid.NewGuid();

                    string host = webHostEnvironment.WebRootPath;
                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(host, "PhotoCourse", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    photoPAth = @"\PhotoCourse\" + d + ImageName;
                }
                newcourse.Image = photoPAth;

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
