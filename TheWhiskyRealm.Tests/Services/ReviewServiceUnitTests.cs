using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data.Models;
using TheWhiskyRealm.Core.Models.Review;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class ReviewServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private IReviewService service;
    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);
        repository = new Repository(dbContext);
        service = new ReviewService(repository);

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

        var review = new Review
        {
            Id = 1,
            Title = "Test Review",
            Content = "Test Content",
            Recommend = true,
            WhiskyId = whisky.Id,
            UserId = user.Id
        };

        await dbContext.Users.AddAsync(user);
        await dbContext.Whiskies.AddAsync(whisky);
        await dbContext.Reviews.AddAsync(review);
        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddReviewAsync_WithValidModelAndUserId_ShouldAddNewReview()
    {
        // Arrange
        var userId = "UserId";
        var model = new ReviewFormModel
        {
            Content = "Test Content",
            Recommend = true,
            Title = "Test Title",
            WhiskyId = 1
        };
        var initialReviewsCount = dbContext.Reviews.Count();

        // Act
        await service.AddReviewAsync(model, userId);

        // Assert
        var finalReviewsCount = dbContext.Reviews.Count();
        Assert.AreEqual(initialReviewsCount + 1, finalReviewsCount);

        var reviewInDb = dbContext.Reviews.FirstOrDefault(r => r.UserId == userId && r.WhiskyId == model.WhiskyId);
        Assert.IsNotNull(reviewInDb);
        Assert.AreEqual(model.Content, reviewInDb.Content);
        Assert.AreEqual(model.Recommend, reviewInDb.Recommend);
        Assert.AreEqual(model.Title, reviewInDb.Title);
    }

    [Test]
    public async Task AllReviewsForWhiskyAsync_WithValidWhiskyId_ShouldReturnAllReviewsForWhisky()
    {
        // Arrange
        var whiskyId = 1;
        var expectedReviews = dbContext.Reviews
            .Where(r => r.WhiskyId == whiskyId)
            .OrderByDescending(r => r.Id)
            .Select(r => new ReviewViewModel
            {
                Content = r.Content,
                Id = r.Id,
                Recommend = r.Recommend,
                Title = r.Title,
                UserName = r.User.UserName
            })
            .ToList();

        // Act
        var reviewViewModels = await service.AllReviewsForWhiskyAsync(whiskyId);

        // Assert
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.Id), reviewViewModels.Select(rv => rv.Id), "ReviewViewModel IDs do not match Review IDs in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.Content), reviewViewModels.Select(rv => rv.Content), "ReviewViewModel Content does not match Review Content in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.Recommend), reviewViewModels.Select(rv => rv.Recommend), "ReviewViewModel Recommend does not match Review Recommend in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.Title), reviewViewModels.Select(rv => rv.Title), "ReviewViewModel Title does not match Review Title in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.UserName), reviewViewModels.Select(rv => rv.UserName), "ReviewViewModel UserName does not match Review UserName in database.");
    }

    [Test]
    public async Task AllReviewsForWhiskyAsync_WithInvalidWhiskyId_ShouldReturnEmptyCollection()
    {
        // Arrange
        var whiskyId = 999; 

        // Act
        var reviewViewModels = await service.AllReviewsForWhiskyAsync(whiskyId);

        // Assert
        Assert.IsEmpty(reviewViewModels);
    }

    [Test]
    public async Task AllUserReviewsAsync_WithValidUserId_ShouldReturnAllUserReviews()
    {
        // Arrange
        var userId = "TestUserId";
        var expectedReviews = dbContext.Reviews
            .Where(r => r.UserId == userId && r.Whisky.isApproved == true)
            .OrderByDescending(r => r.Id)
            .Select(r => new MyReviewModel
            {
                Content = r.Content,
                Id = r.Id,
                Recommend = r.Recommend,
                Title = r.Title,
                WhiskyId = r.WhiskyId,
                WhiskyName = r.Whisky.Name
            })
            .ToList();

        // Act
        var reviewModels = await service.AllUserReviewsAsync(userId);

        // Assert
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.Id), reviewModels.Select(rv => rv.Id), "MyReviewModel IDs do not match Review IDs in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.Content), reviewModels.Select(rv => rv.Content), "MyReviewModel Content does not match Review Content in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.Recommend), reviewModels.Select(rv => rv.Recommend), "MyReviewModel Recommend does not match Review Recommend in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.Title), reviewModels.Select(rv => rv.Title), "MyReviewModel Title does not match Review Title in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.WhiskyId), reviewModels.Select(rv => rv.WhiskyId), "MyReviewModel WhiskyId does not match Review WhiskyId in database.");
        CollectionAssert.AreEqual(expectedReviews.Select(er => er.WhiskyName), reviewModels.Select(rv => rv.WhiskyName), "MyReviewModel WhiskyName does not match Review WhiskyName in database.");
    }

    [Test]
    public async Task AllUserReviewsAsync_WithInvalidUserId_ShouldReturnEmptyCollection()
    {
        // Arrange
        var userId = "NonExistentUserId"; 

        // Act
        var reviewModels = await service.AllUserReviewsAsync(userId);

        // Assert
        Assert.IsEmpty(reviewModels);
    }

    [Test]
    public async Task DeleteReviewAsync_WithValidId_ShouldDeleteReview()
    {
        // Arrange
        var reviewId = 1;
        var initialReviewsCount = dbContext.Reviews.Count();

        // Act
        await service.DeleteReviewAsync(reviewId);

        // Assert
        var finalReviewsCount = dbContext.Reviews.Count();
        Assert.AreEqual(initialReviewsCount - 1, finalReviewsCount);
    }

    [Test]
    public async Task DeleteReviewAsync_WithInvalidId_ShouldNotChangeReviewCount()
    {
        // Arrange
        var reviewId = 999; 
        var initialReviewsCount = dbContext.Reviews.Count();

        // Act
        await service.DeleteReviewAsync(reviewId);

        // Assert
        var finalReviewsCount = dbContext.Reviews.Count();
        Assert.AreEqual(initialReviewsCount, finalReviewsCount);
    }
    [Test]
    public async Task EditReviewAsync_WithValidIdAndModel_ShouldUpdateReview()
    {
        // Arrange
        var reviewId = 1;
        var model = new ReviewFormModel
        {
            Title = "Updated Title",
            Recommend = false,
            Content = "Updated Content"
        };
        var initialReview = dbContext.Reviews.Find(reviewId);

        // Act
        await service.EditReviewAsync(reviewId, model);

        // Assert
        var reviewInDb = dbContext.Reviews.Find(reviewId);
        Assert.IsNotNull(reviewInDb);
        Assert.AreEqual(model.Title, reviewInDb.Title);
        Assert.AreEqual(model.Recommend, reviewInDb.Recommend);
        Assert.AreEqual(model.Content, reviewInDb.Content);
    }

    [Test]
    public async Task EditReviewAsync_WithInvalidId_ShouldNotChangeReviewCount()
    {
        // Arrange
        var reviewId = 999; 
        var model = new ReviewFormModel
        {
            Title = "Updated Title",
            Recommend = false,
            Content = "Updated Content"
        };
        var initialReviewsCount = dbContext.Reviews.Count();

        // Act
        await service.EditReviewAsync(reviewId, model);

        // Assert
        var finalReviewsCount = dbContext.Reviews.Count();
        Assert.AreEqual(initialReviewsCount, finalReviewsCount);
    }
    [Test]
    public async Task GetReviewAsync_WithValidId_ShouldReturnEditReviewFormModel()
    {
        // Arrange
        var reviewId = 1;
        var expectedReview = dbContext.Reviews
            .Where(r => r.Id == reviewId)
            .Select(r => new EditReviewFormModel
            {
                Content = r.Content,
                Recommend = r.Recommend,
                Title = r.Title,
                WhiskyId = r.WhiskyId,
                UserId = r.UserId,
                Id = reviewId
            })
            .FirstOrDefault();

        // Act
        var reviewModel = await service.GetReviewAsync(reviewId);

        // Assert
        Assert.IsNotNull(reviewModel);
        Assert.AreEqual(expectedReview.Content, reviewModel.Content);
        Assert.AreEqual(expectedReview.Recommend, reviewModel.Recommend);
        Assert.AreEqual(expectedReview.Title, reviewModel.Title);
        Assert.AreEqual(expectedReview.WhiskyId, reviewModel.WhiskyId);
        Assert.AreEqual(expectedReview.UserId, reviewModel.UserId);
    }

    [Test]
    public async Task GetReviewAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var reviewId = 999; 

        // Act
        var reviewModel = await service.GetReviewAsync(reviewId);

        // Assert
        Assert.IsNull(reviewModel);
    }

    [Test]
    public async Task GetReviewIdAsync_WithValidUserIdAndWhiskyId_ShouldReturnReviewId()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "TestUserId";
        var expectedReviewId = dbContext.Reviews
            .Where(r => r.WhiskyId == whiskyId && r.UserId == userId)
            .Select(r => r.Id)
            .FirstOrDefault();

        // Act
        var reviewId = await service.GetReviewIdAsync(userId, whiskyId);

        // Assert
        Assert.AreEqual(expectedReviewId, reviewId);
    }

    [Test]
    public async Task GetReviewIdAsync_WithInvalidUserIdOrWhiskyId_ShouldThrowException()
    {
        // Arrange
        var whiskyId = 999; 
        var userId = "NonExistentUserId"; 

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetReviewIdAsync(userId, whiskyId));
    }

    [Test]
    public async Task ReviewExistAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var reviewId = 1;

        // Act
        var reviewExists = await service.ReviewExistAsync(reviewId);

        // Assert
        Assert.IsTrue(reviewExists);
    }

    [Test]
    public async Task ReviewExistAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        var reviewId = 999; 

        // Act
        var reviewExists = await service.ReviewExistAsync(reviewId);

        // Assert
        Assert.IsFalse(reviewExists);
    }

    [Test]
    public async Task UserAlreadyReviewedWhiskyAsync_WithValidUserIdAndWhiskyId_ShouldReturnTrueIfUserAlreadyReviewed()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "TestUserId";

        // Act
        var userAlreadyReviewed = await service.UserAlreadyReviewedWhiskyAsync(userId, whiskyId);

        // Assert
        var expectedUserAlreadyReviewed = dbContext.Reviews.Any(r => r.WhiskyId == whiskyId && r.UserId == userId);
        Assert.AreEqual(expectedUserAlreadyReviewed, userAlreadyReviewed);
    }

    [Test]
    public async Task UserAlreadyReviewedWhiskyAsync_WithInvalidUserIdOrWhiskyId_ShouldReturnFalse()
    {
        // Arrange
        var whiskyId = 999;
        var userId = "NonExistentUserId"; 

        // Act
        var userAlreadyReviewed = await service.UserAlreadyReviewedWhiskyAsync(userId, whiskyId);

        // Assert
        Assert.IsFalse(userAlreadyReviewed);
    }


}
