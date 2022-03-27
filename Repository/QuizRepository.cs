using ProjectItiTeam.Models;
using System.Linq;

using ProjectItiTeam.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace ProjectItiTeam.Repository
{
    public class QuizRepository : IQuizRepository
    {
        ApplicationDbContext Context;
        public QuizRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
        public List<Quiz> GetAllByLevelId(int id) // Get All Quize data in Selected level
        {
            return Context.Quizzes.Where(Q => Q.Level_Id == id).ToList();
        }

        public Quiz GetById(int id)  // Get One Quize data 
        {
            return Context.Quizzes.FirstOrDefault(Q => Q.Id == id);
        }
        public Quiz GetOneByIdWithQuestions(int id) //// Get One Quize data With questions
        {
            return Context.Quizzes.Include(Q => Q.Level).Include(Q => Q.Questions).FirstOrDefault(Q => Q.Id == id);
        }

        public int Insert(Quiz quiz)
        {
            Context.Quizzes.Add(quiz);
            return Context.SaveChanges();
        }

        public int Update(int id, Quiz quiz)
        {
            Quiz QuizUpdated = GetById(id);
            if (QuizUpdated != null)
            {
                QuizUpdated.Date = quiz.Date;
                QuizUpdated.Degree = quiz.Degree;
                QuizUpdated.Mcq_Num = quiz.Mcq_Num;
                QuizUpdated.Tf_Num = quiz.Tf_Num;
                QuizUpdated.Level_Id = quiz.Level_Id;
                QuizUpdated.Questions = quiz.Questions;
                return Context.SaveChanges();

            }
            return 0;
        }

        public int Delete(int id)
        {
            Quiz quiz = GetById(id);
            Context.Quizzes.Remove(quiz);
            return Context.SaveChanges();
        }
    }
}
