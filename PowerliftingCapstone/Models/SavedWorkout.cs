using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class SavedWorkout
	{
		[Key]
		public int SavedWorkoutId { get; set; }
		public DateTime Date { get; set; }
		public string Exercise { get; set; }
		[Display(Name = "% of 1RM")]
		public int OneRMPercentage { get; set; }
		public int? Reps { get; set; }
		[Display(Name = "Weight(kg)")]
		public double? Weight { get; set; }
		public int WorkoutId { get; set; }
		public string Notes { get; set; }
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public UserProfile User { get; set; }
	}
}