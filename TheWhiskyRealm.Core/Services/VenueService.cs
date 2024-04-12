using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class VenueService : IVenueService
{
    private readonly IRepository repo;

    public VenueService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<int> AddVenueAsync(VenueFormViewModel model)
    {
        var venue = new Venue
        {
            Capacity = model.Capacity,
            CityId = model.CityId,
            Name = model.Name,
        };

        await repo.AddAsync(venue);
        await repo.SaveChangesAsync();
        return venue.Id;
    }

    public async Task EditVenueAsync(VenueFormViewModel model)
    {
        var venue = await repo.GetByIdAsync<Venue>(model.Id);
        if (venue != null)
        {
            venue.Name = model.Name;
            venue.CityId = model.CityId;
            venue.Capacity = model.Capacity;
            await repo.SaveChangesAsync();
        }
    }

    public async Task<int> GetTotalVenuesAsync()
    {
        return await repo
             .AllReadOnly<Venue>()
             .CountAsync();
    }

    public async Task<VenueFormViewModel?> GetVenueByIdAsync(int id)
    {
        return await repo
            .All<Venue>()
            .Where(v => v.Id == id)
            .Select(v => new VenueFormViewModel
            {
                Capacity = v.Capacity,
                CityId = v.CityId,
                Id = v.Id,
                Name = v.Name,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<VenueViewModel>> GetVenuesAsync()
    {
        return await repo
            .AllReadOnly<Venue>()
            .Select(v => new VenueViewModel
            {
                VenueId = v.Id,
                VenueName = v.Name,
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<VenueViewModel>> GetVenuesAsync(int currentPage, int pageSize)
    {
        return await repo
           .AllReadOnly<Venue>()
           .Skip((currentPage - 1) * pageSize)
           .Take(pageSize)
           .Select(v => new VenueViewModel
           {
               VenueId = v.Id,
               Capacity = v.Capacity,
               VenueName = v.Name
           })
           .ToListAsync();
    }

    public async Task<ICollection<VenueViewModel>> GetVenuesByCityAsync(int cityId)
    {
        return await repo
           .AllReadOnly<Venue>()
           .Where(v => v.CityId == cityId)
           .Select(v => new VenueViewModel
           {
               VenueId = v.Id,
               VenueName = v.Name,
               Capacity = v.Capacity,
           })
           .ToListAsync();
    }

    public async Task<bool> VenueExistAsync(int id)
    {
        return await repo
            .AllReadOnly<Venue>()
            .AnyAsync(v => v.Id == id);
    }

    public async Task<bool> VenueExistByNameAsync(string name, int cityId, int venueId = 0)
    {
        return await repo
            .AllReadOnly<Venue>()
            .AnyAsync(v => v.Name == name && v.CityId == cityId && v.Id != venueId);
    }
}