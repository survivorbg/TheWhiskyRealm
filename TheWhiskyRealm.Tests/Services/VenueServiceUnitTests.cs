using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class VenueServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private IVenueService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);
        repository = new Repository(dbContext);
        service = new VenueService(repository);

        var venue = new Venue { Id = 1, Name = "Test Venue", Capacity = 100, CityId = 1 };
        await dbContext.AddAsync(venue);
        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddVenueAsync_ShouldAddVenue()
    {
        // Arrange
        var venueFormViewModel = new VenueFormViewModel
        {
            Name = "New Venue",
            Capacity = 200,
            CityId = 2
        };

        // Act
        var venueId = await service.AddVenueAsync(venueFormViewModel);

        // Assert
        var venue = await dbContext.Venues.FindAsync(venueId);
        Assert.IsNotNull(venue);
        Assert.AreEqual(venueFormViewModel.Name, venue.Name);
        Assert.AreEqual(venueFormViewModel.Capacity, venue.Capacity);
        Assert.AreEqual(venueFormViewModel.CityId, venue.CityId);
    }

    [Test]
    public async Task EditVenueAsync_WithExistingId_ShouldUpdateVenue()
    {
        // Arrange
        var existingVenueId = 1;
        var venueFormViewModel = new VenueFormViewModel
        {
            Id = existingVenueId,
            Name = "Updated Venue",
            Capacity = 300,
            CityId = 2
        };

        // Act
        await service.EditVenueAsync(venueFormViewModel);

        // Assert
        var venue = await dbContext.Venues.FindAsync(existingVenueId);
        Assert.IsNotNull(venue);
        Assert.AreEqual(venueFormViewModel.Name, venue.Name);
        Assert.AreEqual(venueFormViewModel.Capacity, venue.Capacity);
        Assert.AreEqual(venueFormViewModel.CityId, venue.CityId);
    }

    [Test]
    public async Task EditVenueAsync_WithNonExistingId_ShouldNotUpdateVenue()
    {
        // Arrange
        var nonExistingVenueId = 999;
        var venueFormViewModel = new VenueFormViewModel
        {
            Id = nonExistingVenueId,
            Name = "Updated Venue",
            Capacity = 300,
            CityId = 2
        };

        // Act
        await service.EditVenueAsync(venueFormViewModel);

        // Assert
        var venue = await dbContext.Venues.FindAsync(nonExistingVenueId);
        Assert.IsNull(venue);
    }
    [Test]
    public async Task GetTotalVenuesAsync_ShouldReturnTotalVenuesCount()
    {
        // Arrange
        var expectedTotalVenues = await dbContext.Venues.CountAsync();

        // Act
        var actualTotalVenues = await service.GetTotalVenuesAsync();

        // Assert
        Assert.AreEqual(expectedTotalVenues, actualTotalVenues);
    }
    [Test]
    public async Task GetVenueByIdAsync_WithExistingId_ShouldReturnVenue()
    {
        // Arrange
        var existingVenueId = 1;

        // Act
        var venueFormViewModel = await service.GetVenueByIdAsync(existingVenueId);

        // Assert
        Assert.IsNotNull(venueFormViewModel);
        Assert.AreEqual(existingVenueId, venueFormViewModel.Id);
    }

    [Test]
    public async Task GetVenueByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingVenueId = 999;

        // Act
        var venueFormViewModel = await service.GetVenueByIdAsync(nonExistingVenueId);

        // Assert
        Assert.IsNull(venueFormViewModel);
    }
    [Test]
    public async Task GetVenuesAsync_ShouldReturnAllVenues()
    {
        // Arrange
        var expectedVenues = dbContext.Venues
            .Select(v => new VenueViewModel
            {
                VenueId = v.Id,
                VenueName = v.Name,
            })
            .ToList();

        // Act
        var actualVenues = await service.GetVenuesAsync();

        // Assert
        Assert.AreEqual(expectedVenues.Count, actualVenues.Count);
        CollectionAssert.AreEqual(expectedVenues.Select(v => v.VenueId), actualVenues.Select(v => v.VenueId));
        CollectionAssert.AreEqual(expectedVenues.Select(v => v.VenueName), actualVenues.Select(v => v.VenueName));
    }
    [Test]
    public async Task GetVenuesAsync_WithPaging_ShouldReturnPagedVenues()
    {
        // Arrange
        var currentPage = 1;
        var pageSize = 2;
        var expectedVenues = dbContext.Venues
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new VenueViewModel
            {
                VenueId = v.Id,
                VenueName = v.Name,
                Capacity = v.Capacity
            })
            .ToList();

        // Act
        var actualVenues = await service.GetVenuesAsync(currentPage, pageSize);

        // Assert
        Assert.AreEqual(expectedVenues.Count, actualVenues.Count());
        CollectionAssert.AreEqual(expectedVenues.Select(v => v.VenueId), actualVenues.Select(v => v.VenueId));
        CollectionAssert.AreEqual(expectedVenues.Select(v => v.VenueName), actualVenues.Select(v => v.VenueName));
        CollectionAssert.AreEqual(expectedVenues.Select(v => v.Capacity), actualVenues.Select(v => v.Capacity));
    }
    [Test]
    public async Task GetVenuesByCityAsync_WithExistingCityId_ShouldReturnVenuesInCity()
    {
        // Arrange
        var existingCityId = 1;
        var expectedVenues = dbContext.Venues
            .Where(v => v.CityId == existingCityId)
            .Select(v => new VenueViewModel
            {
                VenueId = v.Id,
                VenueName = v.Name,
                Capacity = v.Capacity
            })
            .ToList();

        // Act
        var actualVenues = await service.GetVenuesByCityAsync(existingCityId);

        // Assert
        Assert.AreEqual(expectedVenues.Count, actualVenues.Count);
        CollectionAssert.AreEqual(expectedVenues.Select(v => v.VenueId), actualVenues.Select(v => v.VenueId));
        CollectionAssert.AreEqual(expectedVenues.Select(v => v.VenueName), actualVenues.Select(v => v.VenueName));
        CollectionAssert.AreEqual(expectedVenues.Select(v => v.Capacity), actualVenues.Select(v => v.Capacity));
    }

    [Test]
    public async Task GetVenuesByCityAsync_WithNonExistingCityId_ShouldReturnEmptyList()
    {
        // Arrange
        var nonExistingCityId = 999;

        // Act
        var venues = await service.GetVenuesByCityAsync(nonExistingCityId);

        // Assert
        Assert.IsEmpty(venues);
    }
    [Test]
    public async Task VenueExistAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingVenueId = 1;

        // Act
        var venueExists = await service.VenueExistAsync(existingVenueId);

        // Assert
        Assert.IsTrue(venueExists);
    }

    [Test]
    public async Task VenueExistAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingVenueId = 999;

        // Act
        var venueExists = await service.VenueExistAsync(nonExistingVenueId);

        // Assert
        Assert.IsFalse(venueExists);
    }
    [Test]
    public async Task VenueExistByNameAsync_WithExistingNameAndCityId_ShouldReturnTrue()
    {
        // Arrange
        var existingVenueName = "Test Venue";
        var existingCityId = 1;

        // Act
        var venueExists = await service.VenueExistByNameAsync(existingVenueName, existingCityId);

        // Assert
        Assert.IsTrue(venueExists);
    }

    [Test]
    public async Task VenueExistByNameAsync_WithNonExistingNameAndCityId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingVenueName = "Non Existing Venue";
        var nonExistingCityId = 999;

        // Act
        var venueExists = await service.VenueExistByNameAsync(nonExistingVenueName, nonExistingCityId);

        // Assert
        Assert.IsFalse(venueExists);
    }

    [Test]
    public async Task VenueExistByNameAsync_WithExistingNameAndCityIdButDifferentVenueId_ShouldReturnTrue()
    {
        // Arrange
        var existingVenueName = "Test Venue";
        var existingCityId = 1;
        var differentVenueId = 2;

        // Act
        var venueExists = await service.VenueExistByNameAsync(existingVenueName, existingCityId, differentVenueId);

        // Assert
        Assert.IsTrue(venueExists);
    }

}

