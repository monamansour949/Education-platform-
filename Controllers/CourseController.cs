using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System.Collections.Generic;

namespace ProjectItiTeam.Controllers
{
    public class CourseController : Controller
    {
        ICourseRepository CourseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            this.CourseRepository = courseRepository;
        }

        public IActionResult Index()
        {
            List<Course> courses = CourseRepository.GetAll();

            return View(courses);
        }

        public IActionResult New()
        {
            return View(new Course());
        }

        [HttpPost]
        public IActionResult SaveNew(Course course)
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

            return View("Edit", course);
        }

        [HttpPost]
        public IActionResult SaveEdit([FromRoute] int id, Course newcourse)
        {
            if (ModelState.IsValid == true)
            {

                CourseRepository.Update(id, newcourse);
                return RedirectToAction("Index");
            }


            return View("Edit", newcourse);
        }

        public IActionResult Delete(int id)
        {
            Course course = CourseRepository.GetById(id);
            return View("Delete", course);
        }

        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            CourseRepository.Delete(id);
            return RedirectToAction("Index");


        }
    }

}
