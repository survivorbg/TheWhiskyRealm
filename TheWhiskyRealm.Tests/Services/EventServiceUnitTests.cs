using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;
using TheWhiskyRealm.Core.Models.Event;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class EventServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private IEventService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);
        repository = new Repository(dbContext);
        service = new EventService(repository);


        var organiser = new ApplicationUser
        {
            Id = "TestUserId",
            UserName = "TestUser"
        };
        await dbContext.Users.AddAsync(organiser);
        var joined = new ApplicationUser
        {
            Id = "JoinedUserId",
            UserName = "JoinedUser"
        };
        await dbContext.Users.AddAsync(joined);
        var venue = new Venue
        {
            Id = 1,
            Name = "Test Venue",
            Capacity = 100,
            CityId = 1
        };
        await dbContext.Venues.AddAsync(venue);
        var ev = new Event
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            OrganiserId = organiser.Id,
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(3),
            AvailableSpots = venue.Capacity,
            Price = 100,
            VenueId = venue.Id
        };
        await dbContext.Events.AddAsync(ev);
        var ev2 = new Event
        {
            Id = 2,
            Title = "Test Event2",
            Description = "Test Description2",
            OrganiserId = organiser.Id,
            StartDate = DateTime.Now.AddDays(-3),
            EndDate = DateTime.Now.AddDays(-2),
            AvailableSpots = venue.Capacity,
            Price = 100,
            VenueId = venue.Id
        };
        await dbContext.Events.AddAsync(ev2);
        var ue = new UserEvent
        {
            UserId = joined.Id,
            EventId = ev.Id,
        };
        await dbContext.UsersEvents.AddAsync(ue);



        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddEventAsync_ShouldAddEventToDatabase()
    {
        // Arrange
        var model = new EventAddViewModel
        {
            Title = "New Event",
            Description = "New Description",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Price = 100,
            VenueId = 1
        };
        var userId = "TestUserId";

        // Act
        await service.AddEventAsync(model,userId);

        // Assert
        var addedEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Title == model.Title);
        Assert.IsNotNull(addedEvent);
        Assert.AreEqual(model.Title, addedEvent.Title);
        Assert.AreEqual(model.Description, addedEvent.Description);
        Assert.AreEqual(model.StartDate, addedEvent.StartDate);
        Assert.AreEqual(model.EndDate, addedEvent.EndDate);
        Assert.AreEqual(model.Price, addedEvent.Price);
        Assert.AreEqual(model.VenueId, addedEvent.VenueId);
        Assert.AreEqual(userId, addedEvent.OrganiserId);
    }

    [Test]
    public async Task DeleteEventAsync_ShouldDeleteEventFromDatabase()
    {
        // Arrange
        var eventId = 1;

        // Act
        await service.DeleteEventAsync(eventId);

        // Assert
        var deletedEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == eventId);
        Assert.IsNull(deletedEvent);
    }

    //TODO Add test when there are UserEvents with that eventId

    //[Test]
    //public async Task EditEventAsync_ShouldEditEventInDatabase()
    //{
    //    // Arrange
    //    var model = new EventEditViewModel
    //    {
    //        Id = 1,
    //        Title = "Edited Test Event",
    //        Description = "Edited Test Description",
    //        StartDate = DateTime.Now.AddDays(1),
    //        EndDate = DateTime.Now.AddDays(2),
    //        Price = 200,
    //        VenueId = 1
    //    };

    //    // Act
    //    await service.EditEventAsync(model);

    //    // Assert
    //    var editedEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == model.Id);
    //    Assert.IsNotNull(editedEvent);
    //    Assert.AreEqual(model.Title, editedEvent.Title);
    //    Assert.AreEqual(model.Description, editedEvent.Description);
    //    Assert.AreEqual(model.StartDate, editedEvent.StartDate);
    //    Assert.AreEqual(model.EndDate, editedEvent.EndDate);
    //    Assert.AreEqual(model.Price, editedEvent.Price);
    //    Assert.AreEqual(model.VenueId, editedEvent.VenueId);
    //}
    [Test]
    public async Task EventExistAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingEventId = 1;

        // Act
        var eventExists = await service.EventExistAsync(existingEventId);

        // Assert
        Assert.IsTrue(eventExists);
    }

    [Test]
    public async Task EventExistAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingEventId = 999;

        // Act
        var eventExists = await service.EventExistAsync(nonExistingEventId);

        // Assert
        Assert.IsFalse(eventExists);
    }

    [Test]
    public async Task GetAllEventsAsync_ShouldReturnAllEvents()
    {
        // Arrange
        var actualEvents = dbContext.Events
            .Where(e => e.StartDate > DateTime.Now)
            .OrderBy(e => e.StartDate)
            .Select(e => new AllEventViewModel
        {
            AvailableSpots = e.AvailableSpots,
            Id = e.Id,
            Price = e.Price,
            StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
            Title = e.Title,
            VenueName = e.Venue.Name
        }).ToList();

        // Act
        var allEvents = await service.GetAllEventsAsync();

        // Assert
        Assert.AreEqual(actualEvents.Count, allEvents.Count);
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Id), allEvents.Select(e => e.Id));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Title), allEvents.Select(e => e.Title));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.StartDate), allEvents.Select(e => e.StartDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Price), allEvents.Select(e => e.Price));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.VenueName), allEvents.Select(e => e.VenueName));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.AvailableSpots), allEvents.Select(e => e.AvailableSpots));
    }
    [Test]
    public async Task GetAllEventsInVenueAsync_ShouldReturnAllEventsInVenue()
    {
        // Arrange
        var venueId = 1;
        var actualEvents = dbContext.Events
            .Where(e => e.VenueId == venueId)
            .Where(e => e.StartDate > DateTime.Now)
            .Select(e => new EventViewModel
            {
                Id = e.Id,
                Title = e.Title,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Price = e.Price,
                AvailableSpots = e.AvailableSpots,
                OrganiserId = e.OrganiserId,
                JoinedUsers = e.UsersEvents.Count()
            })
            .ToList();

        // Act
        var allEventsInVenue = await service.GetAllEventsInVenueAsync(venueId);

        // Assert
        Assert.AreEqual(actualEvents.Count, allEventsInVenue.Count);
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Id), allEventsInVenue.Select(e => e.Id));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Title), allEventsInVenue.Select(e => e.Title));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.StartDate), allEventsInVenue.Select(e => e.StartDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.EndDate), allEventsInVenue.Select(e => e.EndDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Price), allEventsInVenue.Select(e => e.Price));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.AvailableSpots), allEventsInVenue.Select(e => e.AvailableSpots));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.OrganiserId), allEventsInVenue.Select(e => e.OrganiserId));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.JoinedUsers), allEventsInVenue.Select(e => e.JoinedUsers));
    }
    [Test]
    public async Task GetAllEventsInVenueAsync_WithNonExistingVenueId_ShouldReturnEmpty()
    {
        // Arrange
        var nonExistingVenueId = 999;

        // Act
        var allEventsInVenue = await service.GetAllEventsInVenueAsync(nonExistingVenueId);

        // Assert
        Assert.IsEmpty(allEventsInVenue);
    }
    [Test]
    public async Task GetAllPastEventsAsync_ShouldReturnAllPastEvents()
    {
        // Arrange
        var actualEvents = dbContext.Events
            .Where(e => e.StartDate < DateTime.Now)
            .Select(e => new AllEventViewModel
            {
                Id = e.Id,
                Title = e.Title,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Price = e.Price,
                VenueName = e.Venue.Name,
                AvailableSpots = e.AvailableSpots
            })
            .ToList();

        // Act
        var allPastEvents = await service.GetAllPastEventsAsync();

        // Assert
        Assert.AreEqual(actualEvents.Count, allPastEvents.Count);
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Id), allPastEvents.Select(e => e.Id));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Title), allPastEvents.Select(e => e.Title));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.StartDate), allPastEvents.Select(e => e.StartDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Price), allPastEvents.Select(e => e.Price));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.VenueName), allPastEvents.Select(e => e.VenueName));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.AvailableSpots), allPastEvents.Select(e => e.AvailableSpots));
    }

    [Test]
    public async Task GetAllPastEventsAsync_WithNoPastEvents_ShouldReturnEmpty()
    {
        // Arrange
        var actualEvents = dbContext.Events
            .Where(e => e.StartDate < DateTime.Now)
            .ToList();
        dbContext.Events.RemoveRange(actualEvents);
        await dbContext.SaveChangesAsync();

        // Act
        var allPastEvents = await service.GetAllPastEventsAsync();

        // Assert
        Assert.IsEmpty(allPastEvents);
    }
    [Test]
    public async Task GetAllPastEventsInVenueAsync_ShouldReturnAllPastEventsInVenue()
    {
        // Arrange
        var venueId = 1;
        var actualEvents = dbContext.Events
            .Where(e => e.VenueId == venueId && e.StartDate < DateTime.Now)
            .Select(e => new EventViewModel
            {
                Id = e.Id,
                Title = e.Title,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Price = e.Price,
                AvailableSpots = e.AvailableSpots,
                OrganiserId = e.OrganiserId,
                JoinedUsers = e.UsersEvents.Count()
            })
            .ToList();

        // Act
        var allPastEventsInVenue = await service.GetAllPastEventsInVenueAsync(venueId);

        // Assert
        Assert.AreEqual(actualEvents.Count, allPastEventsInVenue.Count);
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Id), allPastEventsInVenue.Select(e => e.Id));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Title), allPastEventsInVenue.Select(e => e.Title));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.StartDate), allPastEventsInVenue.Select(e => e.StartDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.EndDate), allPastEventsInVenue.Select(e => e.EndDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Price), allPastEventsInVenue.Select(e => e.Price));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.AvailableSpots), allPastEventsInVenue.Select(e => e.AvailableSpots));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.OrganiserId), allPastEventsInVenue.Select(e => e.OrganiserId));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.JoinedUsers), allPastEventsInVenue.Select(e => e.JoinedUsers));
    }

    [Test]
    public async Task GetAllPastEventsInVenueAsync_WithNoPastEventsInVenue_ShouldReturnEmpty()
    {
        // Arrange
        var nonExistingVenueId = 999;

        // Act
        var allPastEventsInVenue = await service.GetAllPastEventsInVenueAsync(nonExistingVenueId);

        // Assert
        Assert.IsEmpty(allPastEventsInVenue);
    }
    [Test]
    public async Task GetEventAsync_WithExistingId_ShouldReturnEventDetails()
    {
        // Arrange
        var eventId = 1;
        var actualEvent = dbContext.Events
            .Where(e => e.Id == eventId)
            .Select(e => new EventDetailsViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                OrganiserName = e.Organiser.UserName,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                EndDate = e.EndDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Price = e.Price,
                VenueName = e.Venue.Name,
                AvailableSpots = e.AvailableSpots
            })
            .FirstOrDefault();

        // Act
        var eventDetails = await service.GetEventAsync(eventId);

        // Assert
        Assert.IsNotNull(eventDetails);
        Assert.AreEqual(actualEvent.Id, eventDetails.Id);
        Assert.AreEqual(actualEvent.Title, eventDetails.Title);
        Assert.AreEqual(actualEvent.Description, eventDetails.Description);
        Assert.AreEqual(actualEvent.OrganiserName, eventDetails.OrganiserName);
        Assert.AreEqual(actualEvent.StartDate, eventDetails.StartDate);
        Assert.AreEqual(actualEvent.EndDate, eventDetails.EndDate);
        Assert.AreEqual(actualEvent.Price, eventDetails.Price);
        Assert.AreEqual(actualEvent.VenueName, eventDetails.VenueName);
        Assert.AreEqual(actualEvent.AvailableSpots, eventDetails.AvailableSpots);
    }


    [Test]
    public async Task GetEventAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingEventId = 999;

        // Act
        var eventDetails = await service.GetEventAsync(nonExistingEventId);

        // Assert
        Assert.IsNull(eventDetails);
    }

    [Test]
    public async Task GetEventForEditAsync_WithExistingId_ShouldReturnEventEditDetails()
    {
        // Arrange
        var eventId = 1;
        var actualEvent = dbContext.Events
            .Where(e => e.Id == eventId)
            .Select(e => new EventEditViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Price = e.Price,
                VenueId = e.Venue.Id
            })
            .FirstOrDefault();

        // Act
        var eventEditDetails = await service.GetEventForEditAsync(eventId);

        // Assert
        Assert.IsNotNull(eventEditDetails);
        Assert.AreEqual(actualEvent.Id, eventEditDetails.Id);
        Assert.AreEqual(actualEvent.Title, eventEditDetails.Title);
        Assert.AreEqual(actualEvent.Description, eventEditDetails.Description);
        Assert.AreEqual(actualEvent.StartDate, eventEditDetails.StartDate);
        Assert.AreEqual(actualEvent.EndDate, eventEditDetails.EndDate);
        Assert.AreEqual(actualEvent.Price, eventEditDetails.Price);
        Assert.AreEqual(actualEvent.VenueId, eventEditDetails.VenueId);
    }

    [Test]
    public async Task GetEventForEditAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingEventId = 999;

        // Act
        var eventEditDetails = await service.GetEventForEditAsync(nonExistingEventId);

        // Assert
        Assert.IsNull(eventEditDetails);
    }
    [Test]
    public async Task GetEventsOrganisedByUserAsync_WithExistingOrganiserId_ShouldReturnEventsOrganisedByUser()
    {
        // Arrange
        var organiserId = "TestUserId";
        var actualEvents = dbContext.Events
            .Where(e => e.OrganiserId == organiserId)
            .Select(e => new EventDetailsViewModel
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                OrganiserName = e.Organiser.UserName,
                StartDate = e.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                EndDate = e.EndDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Price = e.Price,
                VenueName = e.Venue.Name,
                AvailableSpots = e.AvailableSpots
            })
            .ToList();

        // Act
        var eventsOrganisedByUser = await service.GetEventsOrganisedByUserAsync(organiserId);

        // Assert
        Assert.AreEqual(actualEvents.Count, eventsOrganisedByUser.Count);
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Id), eventsOrganisedByUser.Select(e => e.Id));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Title), eventsOrganisedByUser.Select(e => e.Title));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.StartDate), eventsOrganisedByUser.Select(e => e.StartDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.EndDate), eventsOrganisedByUser.Select(e => e.EndDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Price), eventsOrganisedByUser.Select(e => e.Price));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.VenueName), eventsOrganisedByUser.Select(e => e.VenueName));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.AvailableSpots), eventsOrganisedByUser.Select(e => e.AvailableSpots));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.OrganiserName), eventsOrganisedByUser.Select(e => e.OrganiserName));
    }


    [Test]
    public async Task GetEventsOrganisedByUserAsync_WithNonExistingOrganiserId_ShouldReturnEmpty()
    {
        // Arrange
        var nonExistingOrganiserId = "NonExistingUserId";

        // Act
        var eventsOrganisedByUser = await service.GetEventsOrganisedByUserAsync(nonExistingOrganiserId);

        // Assert
        Assert.IsEmpty(eventsOrganisedByUser);
    }
    [Test]
    public async Task GetOrganiserIdAsync_WithExistingId_ShouldReturnOrganiserId()
    {
        // Arrange
        var eventId = 1;
        var actualOrganiserId = dbContext.Events
            .Where(e => e.Id == eventId)
            .Select(e => e.OrganiserId)
            .FirstOrDefault();

        // Act
        var organiserId = await service.GetOrganiserIdAsync(eventId);

        // Assert
        Assert.AreEqual(actualOrganiserId, organiserId);
    }

    [Test]
    public async Task GetOrganiserIdAsync_WithNonExistingId_ShouldReturnEmpty()
    {
        // Arrange
        var nonExistingEventId = 999;

        // Act
        var organiserId = await service.GetOrganiserIdAsync(nonExistingEventId);

        // Assert
        Assert.IsEmpty(organiserId);
    }
    [Test]
    public async Task GetUserUpcomingEventsAsync_WithExistingUserId_ShouldReturnUserUpcomingEvents()
    {
        // Arrange
        var userId = "JoinedUserId";
        var actualEvents = dbContext.UsersEvents
            .Where(ue => ue.UserId == userId && ue.Event.StartDate > DateTime.Now)
            .Select(ue => new EventDetailsViewModel
            {
                Id = ue.Event.Id,
                Title = ue.Event.Title,
                Description = ue.Event.Description,
                OrganiserName = ue.Event.Organiser.UserName,
                StartDate = ue.Event.StartDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                EndDate = ue.Event.EndDate.ToString("hh:mm dddd, dd MMMM yyyy"),
                Price = ue.Event.Price,
                VenueName = ue.Event.Venue.Name,
                AvailableSpots = ue.Event.AvailableSpots
            })
            .ToList();

        // Act
        var userUpcomingEvents = await service.GetUserEventsAsync(userId);

        // Assert
        Assert.AreEqual(actualEvents.Count, userUpcomingEvents.Count);
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Id), userUpcomingEvents.Select(e => e.Id));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Title), userUpcomingEvents.Select(e => e.Title));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.StartDate), userUpcomingEvents.Select(e => e.StartDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.EndDate), userUpcomingEvents.Select(e => e.EndDate));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.Price), userUpcomingEvents.Select(e => e.Price));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.VenueName), userUpcomingEvents.Select(e => e.VenueName));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.AvailableSpots), userUpcomingEvents.Select(e => e.AvailableSpots));
        CollectionAssert.AreEqual(actualEvents.Select(e => e.OrganiserName), userUpcomingEvents.Select(e => e.OrganiserName));
    }

    [Test]
    public async Task GetUserUpcomingEventsAsync_WithNonExistingUserId_ShouldReturnEmpty()
    {
        // Arrange
        var nonExistingUserId = "NonExistingUserId";

        // Act
        var userUpcomingEvents = await service.GetUserEventsAsync(nonExistingUserId);

        // Assert
        Assert.IsEmpty(userUpcomingEvents);
    }
    [Test]
    public async Task HasAlreadyStartedAsync_WithExistingId_ShouldReturnTrueIfEventHasStarted()
    {
        // Arrange
        var eventId = 1;
        var eventInDb = dbContext.Events
            .Where(e => e.Id == eventId)
            .FirstOrDefault();
        if (eventInDb != null)
        {
            eventInDb.StartDate = DateTime.Now.AddHours(-3);
            dbContext.Events.Update(eventInDb);
            await dbContext.SaveChangesAsync();
        }

        // Act
        var hasAlreadyStarted = await service.HasAlreadyStartedAsync(eventId);

        // Assert
        Assert.IsTrue(hasAlreadyStarted);
    }

    [Test]
    public async Task HasAlreadyStartedAsync_WithExistingId_ShouldReturnFalseIfEventHasNotStarted()
    {
        // Arrange
        var eventId = 1;
        var eventInDb = dbContext.Events
            .Where(e => e.Id == eventId)
            .FirstOrDefault();
        if (eventInDb != null)
        {
            eventInDb.StartDate = DateTime.Now.AddHours(3);
            dbContext.Events.Update(eventInDb);
            await dbContext.SaveChangesAsync();
        }

        // Act
        var hasAlreadyStarted = await service.HasAlreadyStartedAsync(eventId);

        // Assert
        Assert.IsFalse(hasAlreadyStarted);
    }

    [Test]
    public async Task HasAlreadyStartedAsync_WithNonExistingId_ShouldReturnTrue()
    {
        // Arrange
        var nonExistingEventId = 999;

        // Act
        var hasAlreadyStarted = await service.HasAlreadyStartedAsync(nonExistingEventId);

        // Assert
        Assert.IsTrue(hasAlreadyStarted);
    }
    [Test]
    public async Task HasAvaialbleSpotsAsync_WithExistingId_ShouldReturnTrueIfEventHasAvailableSpots()
    {
        // Arrange
        var eventId = 1;
        var eventInDb = dbContext.Events
            .Where(e => e.Id == eventId)
            .FirstOrDefault();
        if (eventInDb != null)
        {
            eventInDb.AvailableSpots = 5;
            dbContext.Events.Update(eventInDb);
            await dbContext.SaveChangesAsync();
        }

        // Act
        var hasAvailableSpots = await service.HasAvaialbleSpotsAsync(eventId);

        // Assert
        Assert.IsTrue(hasAvailableSpots);
    }

    [Test]
    public async Task HasAvaialbleSpotsAsync_WithExistingId_ShouldReturnFalseIfEventHasNoAvailableSpots()
    {
        // Arrange
        var eventId = 1;
        var eventInDb = dbContext.Events
            .Where(e => e.Id == eventId)
            .FirstOrDefault();
        if (eventInDb != null)
        {
            eventInDb.AvailableSpots = 0;
            dbContext.Events.Update(eventInDb);
            await dbContext.SaveChangesAsync();
        }

        // Act
        var hasAvailableSpots = await service.HasAvaialbleSpotsAsync(eventId);

        // Assert
        Assert.IsFalse(hasAvailableSpots);
    }

    [Test]
    public async Task HasAvaialbleSpotsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingEventId = 999;

        // Act
        var hasAvailableSpots = await service.HasAvaialbleSpotsAsync(nonExistingEventId);

        // Assert
        Assert.IsFalse(hasAvailableSpots);
    }

    [Test]
    public async Task IsUserAlreadyJoinedAsync_WithExistingIdAndUserId_ShouldReturnTrueIfUserAlreadyJoined()
    {
        // Arrange
        var eventId = 1;
        var userId = "TestUserId";
        var userEventInDb = dbContext.UsersEvents
            .Where(ue => ue.UserId == userId && ue.EventId == eventId)
            .FirstOrDefault();
        if (userEventInDb == null)
        {
            dbContext.UsersEvents.Add(new UserEvent { UserId = userId, EventId = eventId });
            await dbContext.SaveChangesAsync();
        }

        // Act
        var isUserAlreadyJoined = await service.IsUserAlreadyJoinedAsync(eventId, userId);

        // Assert
        Assert.IsTrue(isUserAlreadyJoined);
    }

    [Test]
    public async Task IsUserAlreadyJoinedAsync_WithExistingIdAndUserId_ShouldReturnFalseIfUserNotJoined()
    {
        // Arrange
        var eventId = 1;
        var userId = "TestUserId";
        var userEventInDb = dbContext.UsersEvents
            .Where(ue => ue.UserId == userId && ue.EventId == eventId)
            .FirstOrDefault();
        if (userEventInDb != null)
        {
            dbContext.UsersEvents.Remove(userEventInDb);
            await dbContext.SaveChangesAsync();
        }

        // Act
        var isUserAlreadyJoined = await service.IsUserAlreadyJoinedAsync(eventId, userId);

        // Assert
        Assert.IsFalse(isUserAlreadyJoined);
    }

    [Test]
    public async Task IsUserAlreadyJoinedAsync_WithNonExistingIdOrUserId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingEventId = 999;
        var nonExistingUserId = "NonExistingUserId";

        // Act
        var isUserAlreadyJoined = await service.IsUserAlreadyJoinedAsync(nonExistingEventId, nonExistingUserId);

        // Assert
        Assert.IsFalse(isUserAlreadyJoined);
    }
    [Test]
    public async Task JoinEventAsync_WithValidIdAndUserId_ShouldDecreaseAvailableSpotsByOne()
    {
        // Arrange
        var eventId = 2;
        var userId = "JoinedUserId";
        var initialAvailableSpots = dbContext.Events.Find(eventId).AvailableSpots;

        // Act
        await service.JoinEventAsync(eventId, userId);

        // Assert
        var eventAfterJoin = dbContext.Events.Find(eventId);
        Assert.AreEqual(initialAvailableSpots - 1, eventAfterJoin.AvailableSpots);
    }

    [Test]
    public async Task JoinEventAsync_WithNoAvailableSpots_ShouldNotChangeAvailableSpots()
    {
        // Arrange
        var eventId = 1;
        var userId = "TestUserId";
        var eventToJoin = dbContext.Events.Find(eventId);
        eventToJoin.AvailableSpots = 0;
        await dbContext.SaveChangesAsync();
        var joineToThisEvent = dbContext.UsersEvents.Where(ue => ue.EventId == eventId).Count();

        // Act
        await service.JoinEventAsync(eventId, userId);
        var joineToThisEventAfter = dbContext.UsersEvents.Where(ue => ue.EventId == eventId).Count();
        // Assert
        var eventAfterJoin = dbContext.Events.Find(eventId);
        Assert.AreEqual(0, eventAfterJoin.AvailableSpots);
        Assert.AreEqual(joineToThisEvent, joineToThisEventAfter);
    }

    [Test]
    public async Task LeaveEventAsync_WithValidIdAndUserId_ShouldIncreaseAvailableSpotsByOne()
    {
        // Arrange
        var eventId = 1;
        var userId = "JoinedUserId";
        var initialAvailableSpots = dbContext.Events.Find(eventId).AvailableSpots;
        var initialJoinedUsers = dbContext.UsersEvents.Where(ue => ue.EventId == eventId).Count();

        // Act
        await service.LeaveEventAsync(eventId, userId);

        // Assert
        var JoinedUsersAfter = dbContext.UsersEvents.Where(ue => ue.EventId == eventId).Count();
        var eventAfterLeave = dbContext.Events.Find(eventId);
        Assert.AreEqual(initialAvailableSpots + 1, eventAfterLeave.AvailableSpots);
        Assert.AreEqual(initialJoinedUsers-1, JoinedUsersAfter);
    }

    [Test]
    public async Task GetJoinedUsersAsync_ShouldReturnCorrectUsernames()
    {
        // Arrange
        var eventId = 1;

        // Act
        var result = await service.GetJoinedUsersAsync(eventId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("JoinedUser", result.First());
    }
    [Test]
    public async Task EditEventAsync_ShouldEditEventCorrectly()
    {
        // Arrange
        var model = new EventEditViewModel
        {
            Id = 1,
            Title = "Edited Event",
            Description = "Edited Description",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            Price = 200,
            VenueId = 1
        };
        var availableSpots = 50;

        // Act
        await service.EditEventAsync(model, availableSpots);

        // Assert
        var editedEvent = await dbContext.Events.FirstOrDefaultAsync(e => e.Id == model.Id);
        Assert.IsNotNull(editedEvent);
        Assert.AreEqual(model.Title, editedEvent.Title);
        Assert.AreEqual(model.Description, editedEvent.Description);
        Assert.AreEqual(model.StartDate, editedEvent.StartDate);
        Assert.AreEqual(model.EndDate, editedEvent.EndDate);
        Assert.AreEqual(model.Price, editedEvent.Price);
        Assert.AreEqual(model.VenueId, editedEvent.VenueId);
        Assert.AreEqual(availableSpots, editedEvent.AvailableSpots);
    }

}
