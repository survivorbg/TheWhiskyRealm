using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.WhiskyDataConstants;

namespace TheWhiskyRealm.Core.Models.Whisky.Add;

public class AddWhiskyViewModel
{
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(WhiskyMaxNameLength, MinimumLength = WhiskyMinNameLength, ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;

    [Range(WhiskyMinAge, WhiskyMaxAge, ErrorMessage = ValueMessage)]
    public int? Age { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [Range(WhiskyMinABV, WhiskyMaxABV, ErrorMessage = ValueMessage)]
    [Display(Name = "Alcohol Percentage")]
    public double AlcoholPercentage { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(WhiskyMaxDescLength, MinimumLength = WhiskyMinDescLength, ErrorMessage = LengthMessage)]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Distillery")]
    [Required(ErrorMessage = RequiredMessage)]
    public int DistilleryId { get; set; }
    public IEnumerable<DistilleryAddWhiskyViewModel> Distilleries { get; set; } = Enumerable.Empty<DistilleryAddWhiskyViewModel>();

    [Display(Name = "Whisky Category")]
    [Required(ErrorMessage = RequiredMessage)]
    public int WhiskyTypeId { get; set; }
    public IEnumerable<WhiskyTypeViewModel> WhiskyTypes { get; set; } = Enumerable.Empty<WhiskyTypeViewModel>();
}
