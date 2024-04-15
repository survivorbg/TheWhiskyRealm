using System.ComponentModel.DataAnnotations;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;
using static TheWhiskyRealm.Core.Constants.MessageConstants;
using static TheWhiskyRealm.Infrastructure.Constants.EventDataConstants;

namespace TheWhiskyRealm.Core.Models.Event;

public class EventAddViewModel
{
    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(EventMaxTitleLength, MinimumLength = EventMinTitleLength, ErrorMessage = LengthMessage)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = RequiredMessage)]
    [StringLength(EventMaxDescLength, MinimumLength = EventMinDescLength, ErrorMessage = LengthMessage)]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Start Date")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = RequiredMessage)]
    public DateTime StartDate { get; set; }

    [Display(Name = "End Date")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = RequiredMessage)]
    public DateTime EndDate { get; set; }


    [Range(EventMinPrice, EventMaxPrice, ErrorMessage = ValueMessage)]
    public decimal? Price { get; set; }

    [Display(Name = "Venue")]
    [Required]
    public int VenueId { get; set; }

    public ICollection<VenueViewModel> Venues { get; set; } = new List<VenueViewModel>();
}
