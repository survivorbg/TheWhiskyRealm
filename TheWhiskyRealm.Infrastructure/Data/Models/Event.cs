using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.EventDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

/// <summary>
/// Represents an event entity.
/// </summary>
[Comment("Represents an event entity.")]
public class Event
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    [Key]
    [Comment("The unique identifier of the event.")]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    [Required]
    [StringLength(EventMaxTitleLength)]
    [Comment("The title of the event.")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    [Required]
    [StringLength(EventMaxDescLength)]
    [Comment("The description of the event.")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the identifier of the user who organised the event.
    /// </summary>
    [Required]
    [Comment("The identifier of the user who organised the event.")]
    public string OrganiserId { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user who organised the event.
    /// </summary>
    [ForeignKey(nameof(OrganiserId))]
    [Comment("The user who organised the event.")]
    public ApplicationUser Organiser { get; set; } = null!;
            
    /// <summary>
    /// Gets or sets the start date of the event.
    /// </summary>
    [Required]
    [Comment("The start date of the event.")]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the event.
    /// </summary>
    [Required]
    [Comment("The end date of the event.")]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the duration of the event in hours.
    /// </summary>
    [Required]
    [Range(EventMinDuration, EventMaxDuration)]
    [Comment("The duration of the event in hours.")]
    public int DurationInHours { get; set; }

    /// <summary>
    /// Gets or sets the price of the event.
    /// </summary>
    [Range(EventMinPrice, EventMaxPrice)]
    [Comment("The price of the event.")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the venue where the event will take place.
    /// </summary>
    [Required]
    [Comment("The identifier of the venue where the event will take place.")]
    public int VenueId { get; set; }

    /// <summary>
    /// Gets or sets the venue where the event will take place.
    /// </summary>
    [ForeignKey(nameof(VenueId))]
    [Comment("The venue where the event will take place.")]
    public Venue Venue { get; set; } = null!;

    /// <summary>
    /// Gets or sets the users' events associated with the event.
    /// </summary>
    [Comment("The users' events associated with the event.")]
    public ICollection<UserEvent> UsersEvents { get; set; } = new List<UserEvent>();
}
