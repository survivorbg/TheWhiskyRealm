using System.Xml.Linq;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Contracts;

public interface IVenueService
{
    Task<int> AddVenueAsync(VenueFormViewModel model);
    Task<int> GetTotalVenuesAsync();
    Task<ICollection<VenueViewModel>> GetVenuesAsync();
    Task<IEnumerable<VenueViewModel>> GetVenuesAsync(int currentPage, int pageSize);
    Task<ICollection<VenueViewModel>> GetVenuesByCityAsync(int cityId);
    Task<bool> VenueExistAsync(int id);
    Task<bool> VenueExistByNameAsync(string name, int cityId, int venueId = 0); 
}
