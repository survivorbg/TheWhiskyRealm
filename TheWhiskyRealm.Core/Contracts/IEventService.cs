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
}
