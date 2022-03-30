using ProjectItiTeam.Models.Identity;
using System;

namespace ProjectItiTeam.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
