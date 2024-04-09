using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.RegionDataConstants;

namespace TheWhiskyRealm.Core.Models.AdminArea.Region;

public class AddRegionViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(RegionMaxNameLength, MinimumLength = RegionMinNameLength, ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;
    public int? CountryId { get; set; }
    public ICollection<CountryViewModel> Countries { get; set; } = new List<CountryViewModel>();
}

