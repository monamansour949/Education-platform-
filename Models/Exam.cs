using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectItiTeam.Models
{
    public class Exam
    {
        public int Id { get; set; }


        public DateTime Date { get; set; }
        public int Degree { get; set; }
        [ForeignKey("Course")]
        public int Course_Id { get; set; }
        public int Mcq_Num { get; set; }
        public int Tf_Num { get; set; }
        public virtual Course Course { get; set; }

    }
}
