using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectItiTeam.Models
{
    [Table("Question")] 
    public class Question
    {
        public int Id { get; set; }
        [ForeignKey("Level")]
        public int Level_Id { get; set; }
        public virtual Level Level { get; set; }
        public virtual List<Exam> Exams { get; set; }
        public virtual List<Quiz> Quizzes { get; set; }

    }
}
