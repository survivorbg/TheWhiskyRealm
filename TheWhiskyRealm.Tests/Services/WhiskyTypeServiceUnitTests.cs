using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class WhiskyTypeServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IEnumerable<WhiskyType> whiskyTypes;
    private IRepository repository;
    private IWhiskyTypeService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);

        
        repository = new Repository(dbContext);
        service = new WhiskyTypeService(repository);

        var whiskyType = new WhiskyType { Id = 1, Name = "Test Whisky Type", Description = "Test Description" };
        await dbContext.AddAsync(whiskyType);
        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task GetAllWhiskyTypesAsync_ShouldReturnAllWhiskyTypes()
    {
        // Arrange
        var expectedWhiskyTypes = dbContext.WhiskyTypes
            .Select(wt => new WhiskyTypeViewModel
            {
                Id = wt.Id,
                Name = wt.Name
            })
            .ToList();

        // Act
        var actualWhiskyTypes = await service.GetAllWhiskyTypesAsync();

        // Assert
        Assert.AreEqual(expectedWhiskyTypes.Count, actualWhiskyTypes.Count());
        CollectionAssert.AreEqual(expectedWhiskyTypes.Select(wt => wt.Id), actualWhiskyTypes.Select(wt => wt.Id));
        CollectionAssert.AreEqual(expectedWhiskyTypes.Select(wt => wt.Name), actualWhiskyTypes.Select(wt => wt.Name));
    }
    [Test]
    public async Task GetWhiskyTypeNameAsync_WithExistingId_ShouldReturnWhiskyTypeName()
    {
        // Arrange
        var existingWhiskyTypeId = 1;
        var expectedWhiskyTypeName = "Test Whisky Type"; 

        // Act
        var actualWhiskyTypeName = await service.GetWhiskyTypeNameAsync(existingWhiskyTypeId);

        // Assert
        Assert.AreEqual(expectedWhiskyTypeName, actualWhiskyTypeName);
    }

    [Test]
    public async Task GetWhiskyTypeNameAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingWhiskyTypeId = 999;

        // Act
        var whiskyTypeName = await service.GetWhiskyTypeNameAsync(nonExistingWhiskyTypeId);

        // Assert
        Assert.IsNull(whiskyTypeName);
    }
    [Test]
    public async Task WhiskyTypeExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingWhiskyTypeId = 1;

        // Act
        var whiskyTypeExists = await service.WhiskyTypeExistsAsync(existingWhiskyTypeId);

        // Assert
        Assert.IsTrue(whiskyTypeExists);
    }

    [Test]
    public async Task WhiskyTypeExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingWhiskyTypeId = 999;

        // Act
        var whiskyTypeExists = await service.WhiskyTypeExistsAsync(nonExistingWhiskyTypeId);

        // Assert
        Assert.IsFalse(whiskyTypeExists);
    }


}

