using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class UserProfile
	{
		[Key]
		public int UserProfileId { get; set; }
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		public int Age { get; set; }
		[Display(Name = "Sex(Male or Female)")]
		public string Sex { get; set; }
		[Display(Name = "Weight(kg)")]
		public int Weight { get; set; }
		public double Wilks { get; set; }
		public int? WorkoutOfDay { get; set; }
		[ForeignKey("ApplicationUser")]
		public string ApplicationId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
	}
}