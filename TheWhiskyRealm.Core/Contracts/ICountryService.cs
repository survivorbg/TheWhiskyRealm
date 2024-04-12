using TheWhiskyRealm.Core.Models.AdminArea.Country;

namespace TheWhiskyRealm.Core.Contracts;

public interface ICountryService
{
    public Task<ICollection<CountryViewModel>> GetAllCountriesAsync(int currentPage, int pageSize);
    public Task<ICollection<CountryViewModel>> GetAllCountriesAsync();
    public Task<bool> CountryWithNameExistsAsync(string countryName,int id = 0);
    public Task<bool> CountryExistsAsync(int id);
    Task AddCountryAsync(string name);
    public Task<int> GetTotalCountriesAsync();
    Task<CountryViewModel?> GetByIdAsync(int id);
    Task EditAsync(CountryViewModel model);
}
