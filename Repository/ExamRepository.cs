using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Repository
{
    public class ExamRepository : IExamRepository
    {
        ApplicationDbContext context;

        public ExamRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Exam> GetAll()
        {
            return context.Exams.ToList();
        }

        public Exam GetById(int id)
        {
            return context.Exams.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Exam newExam)
        {
            context.Exams.Add(newExam);
            return context.SaveChanges();
        }

        public int Update(int id, Exam exam)
        {
            Exam oldexam = GetById(id);
            if (oldexam != null)
            {
                oldexam.Course_Id = exam.Course_Id;
                oldexam.Date = exam.Date;
                oldexam.Degree = exam.Degree;
                oldexam.Mcq_Num = exam.Mcq_Num;
                oldexam.Tf_Num = exam.Tf_Num;


                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Exam oldexam = GetById(id);

            context.Exams.Remove(oldexam);
            return context.SaveChanges();
        }



    }
}
