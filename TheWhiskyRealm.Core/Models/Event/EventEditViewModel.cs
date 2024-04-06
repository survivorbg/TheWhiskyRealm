using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Core.Models.Venue;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.EventDataConstants;

namespace TheWhiskyRealm.Core.Models.Event;

public class EventEditViewModel
{
    [Required(ErrorMessage = RequiredMessage)]
    public int Id { get; set; }

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(EventMaxTitleLength, MinimumLength = EventMinTitleLength, ErrorMessage = LengthMessage)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(EventMaxDescLength, MinimumLength = EventMinDescLength, ErrorMessage = LengthMessage)]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Start Date")]
    [Required(ErrorMessage = RequiredMessage)]
    public string StartDate { get; set; } = string.Empty;

    [Display(Name = "End Date")]
    [Required(ErrorMessage = RequiredMessage)]
    public string EndDate { get; set; } = string.Empty;

    
    [Range(EventMinPrice, EventMaxPrice, ErrorMessage = ValueMessage)]
    public decimal? Price { get; set; }

    [Required]
    public int VenueId { get; set; }

    public ICollection<VenueViewModel> Venues { get; set; } = new List<VenueViewModel>();
}
