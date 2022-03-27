using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System.Collections.Generic;

namespace ProjectItiTeam.Controllers
{
    public class QuestionController : Controller
    {
        IQuestionRepoistory QuestionRepoistory;
        ILevelRepository LevelRepository;
        public QuestionController(IQuestionRepoistory questionRepoistory , ILevelRepository levelRepository)
        {
            QuestionRepoistory = questionRepoistory;
            LevelRepository = levelRepository;
        }
        public IActionResult Index(int id)
        {
            List<Question> questions = QuestionRepoistory.GetAllByLevelId(id);
            return View(questions);
        }
        public IActionResult GetById(int id)
        {
            Question question  = QuestionRepoistory.GetById(id);

            return View(question);
        }
        public IActionResult Create()
        {

            ViewBag.Levels = LevelRepository.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Question question )
        {
            if (ModelState.IsValid)
            {
                QuestionRepoistory.Insert(question);
                return RedirectToAction("Index", new { id = question.Level_Id });
            }
            return View("Create", question);
        }

        public IActionResult Edit(int id)
        {
            Question question  = QuestionRepoistory.GetById(id);
            ViewBag.Levels = LevelRepository.GetAll();

            return View(question);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Question question)
        {
            if (ModelState.IsValid == true)
            {
                QuestionRepoistory.Update(id, question);
                return RedirectToAction("Index", new { id = question.Level_Id });
            }


            return View("Edit", question);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Question question  = QuestionRepoistory.GetById(id);
            QuestionRepoistory.Delete(id);
            return RedirectToAction("Index", new { id = question.Level_Id });
        }

    }
}
