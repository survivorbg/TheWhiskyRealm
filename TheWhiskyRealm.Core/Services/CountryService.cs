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

    public async Task AddCountryAsync(string name)
    {
        if (name != null)
        {
            var country = new Country { Name = name };
            await repo.AddAsync(country);
            await repo.SaveChangesAsync();
        }
    }

    public async Task<bool> CountryWithNameExistsAsync(string countryName)
    {
        return await repo
            .AllReadOnly<Country>()
            .AnyAsync(c => c.Name.ToLower() == countryName.ToLower());
    }

    public async Task<ICollection<CountryViewModel>> GetAllCountriesAsync(int currentPage, int pageSize)
    {
        return await repo
            .AllReadOnly<Country>()
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync();
    }

    public async Task<int> GetTotalCountriesAsync()
    {
        return await repo
            .AllReadOnly<Country>()
            .CountAsync();
    }

}
