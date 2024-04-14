using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Models;
using TheWhiskyRealm.Core.Models.AdminArea.Region;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class RegionServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IEnumerable<Region> regions;
    private IRepository repository;
    private IRegionService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);


        repository = new Repository(dbContext);
        service = new RegionService(repository);
        var country = new Country { Id = 1, Name = "Bulgaria" };
        await dbContext.AddAsync(country);

        var region = new Region { Id = 1, Name = "Test Region", CountryId = 1 };
        await dbContext.AddAsync(region);
        var distillery = new Distillery { Id = 1, Name = "Test Distillery", RegionId = 1 };
        await dbContext.AddAsync(distillery);
        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddRegionAsync_WithValidData_ShouldAddRegionAndReturnId()
    {
        // Arrange
        var regionName = "New Region";
        var countryId = 1;

        // Act
        var addedRegionId = await service.AddRegionAsync(regionName, countryId);

        // Assert
        Assert.That(addedRegionId, Is.GreaterThan(0));
        Assert.IsTrue(await repository.AllReadOnly<Region>().AnyAsync(r => r.Id == addedRegionId));
    }
    [Test]
    public async Task GetAllRegionsByCountryIdAsync_WithExistingCountryId_ShouldReturnRegions()
    {
        // Arrange
        var existingCountryId = 1;

        // Act
        var regions = await service.GetAllRegionsByCountryIdAsync(existingCountryId);

        // Assert
        Assert.That(regions, Is.Not.Empty);
    }

    [Test]
    public async Task GetAllRegionsByCountryIdAsync_WithNonExistingCountryId_ShouldReturnEmpty()
    {
        // Arrange
        var nonExistingCountryId = 999;

        // Act
        var regions = await service.GetAllRegionsByCountryIdAsync(nonExistingCountryId);

        // Assert
        Assert.That(regions, Is.Empty);
    }

    [Test]
    public async Task GetAllRegionsByCountryIdAsync_WithExistingCountryId_ShouldReturnCorrectRegionData()
    {
        // Arrange
        var existingCountryId = 1;
        var expectedRegionData = new List<RegionCountryViewModel>
    {
        new RegionCountryViewModel { Name = "Test Region", Distilleries = 1 }
    };

        // Act
        var regions = await service.GetAllRegionsByCountryIdAsync(existingCountryId);

        // Assert
        CollectionAssert.AreEqual(expectedRegionData.Select(r => r.Name), regions.Select(r => r.Name));
        CollectionAssert.AreEqual(expectedRegionData.Select(r => r.Distilleries), regions.Select(r => r.Distilleries));
    }
    [Test]
    public async Task GetAllRegionsAsync_WithExistingCountryId_ShouldReturnRegions()
    {
        // Arrange
        var pageSize = 5;
        var currentPage = 1;

        // Act
        var regions = await service.GetAllRegionsAsync(currentPage, pageSize);

        // Assert
        Assert.That(regions, Is.Not.Empty);
    }

    [Test]
    public async Task GetAllRegionsAsync_WithExistingCountryId_ShouldReturnCorrectRegionData()
    {
        // Arrange
        var pageSize = 5;
        var currentPage = 1;
        var expectedRegionData = new List<RegionViewModel>
    {
        new RegionViewModel { Id = 1, Name = "Test Region", CountryName = "Bulgaria" }
    };

        // Act
        var regions = await service.GetAllRegionsAsync(currentPage, pageSize);

        // Assert
        CollectionAssert.AreEqual(expectedRegionData.Select(r => r.Id), regions.Select(r => r.Id));
        CollectionAssert.AreEqual(expectedRegionData.Select(r => r.Name), regions.Select(r => r.Name));
        CollectionAssert.AreEqual(expectedRegionData.Select(r => r.CountryName), regions.Select(r => r.CountryName));
    }
    [Test]
    public async Task GetTotalRegionsAsync_ShouldReturnCorrectCount()
    {
        // Arrange
        var expectedCount = 1;

        // Act
        var totalRegions = await service.GetTotalRegionsAsync();

        // Assert
        Assert.AreEqual(expectedCount, totalRegions);
    }

    [Test]
    public async Task RegionWithThisNameAndCountryExistsAsync_WithExistingNameAndCountryId_ShouldReturnTrue()
    {
        // Arrange
        var existingRegionName = "Test Region";
        var existingCountryId = 1;

        // Act
        var exists = await service.RegionWithThisNameAndCountryExistsAsync(existingRegionName, existingCountryId);

        // Assert
        Assert.IsTrue(exists);
    }

    [Test]
    public async Task RegionWithThisNameAndCountryExistsAsync_WithNonExistingNameAndCountryId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingRegionName = "Non-existing Region";
        var nonExistingCountryId = 999;

        // Act
        var exists = await service.RegionWithThisNameAndCountryExistsAsync(nonExistingRegionName, nonExistingCountryId);

        // Assert
        Assert.IsFalse(exists);
    }

    [Test]
    public async Task RegionExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingRegionId = 1;

        // Act
        var exists = await service.RegionExistsAsync(existingRegionId);

        // Assert
        Assert.IsTrue(exists);
    }

    [Test]
    public async Task RegionExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingRegionId = 999;

        // Act
        var exists = await service.RegionExistsAsync(nonExistingRegionId);

        // Assert
        Assert.IsFalse(exists);
    }

    [Test]
    public async Task GetRegionByIdAsync_WithExistingId_ShouldReturnCorrectRegionData()
    {
        // Arrange
        var existingRegionId = 1;
        var expectedRegionData = new EditRegionViewModel { Id = 1, Name = "Test Region", CountryId = 1 };

        // Act
        var region = await service.GetRegionByIdAsync(existingRegionId);

        // Assert
        Assert.IsNotNull(region);
        Assert.AreEqual(expectedRegionData.Id, region.Id);
        Assert.AreEqual(expectedRegionData.Name, region.Name);
        Assert.AreEqual(expectedRegionData.CountryId, region.CountryId);
    }

    [Test]
    public async Task GetRegionByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingRegionId = 999;

        // Act
        var region = await service.GetRegionByIdAsync(nonExistingRegionId);

        // Assert
        Assert.IsNull(region);
    }

    [Test]
    public async Task EditRegionAsync_WithExistingRegion_ShouldEditRegion()
    {
        // Arrange
        var existingRegionId = 1;
        var newRegionData = new EditRegionViewModel { Id = 1, Name = "New Region Name", CountryId = 1 };

        // Act
        await service.EditRegionAsync(newRegionData);

        // Assert
        var editedRegion = await dbContext.Regions.FindAsync(existingRegionId);
        Assert.AreEqual(newRegionData.Name, editedRegion.Name);
        Assert.AreEqual(newRegionData.CountryId, editedRegion.CountryId);
    }

    [Test]
    public async Task EditRegionAsync_WithNonExistingRegion_ShouldNotThrowException()
    {
        // Arrange
        var nonExistingRegionId = 999;
        var newRegionData = new EditRegionViewModel { Id = nonExistingRegionId, Name = "New Region Name", CountryId = 1 };

        // Act & Assert
        Assert.DoesNotThrowAsync(async () => await service.EditRegionAsync(newRegionData));
    }
    [Test]
    public async Task GetAllRegionsAsync_ShouldReturnAllRegionsWithCountryNames()
    {
        // Arrange
        var expectedRegions = new List<RegionViewModel>
    {
        new RegionViewModel { Id = 1, Name = "Test Region, Bulgaria" }
    };

        // Act
        var actualRegions = await service.GetAllRegionsAsync();

        // Assert
        CollectionAssert.AreEqual(expectedRegions.Select(r => r.Id), actualRegions.Select(r => r.Id));
        CollectionAssert.AreEqual(expectedRegions.Select(r => r.Name), actualRegions.Select(r => r.Name));
    }



}

