using TheWhiskyRealm.Core.Models.Event;

namespace TheWhiskyRealm.Core.Contracts;

public interface IEventService
{
    Task<ICollection<AllEventViewModel>> GetAllEventsAsync();
    Task<ICollection<AllEventViewModel>> GetAllPastEventsAsync();
}
