using System;
using System.ComponentModel.DataAnnotations.Schema;

using System.Collections.Generic;


namespace ProjectItiTeam.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Level")]
        public int Level_Id { get; set; }
        public DateTime Date { get; set; }
        public string User_Id { get; set; }
        public virtual Level Level { get; set; }
        public virtual List<Exam> Exams { get; set; } // mahran add
        public virtual List<Comment> Comments { get; set; }
     }

}
