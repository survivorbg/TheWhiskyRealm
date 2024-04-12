using TheWhiskyRealm.Core.Models.Venue;

namespace TheWhiskyRealm.Core.Contracts;

public interface IVenueService
{
    Task<ICollection<VenueViewModel>> GetVenuesAsync();
    Task<ICollection<VenueViewModel>> GetVenuesByCityAsync(int cityId);
    Task<bool> VenueExistAsync(int id);
}
