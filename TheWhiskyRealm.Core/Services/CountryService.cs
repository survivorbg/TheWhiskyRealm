using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class CountryService : ICountryService
{
    private readonly IRepository repo;

    public CountryService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<ICollection<CountryViewModel>> GetAllCountriesAsync()
    {
        return await repo
            .AllReadOnly<Country>()
            .Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync();
    }
}
