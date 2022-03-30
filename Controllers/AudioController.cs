using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectItiTeam.Controllers
{
    public class AudioController : Controller
    {
        IAudioRepository AudioRepository;//=new AudioRepository();
        ILevelRepository LevelRepository;//=new LevelRepository();
        private readonly IWebHostEnvironment webHostEnvironment;

        public AudioController(IAudioRepository aduioRepo, ILevelRepository levelRepo, IWebHostEnvironment WebHostEnvironment)
        {
            AudioRepository = aduioRepo;
            LevelRepository = levelRepo;
            webHostEnvironment = WebHostEnvironment;
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
        public IActionResult New() //creat not new to be standerd
        {
            ViewData["Course_ID"] = new SelectList(LevelRepository.GetAll(), "Id", "Name");

            return View("New",new Audio());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Audio audio)
        {
            if (ModelState.IsValid)
            {
                string VidPAth = @"\PathVideos\default.png";
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    Guid d = Guid.NewGuid();

                    string host = webHostEnvironment.WebRootPath;
                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);
                    FileStream fileStream = new FileStream(Path.Combine(host, "PathAudio", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    VidPAth = @"\PathAudio\" + d + ImageName;
                }
                audio.AudioUrl = VidPAth;
            }
                if (audio.Name != null && audio.Level_ID != 0)
            {

                AudioRepository.Insert(audio);
                return RedirectToAction("Index");
            }
            ViewData["Course_ID"] = new SelectList(LevelRepository.GetAll(), "Id", "Name");

            return View("New", audio);
        }
        //Edit
        public IActionResult Edit(int id)
        {
            Audio audio = AudioRepository.GetById(id);
            List<Level> levels = LevelRepository.GetAll();
            ViewBag.lvl = levels.ToList();  // Mahran: levels here actuly list we didn't need to use ToList
            ViewData["Course_ID"] = new SelectList(LevelRepository.GetAll(), "Id", "Name");

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
            ViewBag.lvl = levels.ToList(); // Mahran: levels here actuly list we didn't need to use ToList
            ViewData["Course_ID"] = new SelectList(LevelRepository.GetAll(), "Id", "Name");

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
