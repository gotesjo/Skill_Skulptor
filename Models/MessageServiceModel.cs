namespace SkillSkulptor.Models
{
	public class MessageServiceModel
	{
		public List<Message_object> messagesObjects { get; set; }
		public AppUser receiver { get; set; }
		public string Newmessage { get; set; }
	}
}
