using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class Lift
	{
		[Key]
		public int ProgramId { get; set; }
		[Display(Name = "Set Order")]
		public int SetOrder { get; set; }
		public int WorkoutId { get; set; }
		public string Exercise { get; set; }
		[Display(Name = "%1RM")]
		public int? OneRMPercentage { get; set; }
		public int? Reps { get; set; }
		[Display(Name = "Weight(kg)")]
		public double? Weight { get; set; }
		public bool Completed { get; set; }
		public bool Notes { get; set; }
		public string NoteText { get; set; }
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public UserProfile User { get; set; }
	}
}