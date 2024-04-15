using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data.Models;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Core.Models.Rating;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class RatingServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private IRatingService service;
    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);
        repository = new Repository(dbContext);
        service = new RatingService(repository);

        var user = new ApplicationUser
        {
            Id = "TestUserId",
            UserName = "TestUser"
        };

        var whisky = new Whisky
        {
            Id = 1,
            Name = "Test Whisky",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "Test Description",
            ImageURL = "TestImageURL"
        };

        var rating = new Rating
        {
            Id = 1,
            Nose = 8,
            Taste = 9,
            Finish = 7,
            WhiskyId = whisky.Id,
            UserId = user.Id
        };


        await dbContext.Users.AddAsync(user);
        await dbContext.Whiskies.AddAsync(whisky);
        await dbContext.Ratings.AddAsync(rating);
        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }
    [Test]
    public async Task EditRatingAsync_WithValidModelAndRatingId_ShouldUpdateRating()
    {
        // Arrange
        var ratingId = 1;
        var model = new RatingViewModel
        {
            Nose = 7,
            Taste = 8,
            Finish = 9,
            WhiskyId = 1
        };

        // Act
        await service.EditRatingAsync(model, ratingId);

        // Assert
        var ratingAfterEdit = dbContext.Ratings.Find(ratingId);
        Assert.AreEqual(model.Nose, ratingAfterEdit.Nose);
        Assert.AreEqual(model.Taste, ratingAfterEdit.Taste);
        Assert.AreEqual(model.Finish, ratingAfterEdit.Finish);
    }
    [Test]
    public async Task GetAvgRatingAsync_WithValidWhiskyId_ShouldReturnAverageRating()
    {
        // Arrange
        var whiskyId = 1;

        // Act
        var avgRating = await service.GetAvgRatingAsync(whiskyId);

        // Assert
        var ratings = dbContext.Ratings
            .Where(r => r.WhiskyId == whiskyId)
            .ToList();

        var expectedAvgRating = ratings.Average(r=>r.AveragePoints);
        Assert.AreEqual(expectedAvgRating, avgRating);
    }
    [Test]
    public async Task GetRatingAsync_WithValidUserIdAndWhiskyId_ShouldReturnRatingEditViewModel()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "TestUserId";
        var expectedRating = dbContext.Ratings
            .Where(r => r.UserId == userId && r.WhiskyId == whiskyId)
            .Select(r => new RatingEditViewModel
            {
                Id = r.Id,
                Finish = r.Finish,
                Nose = r.Nose,
                Taste = r.Taste,
                WhiskyId = r.WhiskyId,
                UserId = r.UserId,
            })
            .FirstOrDefault();

        // Act
        var ratingViewModel = await service.GetRatingAsync(userId, whiskyId);

        // Assert
        Assert.AreEqual(expectedRating.Id, ratingViewModel.Id, "RatingEditViewModel ID does not match Rating ID in database.");
        Assert.AreEqual(expectedRating.Nose, ratingViewModel.Nose, "RatingEditViewModel Nose does not match Rating Nose in database.");
        Assert.AreEqual(expectedRating.Taste, ratingViewModel.Taste, "RatingEditViewModel Taste does not match Rating Taste in database.");
        Assert.AreEqual(expectedRating.Finish, ratingViewModel.Finish, "RatingEditViewModel Finish does not match Rating Finish in database.");
    }

    [Test]
    public async Task GetRatingsByUserAsync_WithValidUserId_ShouldReturnMyRatingViewModelCollection()
    {
        // Arrange
        var userId = "TestUserId";
        var expectedRatings = dbContext.Ratings
            .Where(r => r.UserId == userId && r.Whisky.isApproved == true)
            .Select(r => new MyRatingViewModel
            {
                Finish = r.Finish,
                Nose = r.Nose,
                Taste = r.Taste,
                WhiskyId = r.WhiskyId,
                WhiskyName = r.Whisky.Name
            })
            .ToList();

        // Act
        var ratingViewModels = await service.GetRatingsByUserAsync(userId);

        // Assert
        CollectionAssert.AreEqual(expectedRatings.Select(er => er.Finish), ratingViewModels.Select(rv => rv.Finish), "MyRatingViewModel Finish does not match Rating Finish in database.");
        CollectionAssert.AreEqual(expectedRatings.Select(er => er.Nose), ratingViewModels.Select(rv => rv.Nose), "MyRatingViewModel Nose does not match Rating Nose in database.");
        CollectionAssert.AreEqual(expectedRatings.Select(er => er.Taste), ratingViewModels.Select(rv => rv.Taste), "MyRatingViewModel Taste does not match Rating Taste in database.");
        CollectionAssert.AreEqual(expectedRatings.Select(er => er.WhiskyId), ratingViewModels.Select(rv => rv.WhiskyId), "MyRatingViewModel WhiskyId does not match Rating WhiskyId in database.");
        CollectionAssert.AreEqual(expectedRatings.Select(er => er.WhiskyName), ratingViewModels.Select(rv => rv.WhiskyName), "MyRatingViewModel WhiskyName does not match Rating WhiskyName in database.");
    }

    [Test]
    public async Task RateAsync_WithValidUserIdAndModel_ShouldAddNewRating()
    {
        // Arrange
        var userId = "TestUserId";
        var model = new RatingViewModel
        {
            Nose = 7,
            Taste = 8,
            Finish = 9,
            WhiskyId = 1
        };
        var initialRatingsCount = dbContext.Ratings.Count();

        // Act
        await service.RateAsync(userId, model);

        // Assert
        var finalRatingsCount = dbContext.Ratings.Count();
        Assert.AreEqual(initialRatingsCount + 1, finalRatingsCount);
    }

    [Test]
    public async Task RateAsync_WithNullUserId_ShouldNotAddNewRating()
    {
        // Arrange
        string userId = null;
        var model = new RatingViewModel
        {
            Nose = 7,
            Taste = 8,
            Finish = 9,
            WhiskyId = 1
        };
        var initialRatingsCount = dbContext.Ratings.Count();

        // Act
        await service.RateAsync(userId, model);

        // Assert
        var finalRatingsCount = dbContext.Ratings.Count();
        Assert.AreEqual(initialRatingsCount, finalRatingsCount);
    }

    [Test]
    public async Task RateAsync_WithNullModel_ShouldNotAddNewRating()
    {
        // Arrange
        var userId = "TestUserId";
        RatingViewModel model = null;
        var initialRatingsCount = dbContext.Ratings.Count();

        // Act
        await service.RateAsync(userId, model);

        // Assert
        var finalRatingsCount = dbContext.Ratings.Count();
        Assert.AreEqual(initialRatingsCount, finalRatingsCount);
    }
    [Test]
    public async Task UserAlreadyGaveRatingAsync_WithValidUserIdAndWhiskyId_ShouldReturnTrueIfUserAlreadyGaveRating()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "TestUserId";

        // Act
        var userAlreadyGaveRating = await service.UserAlreadyGaveRatingAsync(userId, whiskyId);

        // Assert
        var expectedUserAlreadyGaveRating = dbContext.Ratings.Any(r => r.WhiskyId == whiskyId && r.UserId == userId);
        Assert.AreEqual(expectedUserAlreadyGaveRating, userAlreadyGaveRating);
    }

    [Test]
    public async Task UserAlreadyGaveRatingAsync_WithNonExistantUserIdAndValidWhiskyId_ShouldReturnFalseIfUserDidNotGiveRating()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "NonExistentUserId";

        // Act
        var userAlreadyGaveRating = await service.UserAlreadyGaveRatingAsync(userId, whiskyId);

        // Assert
        var expectedUserAlreadyGaveRating = dbContext.Ratings.Any(r => r.WhiskyId == whiskyId && r.UserId == userId);
        Assert.AreEqual(expectedUserAlreadyGaveRating, userAlreadyGaveRating);
    }
    [Test]
    public async Task UserAlreadyGaveRatingAsync_WithValidUserIdAndNonExistantWhiskyId_ShouldReturnFalseIfUserDidNotGiveRating()
    {
        // Arrange
        var whiskyId = 999;
        var userId = "TestUserId";

        // Act
        var userAlreadyGaveRating = await service.UserAlreadyGaveRatingAsync(userId, whiskyId);

        // Assert
        var expectedUserAlreadyGaveRating = dbContext.Ratings.Any(r => r.WhiskyId == whiskyId && r.UserId == userId);
        Assert.AreEqual(expectedUserAlreadyGaveRating, userAlreadyGaveRating);
    }

}
