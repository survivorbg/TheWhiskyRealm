using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.City;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class CityService : ICityService
{
    private readonly IRepository repo;

    public CityService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<int> AddCityAsync(string name, int countryId, string zip)
    {
        var city = new City
        {
            Name = name,
            CountryId = countryId,
            ZipCode = zip
        };

        await repo.AddAsync(city);
        await repo.SaveChangesAsync();

        return city.Id;
    }

    public async Task<bool> CityWithThisNameAndCountryExistsAsync(string name, int countryId, int cityId = 0)
    {
        return await repo
            .AllReadOnly<City>()
            .AnyAsync(r => r.Name == name && r.CountryId == countryId && r.Id != cityId);
    }

    public async Task EditCityAsync(CityFormViewModel model)
    {
        var city = await repo.GetByIdAsync<City>(model.Id);
        if (city != null)
        {
            city.Name = model.Name;
            city.CountryId = (int)model.CountryId;
            city.ZipCode = model.Zip;
            await repo.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CityViewModel>> GetAllCitiesAsync(int currentPage, int pageSize)
    {
        return await repo
           .AllReadOnly<City>()
           .Skip((currentPage - 1) * pageSize)
           .Take(pageSize)
           .Select(c => new CityViewModel
           {
               Id = c.Id,
               Name = c.Name,
               Country = c.Country.Name,
               Zip = c.ZipCode,
           })
           .ToListAsync();
    }

    public async Task<CityFormViewModel?> GetCityByIdAsync(int id)
    {
        return await repo
            .All<City>()
            .Where(c => c.Id == id)
            .Select(c => new CityFormViewModel
            {
                Id = c.Id,
                CountryId = c.CountryId,
                Name = c.Name,
                Zip = c.ZipCode
            })
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetTotalCitiesAsync()
    {
        return await repo
            .AllReadOnly<City>()
            .CountAsync();
    }
}
