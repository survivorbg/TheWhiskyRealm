using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class DistilleryServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private IDistilleryService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);

        repository = new Repository(dbContext);
        service = new DistilleryService(repository);
        var country = new Country { Id = 1, Name = "United Kingdom" };

        await dbContext.AddAsync(country);
        var region = new Region { Id = 1, Name = "Test Region", CountryId = country.Id };
        await dbContext.AddAsync(region);

        var distillery = new Distillery { Id = 1, Name = "Test Distillery", YearFounded = 2000, RegionId = region.Id, ImageUrl = "TestUrl" };
        await dbContext.AddAsync(distillery);

        var whisky = new Whisky { Id = 1, Name = "Test Whisky", AlcoholPercentage = 40, Description = "Test Description", ImageURL = "TestUrl", DistilleryId = distillery.Id, WhiskyTypeId = 1 };
        await dbContext.AddAsync(whisky);

        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddDistilleryAsync_WithValidModel_ShouldAddDistilleryToDatabase()
    {
        // Arrange
        var distilleryFormViewModel = new DistilleryFormViewModel
        {
            Name = "New Test Distillery",
            YearFounded = 2000,
            RegionId = 1,
            ImageUrl = "NewTestUrl"
        };

        // Act
        var distilleryId = await service.AddDistilleryAsync(distilleryFormViewModel);

        // Assert
        var distilleryInDb = dbContext.Distilleries.FirstOrDefault(d => d.Id == distilleryId);
        Assert.IsNotNull(distilleryInDb);
        Assert.AreEqual(distilleryFormViewModel.Name, distilleryInDb.Name);
        Assert.AreEqual(distilleryFormViewModel.YearFounded, distilleryInDb.YearFounded);
        Assert.AreEqual(distilleryFormViewModel.RegionId, distilleryInDb.RegionId);
        Assert.AreEqual(distilleryFormViewModel.ImageUrl, distilleryInDb.ImageUrl);
    }

    [Test]
    public async Task DistilleryExistByName_WithExistingName_ShouldReturnTrue()
    {
        // Arrange
        var existingDistilleryName = "Test Distillery";

        // Act
        var distilleryExists = await service.DistilleryExistByName(existingDistilleryName);

        // Assert
        Assert.IsTrue(distilleryExists);
    }

    [Test]
    public async Task DistilleryExistByName_WithNonExistingName_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingDistilleryName = "Non Existing Distillery";

        // Act
        var distilleryExists = await service.DistilleryExistByName(nonExistingDistilleryName);

        // Assert
        Assert.IsFalse(distilleryExists);
    }

    [Test]
    public async Task DistilleryExistByName_WithExistingNameAndId_ShouldReturnFalse()
    {
        // Arrange
        var existingDistilleryName = "Test Distillery";
        var existingDistilleryId = 1;

        // Act
        var distilleryExists = await service.DistilleryExistByName(existingDistilleryName, existingDistilleryId);

        // Assert
        Assert.IsFalse(distilleryExists);
    }
    [Test]
    public async Task DistilleryExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingDistilleryId = 1;

        // Act
        var distilleryExists = await service.DistilleryExistsAsync(existingDistilleryId);

        // Assert
        Assert.IsTrue(distilleryExists);
    }

    [Test]
    public async Task DistilleryExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingDistilleryId = 999;

        // Act
        var distilleryExists = await service.DistilleryExistsAsync(nonExistingDistilleryId);

        // Assert
        Assert.IsFalse(distilleryExists);
    }
    [Test]
    public async Task EditDistilleryAsync_WithExistingId_ShouldUpdateDistilleryInDatabase()
    {
        // Arrange
        var existingDistilleryId = 1;
        var distilleryFormViewModel = new DistilleryFormViewModel
        {
            Id = existingDistilleryId,
            Name = "New Test Distillery",
            YearFounded = 2000,
            RegionId = 1,
            ImageUrl = "NewTestUrl"
        };

        // Act
        await service.EditDistilleryAsync(distilleryFormViewModel);

        // Assert
        var distilleryInDb = dbContext.Distilleries.FirstOrDefault(d => d.Id == existingDistilleryId);
        Assert.IsNotNull(distilleryInDb);
        Assert.AreEqual(distilleryFormViewModel.Name, distilleryInDb.Name);
        Assert.AreEqual(distilleryFormViewModel.YearFounded, distilleryInDb.YearFounded);
        Assert.AreEqual(distilleryFormViewModel.RegionId, distilleryInDb.RegionId);
        Assert.AreEqual(distilleryFormViewModel.ImageUrl, distilleryInDb.ImageUrl);
    }

    [Test]
    public async Task EditDistilleryAsync_WithNonExistingId_ShouldNotChangeDatabase()
    {
        // Arrange
        var nonExistingDistilleryId = 999;
        var distilleryFormViewModel = new DistilleryFormViewModel
        {
            Id = nonExistingDistilleryId,
            Name = "New Test Distillery",
            YearFounded = 2000,
            RegionId = 1,
            ImageUrl = "NewTestUrl"
        };
        var initialDistilleryCount = dbContext.Distilleries.Count();

        // Act
        await service.EditDistilleryAsync(distilleryFormViewModel);

        // Assert
        var finalDistilleryCount = dbContext.Distilleries.Count();
        Assert.AreEqual(initialDistilleryCount, finalDistilleryCount);
    }
    [Test]
    public async Task GetAllDistilleriesAsync_ShouldReturnAllDistilleries()
    {
        // Act
        var distilleryViewModels = await service.GetAllDistilleriesAsync();

        // Assert
        var distilleriesInDb = dbContext.Distilleries.ToList();
        Assert.AreEqual(distilleriesInDb.Count, distilleryViewModels.Count(), "Number of DistilleryAddWhiskyViewModels does not match number of distilleries in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Id), distilleryViewModels.Select(dv => dv.DistilleryId), "DistilleryAddWhiskyViewModel IDs do not match distillery IDs in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Name), distilleryViewModels.Select(dv => dv.Name), "DistilleryAddWhiskyViewModel names do not match distillery names in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Region.Country.Name), distilleryViewModels.Select(dv => dv.Country), "DistilleryAddWhiskyViewModel country names do not match distillery country names in database.");
    }
    [Test]
    public async Task GetAllDistilleriesAsync_WithExistingRegionId_ShouldReturnDistilleriesInRegion()
    {
        // Arrange
        var existingRegionId = 1;

        // Act
        var distilleryRegionViewModels = await service.GetAllDistilleriesAsync(existingRegionId);

        // Assert
        var distilleriesInDb = dbContext.Distilleries.Where(d => d.RegionId == existingRegionId).ToList();
        Assert.AreEqual(distilleriesInDb.Count, distilleryRegionViewModels.Count(), "Number of DistilleryRegionViewModels does not match number of distilleries in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Id), distilleryRegionViewModels.Select(dv => dv.Id), "DistilleryRegionViewModel IDs do not match distillery IDs in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Name), distilleryRegionViewModels.Select(dv => dv.Name), "DistilleryRegionViewModel names do not match distillery names in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.YearFounded), distilleryRegionViewModels.Select(dv => dv.YearFounded), "DistilleryRegionViewModel year founded does not match distillery year founded in database.");
    }

    [Test]
    public async Task GetAllDistilleriesAsync_WithNonExistingRegionId_ShouldReturnEmptyList()
    {
        // Arrange
        var nonExistingRegionId = 999;

        // Act
        var distilleryRegionViewModels = await service.GetAllDistilleriesAsync(nonExistingRegionId);

        // Assert
        Assert.IsEmpty(distilleryRegionViewModels, "DistilleryRegionViewModels is not empty.");
    }
    [Test]
    public async Task GetAllDistilleriesAsync_WithValidParameters_ShouldReturnDistilleries()
    {
        // Arrange
        var currentPage = 1;
        var pageSize = 1;
        var sortOrder = "name_desc";

        // Act
        var distilleryViewModels = await service.GetAllDistilleriesAsync(currentPage, pageSize, sortOrder);

        // Assert
        var distilleriesInDb = dbContext.Distilleries.OrderByDescending(d => d.Name).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        Assert.AreEqual(distilleriesInDb.Count, distilleryViewModels.Count(), "Number of DistilleryViewModels does not match number of distilleries in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Id), distilleryViewModels.Select(dv => dv.Id), "DistilleryViewModel IDs do not match distillery IDs in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Name), distilleryViewModels.Select(dv => dv.Name), "DistilleryViewModel names do not match distillery names in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.YearFounded), distilleryViewModels.Select(dv => dv.YearFounded), "DistilleryViewModel year founded does not match distillery year founded in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Region.Name), distilleryViewModels.Select(dv => dv.Region), "DistilleryViewModel region names do not match distillery region names in database.");
        CollectionAssert.AreEqual(distilleriesInDb.Select(d => d.Region.Country.Name), distilleryViewModels.Select(dv => dv.Country), "DistilleryViewModel country names do not match distillery country names in database.");
    }
    [Test]
    public async Task GetDistilleryByIdAsync_WithExistingId_ShouldReturnDistilleryFormViewModel()
    {
        // Arrange
        var existingDistilleryId = 1;

        // Act
        var distilleryFormViewModel = await service.GetDistilleryByIdAsync(existingDistilleryId);

        // Assert
        var distilleryInDb = dbContext.Distilleries.FirstOrDefault(d => d.Id == existingDistilleryId);
        Assert.IsNotNull(distilleryFormViewModel);
        Assert.AreEqual(distilleryInDb.Id, distilleryFormViewModel.Id);
        Assert.AreEqual(distilleryInDb.Name, distilleryFormViewModel.Name);
        Assert.AreEqual(distilleryInDb.YearFounded, distilleryFormViewModel.YearFounded);
        Assert.AreEqual(distilleryInDb.RegionId, distilleryFormViewModel.RegionId);
        Assert.AreEqual(distilleryInDb.ImageUrl, distilleryFormViewModel.ImageUrl);
    }

    [Test]
    public async Task GetDistilleryByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingDistilleryId = 999;

        // Act
        var distilleryFormViewModel = await service.GetDistilleryByIdAsync(nonExistingDistilleryId);

        // Assert
        Assert.IsNull(distilleryFormViewModel);
    }
    [Test]
    public async Task GetDistilleryInfoAsync_WithExistingId_ShouldReturnDistilleryInfoModel()
    {
        // Arrange
        var existingDistilleryId = 1;

        // Act
        var distilleryInfoModel = await service.GetDistilleryInfoAsync(existingDistilleryId);

        // Assert
        var distilleryInDb = dbContext.Distilleries.Include(d => d.Region).ThenInclude(r => r.Country).FirstOrDefault(d => d.Id == existingDistilleryId);
        Assert.IsNotNull(distilleryInfoModel);
        Assert.AreEqual(distilleryInDb.Id, distilleryInfoModel.Id);
        Assert.AreEqual(distilleryInDb.Name, distilleryInfoModel.Name);
        Assert.AreEqual(distilleryInDb.YearFounded, distilleryInfoModel.YearFounded);
        Assert.AreEqual(distilleryInDb.Region.Name, distilleryInfoModel.Region);
        Assert.AreEqual(distilleryInDb.Region.Country.Name, distilleryInfoModel.Country);
    }

    [Test]
    public async Task GetDistilleryInfoAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingDistilleryId = 999;

        // Act
        var distilleryInfoModel = await service.GetDistilleryInfoAsync(nonExistingDistilleryId);

        // Assert
        Assert.IsNull(distilleryInfoModel);
    }
    [Test]
    public async Task GetTotalDistilleriesAsync_ShouldReturnTotalDistilleriesCount()
    {
        // Arrange
        var totalDistilleriesInDb = dbContext.Distilleries.Count();

        // Act
        var totalDistilleries = await service.GetTotalDistilleriesAsync();

        // Assert
        Assert.AreEqual(totalDistilleriesInDb, totalDistilleries);
    }

}
