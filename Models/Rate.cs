using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectItiTeam.Models
{
    public class Rate
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public int Stars { get; set; }
        public int dislike { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Course")]
        public int Course_ID { get; set; }
        public int User_ID { get; set; }
        public string UserName { get; set; }
        public virtual Course Course { get; set; }

    }
}
