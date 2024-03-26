using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.RatingDataConstants;

namespace TheWhiskyRealm.Core.Models.Rating;

public class RatingViewModel
{
    [Required(ErrorMessage = LengthMessage)]
    [Range(RatingMinNoseValue, RatingMaxNoseValue, ErrorMessage = ValueMessage)]
    public int Nose { get; set; }

    [Required(ErrorMessage = LengthMessage)]
    [Range(RatingMinTasteValue, RatingMaxTasteValue, ErrorMessage = ValueMessage)]
    public int Taste { get; set; }

    [Required(ErrorMessage = LengthMessage)]
    [Range(RatingMinFinishValue, RatingMaxFinishValue, ErrorMessage = ValueMessage)]
    public int Finish { get; set; }

    [Required]
    public int WhiskyId { get; set; }

}
