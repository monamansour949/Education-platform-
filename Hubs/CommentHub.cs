using Microsoft.AspNetCore.SignalR;
using ProjectItiTeam.Data;
using ProjectItiTeam.Models;
using ProjectItiTeam.Models.Identity;
using System;

namespace ProjectItiTeam.Hubs
{
    public class CommentHub:Hub
    {
        ApplicationDbContext applictionDbcontext;

        public CommentHub(ApplicationDbContext applictionDbcontext)
        {
            this.applictionDbcontext = applictionDbcontext;
        }

        public void WriteComment(string UserName, string CommentText, string CourseID)
        {
            Comment comment = new Comment() { 
                Text = CommentText, 
                Date = DateTime.Now,
                UserId = UserName,
                CourseId = int.Parse(CourseID) 
            };
            applictionDbcontext.comments.Add(comment);
            Clients.All.SendAsync("NewComment", UserName, CommentText, CourseID);
            applictionDbcontext.SaveChanges();
        }
    }
}
