using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SkillSkulptor.Models
{
	public class Message
	{
		[Key]
		public int MessageId { get; set; }
		public string Text { get; set; }
		public DateTime Datum { get; set; }
		public Boolean ViewStatus { get; set; }

		public string FromUserID { get; set; }
		public string ToUserID { get; set; }

		[ForeignKey(nameof(FromUserID))]
		public virtual AppUser fkFromUser { get; set; }

		[ForeignKey(nameof(ToUserID))]
		public virtual AppUser fkToUser { get; set;}
		



	}
}
