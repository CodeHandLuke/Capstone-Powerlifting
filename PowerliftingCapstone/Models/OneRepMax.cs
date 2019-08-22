using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class OneRepMax
	{
		[Key]
		public int OneRepMaxId { get; set; }
		public DateTime Date { get; set; }
		[Display(Name = "Squat(kg)")]
		public double Squat { get; set; }
		[Display(Name = "Bench(kg)")]
		public double Bench { get; set; }
		[Display(Name = "Deadlift(kg)")]
		public double Deadlift { get; set; }
		[Display(Name = "Total(kg)")]
		public double Total { get; set; }
		public double Wilks { get; set; }
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public UserProfile User { get; set; }
	}
}