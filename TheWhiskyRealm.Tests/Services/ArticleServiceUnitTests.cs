using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using TheWhiskyRealm.Infrastructure.Data.Models;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Core.Models.Article;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class ArticleServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private IArticleService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);
        repository = new Repository(dbContext);
        service = new ArticleService(repository);

        var user = new ApplicationUser { Id = "TestUserId", UserName = "TestUser" };
        await dbContext.AddAsync(user);

        var article = new Article { Id = 1, Title = "Test Article", Content = "Test Content", ImageUrl = "https://example.com/test.jpg", DateCreated = DateTime.UtcNow, Type = ArticleType.General, PublisherUserId = user.Id };
        await dbContext.AddAsync(article);

        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddArticleAsync_WithValidModel_ShouldAddArticle()
    {
        // Arrange
        var articleAddModel = new ArticleAddViewModel
        {
            Title = "New Article",
            Content = "New Content",
            ImageUrl = "https://example.com/new.jpg",
            ArticleType = ArticleType.General.ToString()
        };

        // Act
        var newArticleId = await service.AddArticleAsync(articleAddModel, "TestUserId");

        // Assert
        var article = await dbContext.Articles.FindAsync(newArticleId);
        Assert.IsNotNull(article);
        Assert.AreEqual(articleAddModel.Title, article.Title);
        Assert.AreEqual(articleAddModel.Content, article.Content);
        Assert.AreEqual(articleAddModel.ImageUrl, article.ImageUrl);
        Assert.AreEqual(articleAddModel.ArticleType, article.Type.ToString());
        Assert.AreEqual("TestUserId", article.PublisherUserId);
    }
    [Test]
    public async Task ArticleExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingArticleId = 1;

        // Act
        var articleExists = await service.ArticleExistsAsync(existingArticleId);

        // Assert
        Assert.IsTrue(articleExists);
    }

    [Test]
    public async Task ArticleExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingArticleId = 999;

        // Act
        var articleExists = await service.ArticleExistsAsync(nonExistingArticleId);

        // Assert
        Assert.IsFalse(articleExists);
    }
    [Test]
    public async Task DeleteArticleAsync_WithExistingId_ShouldDeleteArticle()
    {
        // Arrange
        var existingArticleId = 1;

        // Act
        await service.DeleteArticleAsync(existingArticleId);

        // Assert
        var article = await dbContext.Articles.FindAsync(existingArticleId);
        Assert.IsNull(article);
    }

    [Test]
    public async Task DeleteArticleAsync_WithNonExistingId_ShouldNotChangeArticles()
    {
        // Arrange
        var nonExistingArticleId = 999;
        var initialArticlesCount = await dbContext.Articles.CountAsync();

        // Act
        await service.DeleteArticleAsync(nonExistingArticleId);

        // Assert
        var finalArticlesCount = await dbContext.Articles.CountAsync();
        Assert.AreEqual(initialArticlesCount, finalArticlesCount);
    }
    [Test]
    public async Task EditArticleAsync_WithExistingId_ShouldUpdateArticle()
    {
        // Arrange
        var existingArticleId = 1;
        var articleEditModel = new ArticleEditViewModel
        {
            Id = existingArticleId,
            Title = "Updated Article",
            Content = "Updated Content",
            ImageUrl = "https://example.com/updated.jpg",
            ArticleType = ArticleType.Event.ToString()
        };

        // Act
        await service.EditArticleAsync(articleEditModel);

        // Assert
        var article = await dbContext.Articles.FindAsync(existingArticleId);
        Assert.IsNotNull(article);
        Assert.AreEqual(articleEditModel.Title, article.Title);
        Assert.AreEqual(articleEditModel.Content, article.Content);
        Assert.AreEqual(articleEditModel.ImageUrl, article.ImageUrl);
        Assert.AreEqual(articleEditModel.ArticleType, article.Type.ToString());
    }

    [Test]
    public async Task EditArticleAsync_WithNonExistingId_ShouldNotChangeArticles()
    {
        // Arrange
        var nonExistingArticleId = 999;
        var articleEditModel = new ArticleEditViewModel
        {
            Id = nonExistingArticleId,
            Title = "Updated Article",
            Content = "Updated Content",
            ImageUrl = "https://example.com/updated.jpg",
            ArticleType = ArticleType.Event.ToString()
        };
        var initialArticlesCount = await dbContext.Articles.CountAsync();

        // Act
        await service.EditArticleAsync(articleEditModel);

        // Assert
        var finalArticlesCount = await dbContext.Articles.CountAsync();
        Assert.AreEqual(initialArticlesCount, finalArticlesCount);
    }
    [Test]
    public async Task GetAllArticlesAsync_ShouldReturnAllArticles()
    {
        // Arrange
        var expectedArticles = dbContext.Articles
            .Select(a => new ArticleAllViewModel
            {
                ArticleType = a.Type.ToString(),
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Title = a.Title
            })
            .ToList();

        // Act
        var actualArticles = await service.GetAllArticlesAsync();

        // Assert
        Assert.AreEqual(expectedArticles.Count, actualArticles.Count);
        CollectionAssert.AreEqual(expectedArticles.Select(a => a.Id), actualArticles.Select(a => a.Id));
        CollectionAssert.AreEqual(expectedArticles.Select(a => a.Title), actualArticles.Select(a => a.Title));
        CollectionAssert.AreEqual(expectedArticles.Select(a => a.ImageUrl), actualArticles.Select(a => a.ImageUrl));
        CollectionAssert.AreEqual(expectedArticles.Select(a => a.ArticleType), actualArticles.Select(a => a.ArticleType));
    }
    [Test]
    public async Task GetArticleDetailsAsync_WithExistingId_ShouldReturnArticleDetails()
    {
        // Arrange
        var existingArticleId = 1;

        // Act
        var articleDetailsViewModel = await service.GetArticleDetailsAsync(existingArticleId);

        // Assert
        Assert.IsNotNull(articleDetailsViewModel);
        Assert.AreEqual(existingArticleId, articleDetailsViewModel.Id);
    }

    [Test]
    public async Task GetArticleDetailsAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingArticleId = 999;

        // Act
        var articleDetailsViewModel = await service.GetArticleDetailsAsync(nonExistingArticleId);

        // Assert
        Assert.IsNull(articleDetailsViewModel);
    }
    [Test]
    public async Task GetArticleEditAsync_WithExistingId_ShouldReturnArticleEditModel()
    {
        // Arrange
        var existingArticleId = 1;

        // Act
        var articleEditViewModel = await service.GetArticleEditAsync(existingArticleId);

        // Assert
        Assert.IsNotNull(articleEditViewModel);
        Assert.AreEqual(existingArticleId, articleEditViewModel.Id);
    }

    [Test]
    public async Task GetArticleEditAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingArticleId = 999;

        // Act
        var articleEditViewModel = await service.GetArticleEditAsync(nonExistingArticleId);

        // Assert
        Assert.IsNull(articleEditViewModel);
    }
    [Test]
    public async Task GetUserArticlesAsync_WithExistingUserId_ShouldReturnUserArticles()
    {
        // Arrange
        var existingUserId = "TestUserId";

        // Act
        var userArticles = await service.GetUserArticlesAsync(existingUserId);

        // Assert
        Assert.IsNotEmpty(userArticles);
        foreach (var article in userArticles)
        {
            var dbArticle = await dbContext.Articles.FindAsync(article.Id);
            Assert.IsNotNull(dbArticle);
            Assert.AreEqual(existingUserId, dbArticle.PublisherUserId);
        }
    }

    [Test]
    public async Task GetUserArticlesAsync_WithNonExistingUserId_ShouldReturnEmptyList()
    {
        // Arrange
        var nonExistingUserId = "NonExistingUserId";

        // Act
        var userArticles = await service.GetUserArticlesAsync(nonExistingUserId);

        // Assert
        Assert.IsEmpty(userArticles);
    }
    [Test]
    public async Task IsTheArticleAuthorAsync_WithArticleAuthor_ShouldReturnTrue()
    {
        // Arrange
        var existingArticleId = 1;
        var articleAuthorUserId = "TestUserId";

        // Act
        var isAuthor = await service.IsTheArticleAuthorAsync(articleAuthorUserId, existingArticleId);

        // Assert
        Assert.IsTrue(isAuthor);
    }

    [Test]
    public async Task IsTheArticleAuthorAsync_WithNonArticleAuthor_ShouldReturnFalse()
    {
        // Arrange
        var existingArticleId = 1;
        var nonArticleAuthorUserId = "NonAuthorUserId";

        // Act
        var isAuthor = await service.IsTheArticleAuthorAsync(nonArticleAuthorUserId, existingArticleId);

        // Assert
        Assert.IsFalse(isAuthor);
    }

}

