﻿using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;
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

    public async Task AddEventAsync(EventAddViewModel model, string userId)
    {
        var venue = await repo.GetByIdAsync<Venue>(model.VenueId);

        var ev = new Event
        {
            Description = model.Description,
            EndDate = model.EndDate,
            OrganiserId = userId,
            Price = model.Price,
            StartDate = model.StartDate,
            VenueId = model.VenueId,
            Title = model.Title,
            AvailableSpots = venue!.Capacity
        };
        await repo.AddAsync(ev);
        await repo.SaveChangesAsync();
    }

    public async Task DeleteEventAsync(int id)
    {
        var ev = repo.GetByIdAsync<Event>(id);
        if (ev != null)
        {
            var userEvents = await repo
                .All<UserEvent>()
                .Where(uv => uv.EventId == id)
                .ToListAsync();

            repo.DeleteRange(userEvents);
            await repo.DeleteById<Event>(id);
        }

        await repo.SaveChangesAsync();
    }

    public async Task EditEventAsync(EventEditViewModel model, int availableSpots)
    {
        var ev = await repo.GetByIdAsync<Event>(model.Id);

        if (ev != null)
        {
            ev.Price = model.Price;
            ev.Description = model.Description;
            ev.Title = model.Title;
            ev.VenueId = model.VenueId;
            ev.StartDate = model.StartDate;
            ev.EndDate = model.EndDate;
            ev.AvailableSpots = availableSpots;
        }
        await repo.SaveChangesAsync();
    }

    public async Task<bool> EventExistAsync(int id)
    {
        return await repo
            .AllReadOnly<Event>()
            .AnyAsync(e => e.Id == id);
    }

    public async Task<ICollection<AllEventViewModel>> GetAllEventsAsync()
    {
        return await repo
            .AllReadOnly<Event>()
            .Where(e => e.StartDate > DateTime.Now)
            .OrderBy(e => e.StartDate)
            .Select(e => new AllEventViewModel
            {
                Id = e.Id,
                Price = e.Price,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Title = e.Title,
                VenueName = e.Venue.Name,
                AvailableSpots = e.AvailableSpots
            })
            .ToListAsync();
    }

    public async Task<ICollection<EventViewModel>> GetAllEventsInVenueAsync(int venueId)
    {
        return await repo
           .AllReadOnly<Event>()
           .Where(e=>e.VenueId == venueId)
           .Where(e => e.StartDate > DateTime.Now)
           .Select(e => new EventViewModel
           {
               Id = e.Id,
               Price = e.Price,
               StartDate = e.StartDate,
               Title = e.Title,
               AvailableSpots = e.AvailableSpots,
               EndDate = e.EndDate,
               OrganiserId = e.OrganiserId,
               JoinedUsers = e.UsersEvents.Count()
           })
           .ToListAsync();
    }

    public async Task<ICollection<AllEventViewModel>> GetAllPastEventsAsync()
    {
        return await repo
            .AllReadOnly<Event>()
            .Where(e => e.StartDate < DateTime.Now)
            .Select(e => new AllEventViewModel
            {
                Id = e.Id,
                Price = e.Price,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Title = e.Title,
                VenueName = e.Venue.Name,
                AvailableSpots = e.AvailableSpots
            })
            .ToListAsync();
    }

    public async Task<ICollection<EventViewModel>> GetAllPastEventsInVenueAsync(int venueId)
    {
        return await repo
            .AllReadOnly<Event>()
            .Where(e=>e.VenueId == venueId)
            .Where(e => e.StartDate < DateTime.Now)
            .Select(e => new EventViewModel
            {
                Id = e.Id,
                Price = e.Price,
                StartDate = e.StartDate,
                Title = e.Title,
                AvailableSpots = e.AvailableSpots,
                EndDate = e.EndDate,
                OrganiserId = e.OrganiserId,
                JoinedUsers = e.UsersEvents.Count()
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
                AvailableSpots = e.AvailableSpots,
                Description = e.Description,
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
                EndDate = e.EndDate,
                StartDate = e.StartDate,
                Id = e.Id,
                Price = e.Price,
                Title = e.Title,
                VenueId = e.Venue.Id,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<EventDetailsViewModel>> GetEventsOrganisedByUserAsync(string organiserId)
    {
        return await repo
            .AllReadOnly<Event>()
            .Where(e => e.OrganiserId == organiserId)
            .OrderByDescending(e => e.StartDate)
            .Select(e => new EventDetailsViewModel
            {
                Id = e.Id,
                Price = e.Price,
                AvailableSpots = e.AvailableSpots,
                Description = e.Description,
                EndDate = e.EndDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                OrganiserName = e.Organiser.UserName,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Title = e.Title,
                VenueName = e.Venue.Name
            })
            .ToListAsync();
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

    public async Task<ICollection<EventDetailsViewModel>> GetUserEventsAsync(string userId)
    {
        return await repo
            .AllReadOnly<UserEvent>()
            .OrderByDescending(ue => ue.Event.StartDate)
            .Where(ue => ue.UserId == userId)
            .Select(ue => new EventDetailsViewModel
            {
                Id = ue.Event.Id,
                Price = ue.Event.Price,
                AvailableSpots = ue.Event.AvailableSpots,
                Description = ue.Event.Description,
                EndDate = ue.Event.EndDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                OrganiserName = ue.Event.Organiser.UserName,
                StartDate = ue.Event.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Title = ue.Event.Title,
                VenueName = ue.Event.Venue.Name
            })
            .ToListAsync();
    }

    public async Task<bool> HasAlreadyStartedAsync(int id)
    {
        var ev = await repo.GetByIdAsync<Event>(id);
        if (ev == null)
        {
            return true;
        }

        return DateTime.Now >= ev.StartDate.AddHours(-2) ? true : false;
    }

    public async Task<bool> HasAvaialbleSpotsAsync(int id)
    {
        var ev = await repo.GetByIdAsync<Event>(id);
        if (ev == null)
        {
            return false;
        }
        return ev.AvailableSpots > 0 ? true : false;
    }

    public Task<bool> IsUserAlreadyJoinedAsync(int id, string userId)
    {
        return repo
            .AllReadOnly<UserEvent>()
            .AnyAsync(ue => ue.UserId == userId && id == ue.EventId);
    }

    public async Task JoinEventAsync(int id, string userId)
    {
        var ev = await repo.GetByIdAsync<Event>(id);
        if (ev != null && ev.AvailableSpots > 0)
        {
            var userEvent = new UserEvent
            {
                UserId = userId,
                EventId = id
            };

            await repo.AddAsync(userEvent);
            ev.AvailableSpots -= 1;
        }

        await repo.SaveChangesAsync();
    }

    public async Task LeaveEventAsync(int id, string userId)
    {
        var ue = await repo
            .All<UserEvent>()
            .FirstOrDefaultAsync(ue => ue.EventId == id && userId == ue.UserId);

        if (ue != null)
        {
            repo.Delete(ue);
        }

        var ev = await repo.GetByIdAsync<Event>(id);
        if (ev != null)
        {
            ev.AvailableSpots += 1;
        }

        await repo.SaveChangesAsync();
    }
    public async Task<ICollection<string>> GetJoinedUsersAsync(int eventId)
    {
        return await repo
            .AllReadOnly<UserEvent>()
            .Where(ue => ue.EventId == eventId)
            .Select(ue => ue.User.UserName)
            .ToListAsync();
    }

    public async Task<int> GetJoinedUsersCountAsync(int eventId)
    {
        return await repo
            .AllReadOnly<UserEvent>()
            .Where(ue => ue.EventId == eventId)
            .CountAsync();
    }
}
