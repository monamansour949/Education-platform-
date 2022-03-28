using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectItiTeam.Models
{
    public class Video
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(90)]
        public string Description { get; set; }

        [StringLength(90)]
        public string VideoUrl { get; set; }

        [ForeignKey("Course")]
        public int Course_ID { get; set; }
        public int User_Id { get; set; }

        public virtual Course Course { get; set; }

    }
}
 