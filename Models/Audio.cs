using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectItiTeam.Models
{
    public class Audio
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(90)]
        public string Description { get; set; }

        [ForeignKey("Level")]
        public int Level_ID { get; set; }
        public int User_Id { get; set; }

        public virtual Level Level { get; set; }

    }
}
