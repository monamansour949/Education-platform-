using ProjectItiTeam.Data;
using ProjectItiTeam.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectItiTeam.Data.ViewModels
{
    public class NewVideoViewModel
    {
		public int Id { get; set; }

		// TODO: Use Fluent Validation for validation purposes
		[Display(Name = "Video Name")]
		public string Name { get; set; }

		[Display(Name = "Video Description")]
		public string Description { get; set; }

		[Display(Name = "Video URL")]
		public string VideoUrl { get; set; }

		[Display(Name = "Select Course")]
		public VideoCourse VideoCourse { get; set; }

		#region Relationships
		[Display(Name = "Select User(s)")]
		public List<int> UserID { get; set; }

		[Display(Name = "Select a Course")]
		public int Course_ID { get; set; }
		#endregion
	}
}
