using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.CountryDataConstants;
using static TheWhiskyRealm.Core.Constants.MessageConstants;

namespace TheWhiskyRealm.Core.Models.AdminArea.Country;

public class CountryIndexViewModel
{
    public IEnumerable<CountryViewModel> Countries { get; set; } = new List<CountryViewModel>();
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(CountryMaxNameLength, MinimumLength = CountryMinNameLength, ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 10;
}
