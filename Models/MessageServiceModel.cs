namespace SkillSkulptor.Models
{
	public class MessageServiceModel
	{
		public List<Message> messagesObject { get; set; }
		public AppUser receiver { get; set; }
	}
}
