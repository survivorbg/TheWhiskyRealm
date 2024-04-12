namespace TheWhiskyRealm.Core.Models.AdminArea.Venue;

public class VenueInfoViewModel
{
    public string VenueName { get; set; } = string.Empty;
    public IEnumerable<EventViewModel> PendingEvents { get; set; } = new List<EventViewModel>();
    public IEnumerable<EventViewModel> PastEvents { get; set; } = new List<EventViewModel>();
}

public class EventViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? Price { get; set; }
    public int AvailableSpots { get; set; }
    public int JoinedUsers { get; set; } 
    public string OrganiserId { get; set; } = string.Empty;
}
