using TheWhiskyRealm.Core.Models.AdminArea.Venue;
using TheWhiskyRealm.Core.Models.Event;

namespace TheWhiskyRealm.Core.Contracts;

public interface IEventService
{
    Task<ICollection<AllEventViewModel>> GetAllEventsAsync();
    Task<ICollection<EventViewModel>> GetAllEventsInVenueAsync(int venueId);
    Task<ICollection<AllEventViewModel>> GetAllPastEventsAsync();
    Task<ICollection<EventViewModel>> GetAllPastEventsInVenueAsync(int venueId);
    Task<EventDetailsViewModel?> GetEventAsync(int id);
    Task<string> GetOrganiserIdAsync(int id);
    Task<EventEditViewModel?> GetEventForEditAsync(int id);
    Task<bool> EventExistAsync(int id);
    Task EditEventAsync(EventEditViewModel model);
    Task AddEventAsync(EventAddViewModel model, string userId);
    Task<bool> HasAvaialbleSpotsAsync(int id);
    Task<bool> IsUserAlreadyJoinedAsync(int id, string userId);
    Task JoinEventAsync(int id, string userId);
    Task<bool> HasAlreadyStartedAsync(int id);
    Task LeaveEventAsync(int id, string userId);
    Task<ICollection<EventDetailsViewModel>> GetUserEventsAsync(string userId);
    Task<ICollection<string>> GetJoinedUsersAsync(int eventId);
    Task<ICollection<EventDetailsViewModel>> GetEventsOrganisedByUserAsync(string organiserId);
    Task DeleteEventAsync(int id);
}
