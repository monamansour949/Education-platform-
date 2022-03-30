using ProjectItiTeam.Models;
using System.Collections.Generic;

namespace ProjectItiTeam.Repository
{
    public interface ICommentRepository
    {
        int Delete(int id);
        List<Comment> GetAll(int id);
        Comment getbyid(int id);
        int Insert(Comment newcomment);
        int Update(int id, Comment comment);
    }
}