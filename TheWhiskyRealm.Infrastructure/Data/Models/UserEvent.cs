using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents a mapping entity between a user and an event.
/// </summary>
[Comment("Represents a mapping entity between a user and an event.")]
public class UserEvent
{
    /// <summary>
    /// Gets or sets the identifier of the user associated with the event.
    /// </summary>
    [Comment("The identifier of the user associated with the event.")]
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user associated with the event.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    [Comment("The user associated with the event.")]
    public ApplicationUser User { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the event associated with the user.
    /// </summary>
    [Comment("The identifier of the event associated with the user.")]
    public int EventId { get; set; }

    /// <summary>
    /// Gets or sets the event associated with the user.
    /// </summary>
    [ForeignKey(nameof(EventId))]
    [Comment("The event associated with the user.")]
    public Event Event { get; set; } = null!;
}
