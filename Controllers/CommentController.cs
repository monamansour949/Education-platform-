using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Repository;
using System.Collections.Generic;

namespace ProjectItiTeam.Controllers
{
    public class CommentController : Controller
    {
        ICommentRepository commentrepository;

        public CommentController(ICommentRepository commentrepository)
        {
            this.commentrepository = commentrepository;
        }

        public IActionResult Index(int id)
        {
            List<Comment> comments = commentrepository.GetAll(id);
            return View(comments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Comment());
        }
        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            if(ModelState.IsValid==true)
            {
                commentrepository.Insert(comment);
                RedirectToAction("Details", "Courses", new { id = comment.CourseId });
            }

            return View("Create", comment);
        }
        public IActionResult Edit(int id)
        {
            Comment comment = commentrepository.getbyid(id);
            return View(comment);
        }
        public IActionResult Edit(int id,Comment comment)
        {
            if (ModelState.IsValid == true)
            {
                commentrepository.Update(id, comment);
                return RedirectToAction("Index");
            }
            return View("Edit", comment);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            commentrepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
