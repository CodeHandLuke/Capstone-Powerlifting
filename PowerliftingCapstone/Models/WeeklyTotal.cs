using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class WeeklyTotal
	{
		[Key]
		public int WeeklyTotalId { get; set; }
		public int Week { get; set; }
		public string Exercise { get; set; }
		public int? Reps { get; set; }
		[Display(Name = "Weight(kg)")]
		public double? Weight { get; set; }
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public UserProfile User { get; set; }
	}
}