using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PowerliftingCapstone.Models
{
	public class Post
	{
		[Key]
		public int PostId { get; set; }
		[Display(Name = "Post Time")]
		public DateTime DateTime { get; set; }
		[Display(Name = "Post Text")]
		public string PostText { get; set; }
		[ForeignKey(nameof(Thread))]
		public int ThreadId { get; set; }
		public Thread Thread { get; set; }
		[ForeignKey(nameof(User))]
		public int UserId { get; set; }
		public UserProfile User { get; set; }

	}
}