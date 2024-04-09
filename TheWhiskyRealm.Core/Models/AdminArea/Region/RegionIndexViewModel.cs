using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.CountryDataConstants;

namespace TheWhiskyRealm.Core.Models.AdminArea.Region;

public class RegionIndexViewModel
{
    public IEnumerable<RegionViewModel> Regions { get; set; } = new List<RegionViewModel>();
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(CountryMaxNameLength, MinimumLength = CountryMinNameLength, ErrorMessage = LengthMessage)]
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 15;
}
