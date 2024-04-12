using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Core.Models.AdminArea.City;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.VenueDataConstants;

namespace TheWhiskyRealm.Core.Models.AdminArea.Venue;

public class VenueFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(VenueMaxNameLength, MinimumLength = VenueMinNameLength, ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [Range(VenueMinCapacity, VenueMaxCapacity, ErrorMessage = ValueMessage)]
    public int Capacity { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    public int CityId { get; set; }

    public IEnumerable<CityViewModel> Cities { get; set; } = new List<CityViewModel>();

}
