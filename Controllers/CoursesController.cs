using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System.Collections.Generic;

namespace ProjectItiTeam.Controllers
{
    public class CoursesController : Controller
    {
        ICourseRepository CourseRepository;
        private readonly ILevelRepository LevelRepository;

        public CoursesController(ICourseRepository courseRepository,ILevelRepository repo)
        {
            this.CourseRepository = courseRepository;
            this.LevelRepository = repo;
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
        public IActionResult Create(Course course)
        {
            if (course.Name != null && course.Level_Id != 0)
            {
                CourseRepository.Insert(course);
                return RedirectToAction("Index");
            }
            return View("New", course);
        }

        public IActionResult Edit(int id)
        {
            Course course = CourseRepository.GetById(id);
            ViewBag.LevelList = LevelRepository.GetAll();

            return View("Edit", course);
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
