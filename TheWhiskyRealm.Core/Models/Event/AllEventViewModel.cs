namespace TheWhiskyRealm.Core.Models.Event;

public class AllEventViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public string VenueName { get; set; } = string.Empty;
    public int AvailableSpots { get; set; }
}
