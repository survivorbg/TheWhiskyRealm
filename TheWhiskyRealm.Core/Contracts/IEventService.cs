using TheWhiskyRealm.Core.Models.Event;

namespace TheWhiskyRealm.Core.Contracts;

public interface IEventService
{
    Task<ICollection<AllEventViewModel>> GetAllEventsAsync();
    Task<ICollection<AllEventViewModel>> GetAllPastEventsAsync();
    Task<EventDetailsViewModel?> GetEventAsync(int id);
    Task<string> GetOrganiserIdAsync(int id);
    Task<EventEditViewModel?> GetEventForEditAsync(int id);
    Task<bool> EventExistAsync(int id);
    Task EditEventAsync(EventEditViewModel model,DateTime startDate, DateTime endDate);
    Task AddEventAsync(EventAddViewModel model, DateTime startDate, DateTime endDate, string userId);
    Task<bool> HasAvaialbleSpotsAsync(int id);
    Task<bool> IsUserAlreadyJoinedAsync(int id, string userId);
    Task JoinEventAsync(int id, string userId);
    Task<bool> HasAlreadyStartedAsync(int id);
    Task LeaveEventAsync(int id, string userId);
    Task<ICollection<EventDetailsViewModel>> GetUserEventsAsync(string userId);
    Task<ICollection<EventDetailsViewModel>> GetEventsOrganisedByUserAsync(string organiserId);
    Task DeleteEventAsync(int id);
}
