using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class SavedWorkoutDateTime
	{
		[Key]
		public int SavedWorkoutDateId { get; set; }
		public DateTime Date { get; set; }
		public int WorkoutId { get; set; }
	}
}