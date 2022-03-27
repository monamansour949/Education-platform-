using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System.Collections.Generic;

namespace ProjectItiTeam.Controllers
{
    public class ExamsController : Controller
    {
        IExamRepository ExamRepository;
        ICourseRepository courseRepository;
        public ExamsController(IExamRepository examRepository, ICourseRepository courseRepository)
        {
            this.ExamRepository = examRepository;
            this.courseRepository = courseRepository;
        }
        public IActionResult Index()
        {
            List<Exam> exams = ExamRepository.GetAll();
            return View(exams);
        }
        public IActionResult Create()
        {
            ViewBag.CourseList = courseRepository.GetAll();
            return View(new Exam());
        }
        [HttpPost]
        public IActionResult Create(Exam exam)
        {
            if (exam.Degree != 0 && exam.Mcq_Num != 0 && exam.Tf_Num != 0)
            {
                ExamRepository.Insert(exam);
                return RedirectToAction("Index");
            }
            return View("New", exam);
        }
        public IActionResult Edit(int id)
        {
            Exam exam = ExamRepository.GetById(id);
            ViewBag.CourseList = courseRepository.GetAll();
            return View("Edit", exam);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Exam newexam)
        {
            if (ModelState.IsValid == true)
            {
                ExamRepository.Update(id, newexam);
                return RedirectToAction("Index");
            }
            return View("Edit", newexam);
        }
        public IActionResult Details(int id)
        {
            Exam exam = ExamRepository.GetById(id);
            return View(exam);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ExamRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
