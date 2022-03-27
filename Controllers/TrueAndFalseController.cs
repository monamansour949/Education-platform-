using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System.Collections.Generic;


namespace ProjectItiTeam.Controllers
{
    public class TrueAndFalseController : Controller
    {
        ITrueAndFalseRepository TrueAndFalseRepository;
        ILevelRepository LevelRepository;
        public TrueAndFalseController(ITrueAndFalseRepository trueAndFalseRepository, ILevelRepository levelRepository)
        {
            TrueAndFalseRepository = trueAndFalseRepository;
            LevelRepository = levelRepository;
        }
        public IActionResult Index(int id)
        {

            List<TrueAndFalse> trueAndFalses = TrueAndFalseRepository.GetAllByLevelId(id);
            return View(trueAndFalses);
        }
        public IActionResult Details(int id)
        {
            TrueAndFalse trueAndFalse = TrueAndFalseRepository.GetById(id);

            return View(trueAndFalse);
        }
        public IActionResult Create()
        {
            ViewBag.Levels = LevelRepository.GetAll();
            return View();
        }

        [HttpPost]
        public  ActionResult Create(TrueAndFalse trueAndFalse )
        {
            if (ModelState.IsValid)
            {
                TrueAndFalseRepository.Insert(trueAndFalse);
                return RedirectToAction("Index", new {id = trueAndFalse.Level_Id });
            }
            return View("Create", trueAndFalse);
        }

        public IActionResult Edit(int id)
        {
            TrueAndFalse trueAndFalse  = TrueAndFalseRepository.GetById(id);
            ViewBag.Levels = LevelRepository.GetAll();

            return View(trueAndFalse);
        }

        [HttpPost] 
        public IActionResult Edit([FromRoute] int id, TrueAndFalse trueAndFalse )
        {
            if (ModelState.IsValid == true)
            {
                TrueAndFalseRepository.Update(id, trueAndFalse);
                return RedirectToAction("Index", new {id = trueAndFalse.Level_Id} );
            }


            return View("Edit", trueAndFalse);
        }

      
        [HttpGet]
        public IActionResult Delete(int id)
        {
            TrueAndFalse trueAndFalse = TrueAndFalseRepository.GetById(id);
            TrueAndFalseRepository.Delete(id);
            return RedirectToAction("Index", new {id = trueAndFalse.Level_Id });
        }
    }
}
