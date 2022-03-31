using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectItiTeam.Models
{
    public class Artical
    {
        [Key]
        public int ID { get; set; }
        public string UserNAme { get; set; }
        public string Image { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public DateTime date { get; set; }
    }
}
