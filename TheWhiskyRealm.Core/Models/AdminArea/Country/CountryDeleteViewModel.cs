using TheWhiskyRealm.Core.Models.AdminArea.Region;

namespace TheWhiskyRealm.Core.Models.AdminArea.Country;

public class CountryInfoViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<RegionCountryViewModel> Regions { get; set; } = new List<RegionCountryViewModel>();
}
