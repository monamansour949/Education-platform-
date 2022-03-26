using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectItiTeam.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Degree { get; set; }
        public int Mcq_Num { get; set; }
        public int Tf_Num { get; set; }
        public virtual List<Question> Questions { get; set; }
        [ForeignKey("Level")]
        public int  Level_Id { get; set; }
        public virtual Level Level{ get; set; }

    }
}
