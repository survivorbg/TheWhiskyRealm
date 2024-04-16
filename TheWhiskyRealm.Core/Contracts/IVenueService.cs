using TheWhiskyRealm.Core.Models.AdminArea.Venue;

namespace TheWhiskyRealm.Core.Contracts;

public interface IVenueService
{
    Task<int> AddVenueAsync(VenueFormViewModel model);
    Task EditVenueAsync(VenueFormViewModel model);
    Task<int> GetTotalVenuesAsync();
    Task<VenueFormViewModel?> GetVenueByIdAsync(int id);
    Task<ICollection<VenueViewModel>> GetVenuesAsync();
    Task<IEnumerable<VenueViewModel>> GetVenuesAsync(int currentPage, int pageSize);
    Task<ICollection<VenueViewModel>> GetVenuesByCityAsync(int cityId);
    Task<ICollection<VenueViewModel>> GetVenuesWithMoreCapacityAsync(int capacity);
    Task<bool> VenueExistAsync(int id);
    Task<bool> VenueExistByNameAsync(string name, int cityId, int venueId = 0);
    Task<int> GetVenueCapacityAsync(int id);
}
