using ProjectItiTeam.Models;
using System.Linq;

using ProjectItiTeam.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectItiTeam.Repository
{
    public class QuestionRepoistory : IQuestionRepoistory
    {
        ApplicationDbContext Context;
        public QuestionRepoistory(ApplicationDbContext context)
        {
            this.Context = context;
        }

        public List<Question> GetAllByLevelId(int id) // Get All Qustions in Selected level
        {
            return Context.Questions.Where(Q => Q.Level_Id == id).ToList();
        }
        public Question GetById(int id)  // Get One Question  
        {
            return Context.Questions.FirstOrDefault(Q => Q.Id == id);
        }

        public int Insert(Question question)
        {
            Context.Questions.Add(question);
            return Context.SaveChanges();
        }
        public int Update(int id,Question question)
        {
            Question questionUpdated = Context.Questions.FirstOrDefault(q => q.Id == id);
            questionUpdated.Level_Id = question.Level_Id;
            return Context.SaveChanges();
        }



        public int Delete(int id)
        {
            Question question = GetById(id);
            Context.Questions.Remove(question);
            return Context.SaveChanges();
        }
    }
}
