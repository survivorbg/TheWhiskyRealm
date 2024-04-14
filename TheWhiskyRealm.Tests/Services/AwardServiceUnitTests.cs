using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using TheWhiskyRealm.Infrastructure.Data.Models;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Core.Models.Award;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class AwardServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private IAwardService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);
        repository = new Repository(dbContext);
        service = new AwardService(repository);

        var whisky = new Whisky { Id = 1, Name = "Test Whisky", Age = 12, AlcoholPercentage = 40.0, Description = "Test Description", ImageURL = "https://example.com/test.jpg", DistilleryId = 1, WhiskyTypeId = 1 };
        await dbContext.AddAsync(whisky);

        var award = new Award { Id = 1, Title = "Test Award", AwardsCeremony = "Test Ceremony", MedalType = MedalType.Gold, Year = 2024, WhiskyId = whisky.Id };
        await dbContext.AddAsync(award);

        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddAwardAsync_WithValidModel_ShouldAddAward()
    {
        // Arrange
        var awardAddModel = new AwardAddModel
        {
            Title = "New Award",
            AwardsCeremony = "New Ceremony",
            MedalType = MedalType.Gold.ToString(),
            Year = 2025,
            WhiskyId = 1
        };

        // Act
        await service.AddAwardAsync(awardAddModel);

        // Assert
        var award = await dbContext.Awards.FirstOrDefaultAsync(a => a.Title == awardAddModel.Title);
        Assert.IsNotNull(award);
        Assert.AreEqual(awardAddModel.AwardsCeremony, award.AwardsCeremony);
        Assert.AreEqual(awardAddModel.MedalType, award.MedalType.ToString());
        Assert.AreEqual(awardAddModel.Year, award.Year);
        Assert.AreEqual(awardAddModel.WhiskyId, award.WhiskyId);
    }

    [Test]
    public async Task AddAwardAsync_WithNullModel_ShouldNotAddAward()
    {
        // Arrange
        AwardAddModel awardAddModel = null;

        // Act
        await service.DeleteAwardAsync(1);
        await service.AddAwardAsync(awardAddModel);

        // Assert
        var awards = await dbContext.Awards.ToListAsync();
        Assert.IsEmpty(awards);
    }
    [Test]
    public async Task AwardExistAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingAwardId = 1;

        // Act
        var awardExists = await service.AwardExistAsync(existingAwardId);

        // Assert
        Assert.IsTrue(awardExists);
    }

    [Test]
    public async Task AwardExistAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingAwardId = 999;

        // Act
        var awardExists = await service.AwardExistAsync(nonExistingAwardId);

        // Assert
        Assert.IsFalse(awardExists);
    }
    
    [Test]
    public async Task EditAwardAsync_WithExistingId_ShouldUpdateAward()
    {
        // Arrange
        var existingAwardId = 1;
        var awardViewModel = new AwardViewModel
        {
            Id = existingAwardId,
            Title = "Updated Award",
            AwardsCeremony = "Updated Ceremony",
            MedalType = MedalType.Silver.ToString(),
            Year = 2025,
            WhiskyId = 1
        };

        // Act
        await service.EditAwardAsync(awardViewModel);

        // Assert
        var award = await dbContext.Awards.FindAsync(existingAwardId);
        Assert.IsNotNull(award);
        Assert.AreEqual(awardViewModel.Title, award.Title);
        Assert.AreEqual(awardViewModel.AwardsCeremony, award.AwardsCeremony);
        Assert.AreEqual(awardViewModel.MedalType, award.MedalType.ToString());
        Assert.AreEqual(awardViewModel.Year, award.Year);
        Assert.AreEqual(awardViewModel.WhiskyId, award.WhiskyId);
    }
    [Test]
    public async Task GetAllWhiskyAwards_WithExistingWhiskyId_ShouldReturnAwardsForWhisky()
    {
        // Arrange
        var existingWhiskyId = 1;
        var expectedAwards = dbContext.Awards
            .Where(a => a.WhiskyId == existingWhiskyId)
            .Select(a => new AwardViewModel
            {
                Id = a.Id,
                AwardsCeremony = a.AwardsCeremony,
                MedalType = a.MedalType.ToString(),
                Title = a.Title,
                Year = a.Year,
                WhiskyId = a.WhiskyId
            })
            .ToList();

        // Act
        var actualAwards = await service.GetAllWhiskyAwards(existingWhiskyId);

        // Assert
        Assert.AreEqual(expectedAwards.Count, actualAwards.Count);
        CollectionAssert.AreEqual(expectedAwards.Select(a => a.Id), actualAwards.Select(a => a.Id));
        CollectionAssert.AreEqual(expectedAwards.Select(a => a.Title), actualAwards.Select(a => a.Title));
        CollectionAssert.AreEqual(expectedAwards.Select(a => a.AwardsCeremony), actualAwards.Select(a => a.AwardsCeremony));
        CollectionAssert.AreEqual(expectedAwards.Select(a => a.MedalType), actualAwards.Select(a => a.MedalType));
        CollectionAssert.AreEqual(expectedAwards.Select(a => a.Year), actualAwards.Select(a => a.Year));
        CollectionAssert.AreEqual(expectedAwards.Select(a => a.WhiskyId), actualAwards.Select(a => a.WhiskyId));
    }

    [Test]
    public async Task GetAllWhiskyAwards_WithNonExistingWhiskyId_ShouldReturnEmptyList()
    {
        // Arrange
        var nonExistingWhiskyId = 999;

        // Act
        var awards = await service.GetAllWhiskyAwards(nonExistingWhiskyId);

        // Assert
        Assert.IsEmpty(awards);
    }
    [Test]
    public async Task GetAwardByIdAsync_WithExistingId_ShouldReturnAward()
    {
        // Arrange
        var existingAwardId = 1;

        // Act
        var awardViewModel = await service.GetAwardByIdAsync(existingAwardId);

        // Assert
        Assert.IsNotNull(awardViewModel);
        Assert.AreEqual(existingAwardId, awardViewModel.Id);
    }

    [Test]
    public async Task GetAwardByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingAwardId = 999;

        // Act
        var awardViewModel = await service.GetAwardByIdAsync(nonExistingAwardId);

        // Assert
        Assert.IsNull(awardViewModel);
    }
    [Test]
    public async Task DeleteAwardAsync_WithExistingId_ShouldDeleteAward()
    {
        // Arrange
        var existingAwardId = 1;

        // Act
        await service.DeleteAwardAsync(existingAwardId);

        // Assert
        var award = await dbContext.Awards.FindAsync(existingAwardId);
        Assert.IsNull(award);
    }

    [Test]
    public async Task DeleteAwardAsync_WithNonExistingId_ShouldNotChangeAwards()
    {
        // Arrange
        var nonExistingAwardId = 999;
        var initialAwardsCount = await dbContext.Awards.CountAsync();

        // Act
        await service.DeleteAwardAsync(nonExistingAwardId);

        // Assert
        var finalAwardsCount = await dbContext.Awards.CountAsync();
        Assert.AreEqual(initialAwardsCount, finalAwardsCount);
    }
}
