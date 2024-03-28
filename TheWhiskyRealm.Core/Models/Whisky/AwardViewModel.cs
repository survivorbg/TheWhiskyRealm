using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.AwardDataConstants;
using static TheWhiskyRealm.Core.Constants.MessageConstants;

namespace TheWhiskyRealm.Core.Models.Whisky;

public class AwardViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(AwardMaxTitleLength, MinimumLength = AwardMinTitleLength, ErrorMessage = LengthMessage)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(AwardMaxDescLength, MinimumLength = AwardMinDescLength, ErrorMessage = LengthMessage)]
    public string AwardsCeremony { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    public string MedalType { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [Range(AwardMinYearValue, AwardMaxYearValue, ErrorMessage = ValueMessage)]
    public int Year { get; set; }

    public int WhiskyId { get; set; }

    public IEnumerable<string> MedalTypeOptions { get; set; } = Enumerable.Empty<string>();

}
