using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.CityDataConstants;

namespace TheWhiskyRealm.Core.Models.AdminArea.City;

public class CityFormViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(CityMaxNameLength, MinimumLength = CityMinNameLength, ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;
    public int? CountryId { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(CityMaxZipCodeLength, MinimumLength = CityMinZipCodeLength, ErrorMessage = LengthMessage)]
    public string Zip { get; set; } = string.Empty;

    public ICollection<CountryViewModel> Countries { get; set; } = new List<CountryViewModel>();
}
