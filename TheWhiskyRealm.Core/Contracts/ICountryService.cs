using TheWhiskyRealm.Core.Models.AdminArea;

namespace TheWhiskyRealm.Core.Contracts;

public interface ICountryService
{
    public Task<ICollection<CountryViewModel>> GetAllCountriesAsync();
}
