using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class WorkoutSerialization
	{
		[Key]
		public int WorkoutSerialId { get; set; }
		public int WorkoutId { get; set; }
		public string WeekDay { get; set; }
	}
}