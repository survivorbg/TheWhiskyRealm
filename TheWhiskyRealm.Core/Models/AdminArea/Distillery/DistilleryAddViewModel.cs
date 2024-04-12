using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.DistilleryDataConstants;

namespace TheWhiskyRealm.Core.Models.AdminArea.Distillery;

public class DistilleryAddViewModel
{
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(DistilleryMaxNameLength,MinimumLength = DistilleryMinNameLength, ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [Range(DistilleryMinYearFounded, DistilleryMaxYearFounded, ErrorMessage = ValueMessage)]
    [Display(Name="Year Founded")]
    public int YearFounded { get; set; }

    [Display(Name = "Image URL")]
    public string? ImageUrl { get; set; }
    public int RegionId { get; set; }
    public ICollection<RegionViewModel> Regions { get; set; } = new List<RegionViewModel>();

}

