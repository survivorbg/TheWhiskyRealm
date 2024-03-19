using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static TheWhiskyRealm.Infrastructure.Constants.EventDataConstants;

namespace TheWhiskyRealm.Infrastructure.Data.Models;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(MaxTitleLength)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(MaxDescLength)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string OrganiserId { get; set; } = null!;

    [ForeignKey(nameof(OrganiserId))]
    public IdentityUser Organiser { get; set; } = null!;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    [Range(MinDuration,MaxDuration)]
    public int DurationInHours { get; set; }

    [Range(MinPrice,MaxPrice)]
    public decimal? Price { get; set; }

    [Required]
    public int VenueId { get; set; }

    [ForeignKey(nameof(VenueId))]
    public Venue Venue { get; set; } = null!;
}
