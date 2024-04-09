using static TheWhiskyRealm.Infrastructure.Constants.CountryDataConstants;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using System.ComponentModel.DataAnnotations;

namespace TheWhiskyRealm.Core.Models.AdminArea.Country;

public class CountryViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(CountryMaxNameLength, MinimumLength = CountryMinNameLength, ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;
}

   