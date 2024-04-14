using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Comment;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class CommentServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IEnumerable<Comment> comments;
    private IRepository repository;
    private ICommentService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);

        repository = new Repository(dbContext);
        service = new CommentService(repository);

        var author = new ApplicationUser { Id = "AuthorId", UserName = "Author" };
        var user = new ApplicationUser { Id = "TestUserId", UserName = "TestUser" };
        await dbContext.AddAsync(user);
        var article = new Article { Id = 1, Title = "Test Article", Content = "Test Content", ImageUrl = "TestUrl", DateCreated = DateTime.Now, Type = ArticleType.General, PublisherUserId = author.Id };
        await dbContext.AddAsync(article);

        var comment = new Comment { Id = 1, Content = "Test Comment", PostedDate = DateTime.Now, UserId = user.Id, ArticleId = article.Id };

        await dbContext.AddAsync(comment);
        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task AddCommentAsync_WithValidModel_ShouldAddCommentToDatabase()
    {
        // Arrange
        var commentAddViewModel = new CommentAddViewModel
        {
            ArticleId = 1,
            Content = "Test Content"
        };
        var userId = "TestId";

        // Act
        await service.AddCommentAsync(commentAddViewModel, userId);

        // Assert
        var commentInDb = dbContext.Comments.FirstOrDefault(c => c.UserId == userId && c.ArticleId == commentAddViewModel.ArticleId);
        Assert.IsNotNull(commentInDb);
        Assert.AreEqual(commentAddViewModel.Content, commentInDb.Content);
    }
    [Test]
    public async Task CommentExistsAsync_WithExistingId_ShouldReturnTrue()
    {
        // Arrange
        var existingCommentId = 1;

        // Act
        var commentExists = await service.CommentExistsAsync(existingCommentId);

        // Assert
        Assert.IsTrue(commentExists);
    }

    [Test]
    public async Task CommentExistsAsync_WithNonExistingId_ShouldReturnFalse()
    {
        // Arrange
        var nonExistingCommentId = 999;

        // Act
        var commentExists = await service.CommentExistsAsync(nonExistingCommentId);

        // Assert
        Assert.IsFalse(commentExists);
    }

    [Test]
    public async Task DeleteCommentAsync_WithExistingId_ShouldDeleteCommentFromDatabase()
    {
        // Arrange
        var existingCommentId = 1;

        // Act
        await service.DeleteCommentAsync(existingCommentId);

        // Assert
        var commentInDb = dbContext.Comments.FirstOrDefault(c => c.Id == existingCommentId);
        Assert.IsNull(commentInDb);
    }

    [Test]
    public async Task DeleteCommentAsync_WithNonExistingId_ShouldNotChangeDatabase()
    {
        // Arrange
        var nonExistingCommentId = 999;
        var initialCommentCount = dbContext.Comments.Count();

        // Act
        await service.DeleteCommentAsync(nonExistingCommentId);

        // Assert
        var finalCommentCount = dbContext.Comments.Count();
        Assert.AreEqual(initialCommentCount, finalCommentCount);
    }
    [Test]
    public async Task EditCommentAsync_WithExistingId_ShouldUpdateCommentInDatabase()
    {
        // Arrange
        var existingCommentId = 1;
        var newContent = "New Test Content";
        var commentEditViewModel = new CommentEditViewModel
        {
            Id = existingCommentId,
            Content = newContent,
            ArticleId = 1,
            ArticleTitle = "Test Article"
        };

        // Act
        await service.EditCommentAsync(commentEditViewModel);

        // Assert
        var commentInDb = dbContext.Comments.FirstOrDefault(c => c.Id == existingCommentId);
        Assert.IsNotNull(commentInDb);
        Assert.AreEqual(newContent, commentInDb.Content);
    }

    [Test]
    public async Task EditCommentAsync_WithNonExistingId_ShouldNotChangeDatabase()
    {
        // Arrange
        var nonExistingCommentId = 999;
        var newContent = "New Test Content";
        var commentEditViewModel = new CommentEditViewModel
        {
            Id = nonExistingCommentId,
            Content = newContent,
            ArticleId = 1,
            ArticleTitle = "Test Article"
        };
        var initialCommentCount = dbContext.Comments.Count();

        // Act
        await service.EditCommentAsync(commentEditViewModel);

        // Assert
        var finalCommentCount = dbContext.Comments.Count();
        Assert.AreEqual(initialCommentCount, finalCommentCount);
    }
    [Test]
    public async Task GetCommentAuthorIdAsync_WithExistingId_ShouldReturnAuthorId()
    {
        // Arrange
        var existingCommentId = 1;

        // Act
        var authorId = await service.GetCommentAuthorIdAsync(existingCommentId);

        // Assert
        var commentInDb = dbContext.Comments.FirstOrDefault(c => c.Id == existingCommentId);
        Assert.IsNotNull(commentInDb);
        Assert.AreEqual(commentInDb.UserId, authorId);
    }

    [Test]
    public async Task GetCommentAuthorIdAsync_WithNonExistingId_ShouldReturnEmptyString()
    {
        // Arrange
        var nonExistingCommentId = 999;

        // Act
        var authorId = await service.GetCommentAuthorIdAsync(nonExistingCommentId);

        // Assert
        Assert.AreEqual(string.Empty, authorId);
    }
    [Test]
    public async Task GetCommentByIdAsync_WithExistingId_ShouldReturnCommentViewModel()
    {
        // Arrange
        var existingCommentId = 1;

        // Act
        var commentViewModel = await service.GetCommentByIdAsync(existingCommentId);

        // Assert
        var commentInDb = dbContext.Comments.FirstOrDefault(c => c.Id == existingCommentId);
        Assert.IsNotNull(commentViewModel, "CommentViewModel is null.");
        Assert.AreEqual(commentInDb.Id, commentViewModel.Id, "CommentViewModel ID does not match.");
        Assert.AreEqual(commentInDb.Content, commentViewModel.Content, "CommentViewModel content does not match.");
        Assert.AreEqual(commentInDb.User.UserName, commentViewModel.AuthorName, "CommentViewModel author name does not match.");
        Assert.AreEqual(commentInDb.Article.Id, commentViewModel.ArticleId, "CommentViewModel article ID does not match.");
    }

    [Test]
    public async Task GetCommentByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        var nonExistingCommentId = 999;

        // Act
        var commentViewModel = await service.GetCommentByIdAsync(nonExistingCommentId);

        // Assert
        Assert.IsNull(commentViewModel, "CommentViewModel is not null."); //TODO Add messages everywhere
    }
    [Test]
    public async Task GetCommentsForArticleAsync_WithExistingArticleId_ShouldReturnCommentViewModels()
    {
        // Arrange
        var existingArticleId = 1;

        // Act
        var commentViewModels = await service.GetCommentsForArticleAsync(existingArticleId);

        // Assert
        var commentsInDb = dbContext.Comments.Where(c => c.ArticleId == existingArticleId).ToList();
        Assert.AreEqual(commentsInDb.Count, commentViewModels.Count, "Number of CommentViewModels does not match number of comments in database.");
        CollectionAssert.AreEqual(commentsInDb.Select(c => c.Id), commentViewModels.Select(cv => cv.Id), "CommentViewModel IDs do not match comment IDs in database.");
    }

    [Test]
    public async Task GetCommentsForArticleAsync_WithNonExistingArticleId_ShouldReturnEmptyList()
    {
        // Arrange
        var nonExistingArticleId = 999;

        // Act
        var commentViewModels = await service.GetCommentsForArticleAsync(nonExistingArticleId);

        // Assert
        Assert.IsEmpty(commentViewModels, "CommentViewModels is not empty.");
    }

}
