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

    public async Task EditEventAsync(EventEditViewModel model, DateTime startDate, DateTime endDate)
    {
        Event ev = await repo.GetByIdAsync<Event>(model.Id);
        if (ev != null)
        {
            ev.Price = model.Price;
            ev.Description = model.Description;
            ev.DurationInHours = model.DurationInHours;
            ev.Title = model.Title;
            ev.VenueId = model.VenueId;
            ev.StartDate = startDate;
            ev.EndDate = endDate;
        }
        await repo.SaveChangesAsync();
    }

    public async Task<bool> EventExistAsync(int id)
    {
        return await repo
            .AllReadOnly<Event>()
            .AnyAsync(e=>e.Id == id);
    }

    public async Task<ICollection<AllEventViewModel>> GetAllEventsAsync()
    {
        return await repo
            .AllReadOnly<Event>()
            .Where(e => e.StartDate > DateTime.UtcNow)
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
        return await repo
            .AllReadOnly<Event>()
            .Where(e => e.Id == id)
            .Select(e => new EventDetailsViewModel
            {
                Id = e.Id,
                Price = e.Price,
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

    public Task<EventEditViewModel?> GetEventForEditAsync(int id)
    {
        return repo
            .All<Event>()
            .Where(e => e.Id == id)
            .Select(e => new EventEditViewModel()
            {
                Description = e.Description,
                DurationInHours = e.DurationInHours,
                EndDate = e.EndDate.ToString("hh:mm dd.MM.yyyy"),
                StartDate = e.StartDate.ToString("hh:mm dd.MM.yyyy"),
                Id = e.Id,
                Price = e.Price,
                Title = e.Title,
                VenueId = e.Venue.Id,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<string> GetOrganiserIdAsync(int id)
    {
        var ev = await repo.GetByIdAsync<Event>(id);
        if (ev == null)
        {
            return string.Empty;
        }
        return ev.OrganiserId;
    }
}
