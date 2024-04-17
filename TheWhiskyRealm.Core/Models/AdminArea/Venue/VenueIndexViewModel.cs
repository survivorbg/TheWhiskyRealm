namespace TheWhiskyRealm.Core.Models.AdminArea.Venue;

public class VenueIndexViewModel
{
    public IEnumerable<VenueViewModel> Venues { get; set; } = new List<VenueViewModel>();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 10;
}
