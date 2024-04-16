namespace TheWhiskyRealm.Core.Models.Event;

public class EventDetailsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string OrganiserName { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string EndDate { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public string VenueName { get; set; } = string.Empty;
    public int AvailableSpots { get; set; }
    public ICollection<string> JoinedUsers { get; set; } = new List<string>();
}
