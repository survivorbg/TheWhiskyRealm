using System.ComponentModel.DataAnnotations;
using static TheWhiskyRealm.Infrastructure.Constants.WhiskyTypeDataConstants;
using static TheWhiskyRealm.Core.Constants.MessageConstants;

namespace TheWhiskyRealm.Core.Models.Whisky.Add;

public class WhiskyTypeViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(WhiskyTypeMaxNameLength,
        MinimumLength = WhiskyTypeMinNameLength,
        ErrorMessage = LengthMessage)]
    public string Name { get; set; } = string.Empty;
}