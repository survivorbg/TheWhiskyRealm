using TheWhiskyRealm.Core.Models.Event;

namespace TheWhiskyRealm.Core.Contracts;

public interface IVenueService
{
    Task<ICollection<VenueViewModel>> GetVenuesAsync();
}
