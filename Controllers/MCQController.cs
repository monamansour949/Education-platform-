using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System.Collections.Generic;
namespace ProjectItiTeam.Controllers
{
    public class MCQController : Controller
    {
        IMCQRepository MCQRepository;
        ILevelRepository LevelRepository;
        public MCQController(IMCQRepository MCQRepository, ILevelRepository levelRepository)
        {
            this.MCQRepository = MCQRepository;
            this.LevelRepository = levelRepository;
        }
        public IActionResult Index(int id)
        {
            List<MCQ> MCQs = MCQRepository.GetAllBylevelId(id);
            return View(MCQs);
        }
        public IActionResult Details(int id)
        {
            MCQ mCQ  = MCQRepository.GetOneWithChoices(id);

            return View(mCQ);
        }
        public IActionResult Create()
        {

            ViewBag.Levels = LevelRepository.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(MCQ MCQ)
        {
            if (ModelState.IsValid)
            {
                MCQRepository.Insert(MCQ);
                return RedirectToAction("Index", new { id = MCQ.Level_Id });
            }
            return View("Create", MCQ);
        }

        public IActionResult Edit(int id)
        {
            MCQ MCQ  = MCQRepository.GetOneWithChoices(id);
            ViewBag.Levels = LevelRepository.GetAll();

            return View(MCQ);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, MCQ MCQ )
        {
            if (ModelState.IsValid == true)
            {
                MCQRepository.Update(id, MCQ);
                return RedirectToAction("Index", new { id = MCQ.Level_Id });
            }
      

            return View("Edit", MCQ);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            MCQ MCQ = MCQRepository.GetById(id);
            MCQRepository.Delete(id);
            return RedirectToAction("Index", new { id = MCQ.Level_Id });
        }
    }
}
