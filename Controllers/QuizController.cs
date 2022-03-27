using Microsoft.AspNetCore.Mvc;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.ViewModel;
using ProjectItiTeam.Repository;
using System.Collections.Generic;

namespace ProjectItiTeam.Controllers
{
    public class QuizController : Controller
    {
        IQuestionRepoistory QuestionRepoistory;
        ILevelRepository LevelRepository;
        public QuizController(IQuestionRepoistory questionRepoistory, ILevelRepository levelRepository)
        {
            QuestionRepoistory = questionRepoistory;
            LevelRepository = levelRepository;

        }
        public IActionResult Index()
        {
           // List(Quiz)
            return View();
        }
    }
}
