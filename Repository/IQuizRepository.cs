using ProjectItiTeam.Models;
using System.Collections.Generic;

namespace ProjectItiTeam.Repository
{
    public interface IQuizRepository
    {
        int Delete(int id);
        List<Quiz> GetAllByLevelId(int id);
        Quiz GetById(int id);
        Quiz GetOneByIdWithQuestions(int id);
        int Insert(Quiz quiz);
        int Update(int id, Quiz quiz);
    }
}