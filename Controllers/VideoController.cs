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
    public class VideoController : Controller
    {
        IVideoRepository VideoRepository;//=new VideoRepository();
        ICourseRepository CourseRepository;//=new CourseRepository();
        private readonly IWebHostEnvironment webHostEnvironment;

        public VideoController(IVideoRepository videoRepo, ICourseRepository courseRepo, IWebHostEnvironment WebHostEnvironment)
        {
            VideoRepository = videoRepo;
            CourseRepository = courseRepo;
            webHostEnvironment = WebHostEnvironment;
        }

        public IActionResult testGuid()
        {

            ViewBag.id = VideoRepository.id;
            return View();
        }
        //Index
        public IActionResult Index()
        {
            List<Video> video = VideoRepository.GetAll();
            return View("Index", video);
        }
        //Details
        public IActionResult Details(int Id)
        {
            Video video =
                 VideoRepository.GetById(Id);

            return View("Details", video);
        }
        //New

        public IActionResult New()
        {
            ViewData["Course_ID"] = new SelectList(CourseRepository.GetAll(), "Id", "Name");
            return View("New", new Video());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Video video)
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
                    FileStream fileStream = new FileStream(Path.Combine(host, "PathVideos", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    VidPAth = @"\PathVideos\" + d + ImageName;
                }
                video.pathViedoes = VidPAth;
            }

            if (video.Name != null && video.Course_ID != 0)
            {
                VideoRepository.Insert(video);
                return RedirectToAction("Index");
            }
            ViewData["Course_ID"] = new SelectList(CourseRepository.GetAll(), "Id", "Name");

            return View("New", video);
        }
        //Edit
        public IActionResult Edit(int id)
        {
            Video video = VideoRepository.GetById(id);
            List<Course> courses = CourseRepository.GetAll();
            ViewBag.crs = courses.ToList();
            ViewData["Course_ID"] = new SelectList(CourseRepository.GetAll(), "Id", "Name");

            return View("Edit", video);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit([FromRoute] int id, Video video)
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
                    FileStream fileStream = new FileStream(Path.Combine(host, "PathVideos", d + ImageName), FileMode.Create);
                    files[0].CopyTo(fileStream);

                    VidPAth = @"\PathVideos\" + d + ImageName;
                }
                video.pathViedoes = VidPAth;
            }
            if (video.Name != null)
            {
                VideoRepository.update(id, video);
                return RedirectToAction("Index");
            }
            List < Course > courses = CourseRepository.GetAll();
            ViewBag.crs = courses.ToList();
            ViewData["Course_ID"] = new SelectList(CourseRepository.GetAll(), "Id", "Name");

            return View("Edit", video);
        }

        //Delete
        public IActionResult Delete(int id)
        {
            Video video = VideoRepository.GetById(id);
            return View("Delete", video);
        }

        [HttpPost]
        public IActionResult SaveDelete(int id)
        {
            VideoRepository.delete(id);
            return RedirectToAction("Index");

        }
    }
}
