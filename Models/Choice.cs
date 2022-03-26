using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectItiTeam.Models
{
    public class choice
    {
        public int ID { get; set; }
        public  string Text { get; set; }
        [ForeignKey("MCQ")]
        public int MCQ_Id { get; set; }
        public virtual MCQ MCQ { get; set; }
    }
}
