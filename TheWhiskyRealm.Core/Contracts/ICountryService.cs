using TheWhiskyRealm.Core.Models.AdminArea.Country;

namespace TheWhiskyRealm.Core.Contracts;

public interface ICountryService
{
    public Task<ICollection<CountryViewModel>> GetAllCountriesAsync(int currentPage, int pageSize);
    public Task<bool> CountryWithNameExistsAsync(string countryName);
    Task AddCountryAsync(string name);
    public Task<int> GetTotalCountriesAsync();
    Task<CountryViewModel?> GetByIdAsync(int id);
    Task EditAsync(CountryViewModel model);
}
