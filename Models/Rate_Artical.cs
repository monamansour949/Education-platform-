using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectItiTeam.Models
{
    public class Rate_Artical
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public int Stars { get; set; }
        public int dislike { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Artical")]
        public int Artical_ID { get; set; }
        public string UserName { get; set; }
        public virtual Artical Artical { get; set; }
    }
}
