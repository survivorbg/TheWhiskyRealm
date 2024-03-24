using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.DistilleryDataConstants;
using static TheWhiskyRealm.Infrastructure.Constants.CountryDataConstants;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
namespace TheWhiskyRealm.Core.Models.Whisky.Add;

public class DistilleryAddWhiskyViewModel
{
    public int DistilleryId { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(DistilleryMaxNameLength, MinimumLength = DistilleryMinNameLength, ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(CountryMaxNameLength, MinimumLength = CountryMinNameLength, ErrorMessage = LengthMessage)]
    public string Country { get; set; } = string.Empty;
}
