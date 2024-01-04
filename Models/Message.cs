using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SkillSkulptor.Models
{
	public class Message
	{
		[Key]
		public int MessageId { get; set; }

		[Required]
        [MaxLength(500, ErrorMessage = "Du får bara skicka max 100 tecken")]
        public string Text { get; set; }
		public DateTime Datum { get; set; }
		public Boolean ViewStatus { get; set; }
		public string? UnknownUser { get; set; }

		public string? FromUserID { get; set; }
		[Required]
		public string ToUserID { get; set; }

		[ForeignKey(nameof(FromUserID))]
		public virtual AppUser fkFromUser { get; set; }

		[ForeignKey(nameof(ToUserID))]
		public virtual AppUser fkToUser { get; set;}
		



	}
}
