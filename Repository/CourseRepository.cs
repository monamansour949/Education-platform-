using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Repository
{
    public class CourseRepository : ICourseRepository
    {
        ApplicationDbContext context;

        public CourseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Course> GetAll()
        {
            return context.Courses.Include(c=>c.Level).ToList();
        }

        public Course GetById(int id)
        {
            return context.Courses.Include(c=>c.Level).FirstOrDefault(c => c.Id == id);
        }

        public int Insert(Course newCourse)
        {
            context.Courses.Add(newCourse);
            return context.SaveChanges();
        }

        public int Update(int id, Course course)
        {
            Course oldcrs = GetById(id);
            if (oldcrs != null)
            {
                oldcrs.Name = course.Name;
                oldcrs.Date = course.Date;
                oldcrs.Level_Id = course.Level_Id;
                oldcrs.User_Id = course.User_Id;

                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Course oldcrs = GetById(id);
            context.Courses.Remove(oldcrs);
            return context.SaveChanges();
        }
    }
}
