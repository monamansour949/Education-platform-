using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Controllers
{
    public class AudioController : Controller
    {
        IAudioRepository AudioRepository;//=new AudioRepository();
        ILevelRepository LevelRepository;//=new LevelRepository();

        public AudioController(IAudioRepository aduioRepo, ILevelRepository levelRepo)
        {
            AudioRepository = aduioRepo;
            LevelRepository = levelRepo;
        }

        public IActionResult testGuid()
        {

            ViewBag.id = AudioRepository.id;
            return View();
        }
        //Index
        public IActionResult Index()
        {
            List<Audio> audio = AudioRepository.GetAll();
            return View("Index", audio);
        }
        //Details
        public IActionResult Details(int Id)
        {
            Audio audio =
                 AudioRepository.GetById(Id);

            return View("Details", audio);
        }
        //New
        public IActionResult New()
        {
            return View("New",new Audio());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Audio audio)
        {
            if (audio.Name != null && audio.Level_ID != 0)
            {
                AudioRepository.Insert(audio);
                return RedirectToAction("Idex");
            }
            return View("New", audio);
        }
        //Edit
        public IActionResult Edit(int id)
        {
            Audio audio = AudioRepository.GetById(id);
            List<Level> levels = LevelRepository.GetAll();
            ViewBag.lvl = levels.ToList();
            return View("Edit", audio);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit([FromRoute] int id, Audio audio)
        {
            if (audio.Name != null)
            {
                AudioRepository.update(id, audio);
                return RedirectToAction("Index");
            }
            List<Level> levels = LevelRepository.GetAll();
            ViewBag.lvl = levels.ToList();
            return View("Edit", audio);
        }
        //Delete
        public IActionResult Delete(int id)
        {
            Audio audio = AudioRepository.GetById(id);
            return View("Delete",audio);
        }

        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            AudioRepository.delete(id);
            return RedirectToAction("Index");

        }
    }
}
