using System.ComponentModel.DataAnnotations.Schema;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class UserWhisky
{
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; } = null!;

    public int WhiskyId { get; set; }

    [ForeignKey(nameof(WhiskyId))]
    public Whisky Whisky { get; set; } = null!;
}
