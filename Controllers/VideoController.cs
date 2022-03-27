using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Controllers
{
    public class VideoController : Controller
    {
        IVideoRepository VideoRepository;//=new VideoRepository();
        ICourseRepository CourseRepository;//=new CourseRepository();

        public VideoController(IVideoRepository videoRepo, ICourseRepository courseRepo)
        {
            VideoRepository = videoRepo;
            CourseRepository = courseRepo;
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
            return View("New", new Video());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNew(Video video)
        {
            if (video.Name != null && video.Course_ID != 0)
            {
                VideoRepository.Insert(video);
                return RedirectToAction("Index");
            }
            return View("New", video);
        }
        //Edit
        public IActionResult Edit(int id)
        {
            Video video = VideoRepository.GetById(id);
            List<Course> courses = CourseRepository.GetAll();
            ViewBag.crs = courses.ToList();
            return View("Edit", video);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdit([FromRoute] int id, Video video)
        {
            if (video.Name != null)
            {
                VideoRepository.update(id, video);
                return RedirectToAction("Index");
            }
            List < Course > courses = CourseRepository.GetAll();
            ViewBag.crs = courses.ToList();
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
