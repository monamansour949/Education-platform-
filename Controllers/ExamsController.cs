using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System.Collections.Generic;

namespace ProjectItiTeam.Controllers
{
    public class ExamsController : Controller
    {
        IExamRepository ExamRepository;
        public ExamsController(IExamRepository examRepository)
        {
            this.ExamRepository = examRepository;
        }
        public IActionResult Index()
        {
            List<Exam> exams = ExamRepository.GetAll();
            return View(exams);
        }
        public IActionResult Create()
        {
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
