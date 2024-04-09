using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
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

    public async Task<bool> CountryExistsAsync(int id)
    {
        return await repo
          .AllReadOnly<Country>()
          .AnyAsync(c => c.Id == id);
    }

    public async Task<bool> CountryWithNameExistsAsync(string countryName)
    {
        return await repo
            .AllReadOnly<Country>()
            .AnyAsync(c => c.Name.ToLower() == countryName.ToLower());
    }

    public async Task EditAsync(CountryViewModel model)
    {
        var country = await repo.GetByIdAsync<Country>(model.Id);
        if(country != null)
        {
            country.Name = model.Name;
            await repo.SaveChangesAsync(); //TODO Check for missed async/await
        };

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

    public async Task<CountryViewModel?> GetByIdAsync(int id)
    {
        var country = await repo.GetByIdAsync<Country>(id);
        if(country != null)
        {
            var model = new CountryViewModel
            {
                Id = country.Id,
                Name = country.Name,
            };
            return model;
        }
        return null;

    }

    public async Task<int> GetTotalCountriesAsync()
    {
        return await repo
            .AllReadOnly<Country>()
            .CountAsync();
    }

}
