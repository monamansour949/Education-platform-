using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System.Collections.Generic;

namespace ProjectItiTeam.Controllers
{
    public class LevelController : Controller
    {
        ILevelRepository levelRepository;

        public LevelController(ILevelRepository levelRepository)
        {
            this.levelRepository = levelRepository;
        }

        public IActionResult Index()
        {
            List<Level> levels = levelRepository.GetAll();

            return View(levels);
        }

        [HttpPost]
        public IActionResult SaveNew(Level level)
        {
            if (level.Name !=null)
            {
                levelRepository.Insert(level);
                return RedirectToAction("Index");
            }
            return View("New",level);
        }

        public IActionResult Edit(int id)
        {
           Level level = levelRepository.GetById(id);

            return View("Edit", level);
        }

        [HttpPost]
        public IActionResult SaveEdit([FromRoute] int id, Level newlevel)
        {
            if (ModelState.IsValid == true)
            {

              levelRepository.Update(id, newlevel);
                return RedirectToAction("Index");
            }


            return View("Edit", newlevel);
        }

        public IActionResult Delete(int id)
        {
           Level level = levelRepository.GetById(id);
            return View("Delete", level);
        }
        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
           levelRepository.Delete(id);
            return RedirectToAction("Index");


        }
    }
}
