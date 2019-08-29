using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class Thread
	{
		[Key]
		public int ThreadId { get; set; }
		public DateTime DateTime { get; set; }
		[Display(Name = "Posted By")]
		public string PostedBy { get; set; }
		[Display(Name = "Thread Title")]
		public string ThreadTitle { get; set; }
		public int Posts { get; set; }
		[Display(Name = "Last Post")]
		public DateTime LastPost { get; set; }
		public virtual List<Post> ThreadPosts { get; set; }

	}
}