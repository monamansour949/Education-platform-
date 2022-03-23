using ProjectItiTeam.Models;
using System.Collections.Generic;

namespace ProjectItiTeam.Repository
{
    public interface IExamRepository
    {
        int Delete(int id);
        List<Exam> GetAll();
        Exam GetById(int id);
        int Insert(Exam newExam);
        int Update(int id, Exam exam);
    }
}