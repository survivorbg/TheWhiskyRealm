using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Venue;
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

    public async Task<bool> VenueExistAsync(int id)
    {
        return await repo
            .AllReadOnly<Venue>()
            .AnyAsync(v => v.Id == id);
    }
}
