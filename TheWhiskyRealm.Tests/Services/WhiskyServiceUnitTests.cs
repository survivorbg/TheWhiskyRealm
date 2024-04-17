using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky.Add;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class WhiskyServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private IRatingService ratingService;
    private IWhiskyService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);
        repository = new Repository(dbContext);
        ratingService = new RatingService(repository); 
        service = new WhiskyService(repository, ratingService);

        var testPublisher = new ApplicationUser
        {
            UserName = "Test Publisher",
            Id = "Test Id"
        };
        await dbContext.Users.AddAsync(testPublisher);
        var distillery = new Distillery
        {
            Name = "Test Distillery",
            Id = 1,
        };
        await dbContext.Distilleries.AddAsync(distillery);

        var whisky = new Whisky
        {
            Id = 1,
            Name = "Test Whisky",
            Age = 12,
            AlcoholPercentage = 40,
            Description = "Test Description",
            ImageURL = "Test URL",
            isApproved = true,
            PublishedBy = testPublisher.UserName,
            DistilleryId = distillery.Id,
            WhiskyTypeId = 1,
            Reviews = new List<Review>()
           
        };
        await dbContext.Whiskies.AddAsync(whisky);
        var whiskyTwo = new Whisky
        {
            Id = 2,
            Name = "Test Whisky 2",
            Age = 12,
            AlcoholPercentage = 40,
            Description = "Test Description 2",
            ImageURL = "Test URL 2",
            isApproved = false,
            PublishedBy = testPublisher.UserName,
            DistilleryId = distillery.Id,
            WhiskyTypeId = 1,
            Reviews = new List<Review>()
        };
        await dbContext.Whiskies.AddAsync(whiskyTwo);
        var whiskyThree = new Whisky
        {
            Id = 3,
            Name = "Test Whisky 3",
            Age = 12,
            AlcoholPercentage = 40,
            Description = "Test Description 3",
            ImageURL = "Test URL 3",
            isApproved = true,
            PublishedBy = null,
            DistilleryId = distillery.Id,
            WhiskyTypeId = 1,
            Reviews = new List<Review>()
        };
        await dbContext.Whiskies.AddAsync(whiskyThree);
        UserWhisky ue = new UserWhisky
        {
            UserId = testPublisher.Id,
            WhiskyId = 1,
            Whisky = whisky,
            User = testPublisher
        };
        await dbContext.UsersWhiskies.AddAsync(ue);
        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task WhiskyExistAsync_ShouldReturnTrue_WhenWhiskyExists()
    {
        // Arrange
        var whiskyId = 1;

        // Act
        var result = await service.WhiskyExistAsync(whiskyId);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task WhiskyExistAsync_ShouldReturnFalse_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var whiskyId = 100; 

        // Act
        var result = await service.WhiskyExistAsync(whiskyId);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task ApproveWhiskyAsync_ShouldSetIsApprovedToTrue_WhenWhiskyExists()
    {
        // Arrange
        var whiskyId = 1;

        // Act
        await service.ApproveWhiskyAsync(whiskyId);
        var whisky = await dbContext.Whiskies.FindAsync(whiskyId);

        // Assert
        Assert.IsTrue(whisky.isApproved);
    }

    [Test]
    public async Task ApproveWhiskyAsync_ShouldNotThrowException_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var whiskyId = 100; 

        // Act & Assert
        Assert.DoesNotThrowAsync(() => service.ApproveWhiskyAsync(whiskyId));
    }

    [Test]
    public async Task GetWhiskyPublisherAsync_ShouldReturnPublisher_WhenWhiskyExists()
    {
        // Arrange
        var whiskyId = 1;

        // Act
        var result = await service.GetWhiskyPublisherAsync(whiskyId);

        // Assert
        Assert.AreEqual("Test Publisher", result);
    }

    [Test]
    public async Task GetWhiskyPublisherAsync_ShouldReturnNull_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var whiskyId = 100; 

        // Act
        var result = await service.GetWhiskyPublisherAsync(whiskyId);

        // Assert
        Assert.IsNull(result);
    }
    [Test]
    public async Task WhiskyIsApprovedAsync_ShouldReturnTrue_WhenWhiskyIsApproved()
    {
        // Arrange
        var whiskyId = 1;

        // Act
        var result = await service.WhiskyIsApprovedAsync(whiskyId);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public async Task WhiskyIsApprovedAsync_ShouldReturnFalse_WhenWhiskyIsNotApprovedOrDoesNotExist()
    {
        // Arrange
        var whiskyId = 2; 

        // Act
        var result = await service.WhiskyIsApprovedAsync(whiskyId);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public async Task GetWhiskiesByDistilleryIdAsync_ShouldReturnWhiskies_WhenDistilleryExists()
    {
        // Arrange
        var distilleryId = 1;

        // Act
        var result = await service.GetWhiskiesByDistilleryIdAsync(distilleryId);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);
        Assert.IsTrue(result.All(w => w.IsApproved == "Yes" || w.IsApproved == "No"));
    }

    [Test]
    public async Task GetWhiskiesByDistilleryIdAsync_ShouldReturnEmpty_WhenDistilleryDoesNotExist()
    {
        // Arrange
        var distilleryId = 100; 

        // Act
        var result = await service.GetWhiskiesByDistilleryIdAsync(distilleryId);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }

    [Test]
    public void GetWhiskyByIdAsync_ShouldThrowException_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var whiskyId = 100; 

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => service.GetWhiskyByIdAsync(whiskyId));
    }
    [Test]
    public async Task AddWhiskyAsync_ShouldAddWhisky_WhenModelIsValid()
    {
        // Arrange
        var model = new WhiskyFormModel
        {
            Age = 12,
            AlcoholPercentage = 40,
            Description = "Test Description",
            DistilleryId = 1,
            WhiskyTypeId = 1,
            Name = "Test Whisky",
            ImageURL = "Test URL",
            IsApproved = true,
        };

        // Act
        await service.AddWhiskyAsync(model);
        var whisky = await dbContext.Whiskies.FindAsync(4); 

        // Assert
        Assert.IsNotNull(whisky);
        Assert.AreEqual(model.Name, whisky.Name);
    }

    [Test]
    public void GetWhiskyByIdForEditAsync_ShouldThrowException_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var whiskyId = 100; 

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => service.GetWhiskyByIdForEditAsync(whiskyId));
    }
    [Test]
    public async Task EditWhiskyAsync_ShouldEditWhisky_WhenWhiskyExistsAndModelIsValid()
    {
        // Arrange
        var whiskyId = 1;
        var model = new WhiskyFormModel
        {
            Name = "Edited Whisky",
            Age = 15,
            AlcoholPercentage = 45,
            Description = "Edited Description",
            DistilleryId = 1,
            WhiskyTypeId = 1,
            ImageURL = "Edited URL",
            IsApproved = true,
        };

        // Act
        await service.EditWhiskyAsync(whiskyId, model);
        var whisky = await dbContext.Whiskies.FindAsync(whiskyId);

        // Assert
        Assert.IsNotNull(whisky);
        Assert.AreEqual(model.Name, whisky.Name);
        Assert.AreEqual(model.Age, whisky.Age);
        Assert.AreEqual(model.AlcoholPercentage, whisky.AlcoholPercentage);
        Assert.AreEqual(model.Description, whisky.Description);
        Assert.AreEqual(model.DistilleryId, whisky.DistilleryId);
        Assert.AreEqual(model.ImageURL, whisky.ImageURL);
        Assert.AreEqual(model.IsApproved, whisky.isApproved);
    }

    [Test]
    public async Task EditWhiskyAsync_ShouldNotThrowException_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var whiskyId = 100; 
        var model = new WhiskyFormModel();

        // Act & Assert
        Assert.DoesNotThrowAsync(() => service.EditWhiskyAsync(whiskyId, model));
    }

    [Test]
    public async Task DeleteAsync_ShouldNotThrowException_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var whiskyId = 100; 

        // Act & Assert
        Assert.DoesNotThrowAsync(() => service.DeleteAsync(whiskyId));
    }

    [Test]
    public async Task WhiskyInFavouritesAsync_ShouldReturnTrue_WhenWhiskyIsInFavourites()
    {
        // Arrange
        var userId = "Test Id";
        var whiskyId = 1;

        // Act
        var result = await service.WhiskyInFavouritesAsync(userId, whiskyId);

        // Assert
        Assert.IsTrue(result);
    }
    

    [Test]
    public async Task WhiskyInFavouritesAsync_ShouldReturnFalse_WhenWhiskyIsNotInFavourites()
    {
        // Arrange
        var userId = "Test Id";
        var whiskyId = 2;

        // Act
        var result = await service.WhiskyInFavouritesAsync(userId, whiskyId);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task WhiskyInFavouritesAsync_ShouldReturnFalse_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = "NonExistentUser"; 
        var whiskyId = 1;

        // Act
        var result = await service.WhiskyInFavouritesAsync(userId, whiskyId);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public async Task WhiskyInFavouritesAsync_ShouldReturnFalse_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var userId = "Test Id";
        var whiskyId = 100; 

        // Act
        var result = await service.WhiskyInFavouritesAsync(userId, whiskyId);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public async Task AddToFavouritesAsync_ShouldAddWhiskyToFavourites_WhenUserAndWhiskyExist()
    {
        // Arrange
        var userId = "Test Id";
        var whiskyId = 2; 

        // Act
        await service.AddToFavouritesAsync(userId, whiskyId);
        var isFavourite = await service.WhiskyInFavouritesAsync(userId, whiskyId);

        // Assert
        Assert.IsTrue(isFavourite);
    }
    [Test]
    public async Task RemoveFromFavouritesAsync_ShouldRemoveWhiskyFromFavourites_WhenWhiskyIsInFavourites()
    {
        // Arrange
        var userId = "Test Id";
        var whiskyId = 1; 

        // Act
        await service.RemoveFromFavouritesAsync(userId, whiskyId);
        var isFavourite = await service.WhiskyInFavouritesAsync(userId, whiskyId);

        // Assert
        Assert.IsFalse(isFavourite);
    }

    [Test]
    public async Task RemoveFromFavouritesAsync_ShouldNotThrowException_WhenWhiskyIsNotInFavourites()
    {
        // Arrange
        var userId = "Test Id";
        var whiskyId = 2; 

        // Act & Assert
        Assert.DoesNotThrowAsync(() => service.RemoveFromFavouritesAsync(userId, whiskyId));
    }
    [Test]
    public async Task MyFavouriteWhiskiesAsync_ShouldReturnEmpty_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = "NonExistentUser"; 

        // Act
        var result = await service.MyFavouriteWhiskiesAsync(userId);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result);
    }


}

