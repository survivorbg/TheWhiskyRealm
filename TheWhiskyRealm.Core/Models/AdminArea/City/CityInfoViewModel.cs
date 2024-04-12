using TheWhiskyRealm.Core.Models.AdminArea.Venue;

namespace TheWhiskyRealm.Core.Models.AdminArea.City;

public class CityInfoViewModel : CityFormViewModel
{
    public ICollection<VenueViewModel> Venues { get; set; } = new List<VenueViewModel>();
}
