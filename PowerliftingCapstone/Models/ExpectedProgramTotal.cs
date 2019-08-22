using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class ExpectedProgramTotal
	{
		[Key]
		public int ExpectedTotalId { get; set; }
		public string Exercise { get; set; }
		public int Reps { get; set; }
		[Display(Name = "Weight(kg)")]
		public double Weight { get; set; }
	}
}