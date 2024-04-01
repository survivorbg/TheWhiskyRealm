using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Event;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class EventService : IEventService
{
    private readonly IRepository repo;

    public EventService(IRepository repo)
    {
        this.repo = repo;
    }

    public async Task<ICollection<AllEventViewModel>> GetAllEventsAsync()
    {
        return await repo
            .AllReadOnly<Event>()
            .Where(e=> e.StartDate > DateTime.UtcNow)
            .Select(e=>new AllEventViewModel
            {
                Id = e.Id,
                Price = e.Price,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Title = e.Title,
                VenueName = e.Venue.Name,
                AvailableSpots = e.Venue.Capacity
            })
            .ToListAsync(); 
    }

    public async Task<ICollection<AllEventViewModel>> GetAllPastEventsAsync()
    {
        return await repo
            .AllReadOnly<Event>()
            .Where(e => e.StartDate < DateTime.UtcNow)
            .Select(e => new AllEventViewModel
            {
                Id = e.Id,
                Price = e.Price,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Title = e.Title,
                VenueName = e.Venue.Name,
                AvailableSpots = e.Venue.Capacity
            })
            .ToListAsync();
    }

    public async Task<EventDetailsViewModel?> GetEventAsync(int id)
    {
        return  await repo
            .AllReadOnly<Event>()
            .Where(e => e.Id == id)
            .Select(e=>new EventDetailsViewModel
            {
                Id = e.Id,
                Price= e.Price,
                AvailableSpots = e.Venue.Capacity,
                Description = e.Description,
                DurationInHours = e.DurationInHours,
                EndDate = e.EndDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                OrganiserName = e.Organiser.UserName,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Title = e.Title,
                VenueName = e.Venue.Name
            })
            .FirstOrDefaultAsync(); 
    }
}
