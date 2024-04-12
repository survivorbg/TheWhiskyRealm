
using TheWhiskyRealm.Core.Models.AdminArea.City;

namespace TheWhiskyRealm.Core.Contracts;

public interface ICityService
{
    Task<int> AddCityAsync(string name, int countryId, string zip);
    Task<bool> CityWithThisNameAndCountryExistsAsync(string name, int countryId, int cityId = 0);
    Task<IEnumerable<CityViewModel>> GetAllCitiesAsync(int currentPage, int pageSize);
    Task<int> GetTotalCitiesAsync();
}
