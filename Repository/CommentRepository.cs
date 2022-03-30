using Microsoft.EntityFrameworkCore;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectItiTeam.Repository
{
    public class CommentRepository : ICommentRepository
    {
        ApplicationDbContext context;

        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Comment> GetAll(int id)
        {
            return context.comments.Where(c => c.CourseId == id).ToList();
        }

        public Comment getbyid(int id)
        {
            return context.comments.FirstOrDefault(c => c.Id == id);
        }

        public int Insert(Comment newcomment)
        {
            context.comments.Add(newcomment);
            return context.SaveChanges();
        }

        public int Update(int id, Comment comment)
        {
            Comment oldcomment = getbyid(id);
            if (oldcomment != null && oldcomment.UserId == comment.UserId)
            {

                oldcomment.Text = comment.Text;
                oldcomment.Date = comment.Date;
                return context.SaveChanges();
            }
            return 0;
        }

        public int Delete(int id)
        {
            Comment oldcomment = getbyid(id);
            context.comments.Remove(oldcomment);
            return context.SaveChanges();
        }
    }
}
