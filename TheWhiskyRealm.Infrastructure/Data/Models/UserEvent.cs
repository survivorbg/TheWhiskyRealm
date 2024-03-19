using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class UserEvent
{
    public string UserId { get; set; } = null!;
    [ForeignKey(nameof(UserId))]
    public IdentityUser User { get; set; } = null!;
    public int EventId { get; set; }
    [ForeignKey(nameof(EventId))]
    public Event Event { get; set; } = null!;
}
