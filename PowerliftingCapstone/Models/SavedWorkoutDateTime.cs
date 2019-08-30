using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class SavedWorkoutDateTime
	{
		[Key]
		public int SavedWorkoutDateTimeId { get; set; }
		[Display(Name = "Workout Completion")]
		public DateTime Date { get; set; }
		public int WorkoutId { get; set; }
		[Display(Name = "Completed Reps")]
		public string CompletedReps { get; set; }
		[Display(Name = "Completed Weight")]
		public string CompletedWeight { get; set; }
		public virtual int? ActualReps { get; set; }
		public virtual int? ExpectedReps { get; set; }
		public virtual double? ActualWeight { get; set; }
		public virtual double? ExpectedWeight { get; set; }
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public UserProfile User { get; set; }
	}
}