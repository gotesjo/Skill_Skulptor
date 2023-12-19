namespace SkillSkulptor.Models
{
	public class SearchViewModel
	{
		public string SearchString { get; set; }
		public AppUser LoggedInUser { get; set; }
		public List<AppUser> Users { get; set; }

	}
}
