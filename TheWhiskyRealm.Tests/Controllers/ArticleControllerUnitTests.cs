using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TheWhiskyRealm.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Article;
using TheWhiskyRealm.Infrastructure.Data.Enums;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class ArticleControllerUnitTests
{
    private Mock<IArticleService> mockArticleService;
    private Mock<ICommentService> mockCommentService;
    private ArticleController controller;

    [SetUp]
    public void SetUp()
    {
        mockArticleService = new Mock<IArticleService>();
        mockCommentService = new Mock<ICommentService>();
        controller = new ArticleController(mockArticleService.Object, mockCommentService.Object);
    }

    [Test]
    public async Task Index_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var articles = new List<ArticleAllViewModel>
    {
        new ArticleAllViewModel { Id = 1, Title = "Test Article 1", ImageUrl = "https://test.com/image1.jpg", ArticleType = "Test Type 1" },
        new ArticleAllViewModel { Id = 2, Title = "Test Article 2", ImageUrl = "https://test.com/image2.jpg", ArticleType = "Test Type 2" }
    };

        mockArticleService.Setup(s => s.GetAllArticlesAsync()).ReturnsAsync(articles);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<ArticleAllViewModel>>(viewResult.Model);
        var model = viewResult.Model as List<ArticleAllViewModel>;
        CollectionAssert.AreEquivalent(articles, model);
    }

    [Test]
    public async Task Edit_WhenIdIsInvalid_ReturnsNotFoundResult()
    {
        // Arrange
        var id = 1;

        mockArticleService.Setup(x => x.GetArticleEditAsync(id)).ReturnsAsync((ArticleEditViewModel)null);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_WhenUserIsNotTheAuthorAndNotAdmin_ReturnsUnauthorizedResult()
    {
        // Arrange
        var id = 1;
        var userId = "testUserId";
        var article = new ArticleEditViewModel { Id = id, Title = "Test Article", Content = "Test Content", ImageUrl = "https://test.com/image.jpg", ArticleType = "Test Type" };

        mockArticleService.Setup(x => x.GetArticleEditAsync(id)).ReturnsAsync(article);
        mockArticleService.Setup(x => x.IsTheArticleAuthorAsync(userId, id)).ReturnsAsync(false);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) })) } };

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }

    [Test]
    public async Task Edit_WhenUserIsTheAuthorOrAdmin_ReturnsViewResultWithModel()
    {
        // Arrange
        var id = 1;
        var userId = "testUserId";
        var article = new ArticleEditViewModel { Id = id, Title = "Test Article", Content = "Test Content", ImageUrl = "https://test.com/image.jpg", ArticleType = "Test Type" };
        var articleTypeOptions = Enum.GetNames(typeof(ArticleType));

        mockArticleService.Setup(x => x.GetArticleEditAsync(id)).ReturnsAsync(article);
        mockArticleService.Setup(x => x.IsTheArticleAuthorAsync(userId, id)).ReturnsAsync(true);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId), new Claim(ClaimTypes.Role, "Administrator") })) } };

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<ArticleEditViewModel>(viewResult.Model);
        var model = viewResult.Model as ArticleEditViewModel;
        Assert.AreEqual(article, model);
        CollectionAssert.AreEquivalent(articleTypeOptions, model.ArticleTypeOptions);
    }
    [Test]
    public async Task Edit_WhenArticleDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var model = new ArticleEditViewModel { Id = 1, Title = "Test Article", Content = "Test Content", ImageUrl = "https://test.com/image.jpg", ArticleType = "Test Type" };

        mockArticleService.Setup(x => x.ArticleExistsAsync(model.Id)).ReturnsAsync(false);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_WhenModelStateIsInvalid_ReturnsViewResultWithModel()
    {
        // Arrange
        var model = new ArticleEditViewModel { Id = 1, Title = "Test Article", Content = "Test Content", ImageUrl = "https://test.com/image.jpg", ArticleType = "Test Type" };
        var userId = "testUserId";
        var articleTypeOptions = Enum.GetNames(typeof(ArticleType));

        mockArticleService.Setup(x => x.ArticleExistsAsync(model.Id)).ReturnsAsync(true);
        mockArticleService.Setup(x => x.IsTheArticleAuthorAsync(userId, model.Id)).ReturnsAsync(true);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId), new Claim(ClaimTypes.Role, "Administrator") })) } };

        controller.ModelState.AddModelError("Error", "Some error");

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

    [Test]
    public async Task Edit_WhenArticleExistsAndModelStateIsValid_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new ArticleEditViewModel { Id = 1, Title = "Test Article", Content = "Test Content", ImageUrl = "https://test.com/image.jpg", ArticleType = "Test Type" };
        var userId = "testUserId";

        mockArticleService.Setup(x => x.ArticleExistsAsync(model.Id)).ReturnsAsync(true);
        mockArticleService.Setup(x => x.IsTheArticleAuthorAsync(userId, model.Id)).ReturnsAsync(true);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId), new Claim(ClaimTypes.Role, "Administrator") })) } };

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(ArticleController.Details), actionResult.ActionName);
        Assert.AreEqual(model.Id, actionResult.RouteValues["id"]);
    }
    [Test]
    public void Add_ReturnsViewResultWithModel()
    {
        // Arrange
        var articleTypeOptions = Enum.GetNames(typeof(ArticleType));

        // Act
        var result = controller.Add();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<ArticleAddViewModel>(viewResult.Model);
        var model = viewResult.Model as ArticleAddViewModel;
        CollectionAssert.AreEquivalent(articleTypeOptions, model.ArticleTypeOptions);
    }
    [Test]
    public async Task Add_WhenModelStateIsInvalid_ReturnsViewResultWithModel()
    {
        // Arrange
        var model = new ArticleAddViewModel { Title = "Test Article", Content = "Test Content", ImageUrl = "https://test.com/image.jpg", ArticleType = "Test Type" };
        var articleTypeOptions = Enum.GetNames(typeof(ArticleType));

        controller.ModelState.AddModelError("Error", "Some error");

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
        CollectionAssert.AreEquivalent(articleTypeOptions, model.ArticleTypeOptions);
    }

    [Test]
    public async Task Add_WhenModelStateIsValid_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new ArticleAddViewModel { Title = "Test Article", Content = "Test Content", ImageUrl = "https://test.com/image.jpg", ArticleType = "Test Type" };
        var userId = "testUserId";
        var newArticleId = 1;

        mockArticleService.Setup(x => x.AddArticleAsync(model, userId)).ReturnsAsync(newArticleId);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) })) } };

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(ArticleController.Details), actionResult.ActionName);
        Assert.AreEqual(newArticleId, actionResult.RouteValues["id"]);
    }
    [Test]
    public async Task Delete_WhenArticleDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var id = 1;

        mockArticleService.Setup(x => x.ArticleExistsAsync(id)).ReturnsAsync(false);

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Delete_WhenUserIsNotTheAuthorAndNotAdmin_ReturnsUnauthorizedResult()
    {
        // Arrange
        var id = 1;
        var userId = "testUserId";

        mockArticleService.Setup(x => x.ArticleExistsAsync(id)).ReturnsAsync(true);
        mockArticleService.Setup(x => x.IsTheArticleAuthorAsync(userId, id)).ReturnsAsync(false);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) })) } };

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }

    [Test]
    public async Task Delete_WhenUserIsTheAuthorOrAdmin_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = 1;
        var userId = "testUserId";

        mockArticleService.Setup(x => x.ArticleExistsAsync(id)).ReturnsAsync(true);
        mockArticleService.Setup(x => x.IsTheArticleAuthorAsync(userId, id)).ReturnsAsync(true);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId), new Claim(ClaimTypes.Role, "Administrator") })) } };

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(ArticleController.Index), actionResult.ActionName);
    }
    [Test]
    public async Task MyArticles_ReturnsViewResultWithModel()
    {
        // Arrange
        var userId = "testUserId";
        var articles = new List<ArticleAllViewModel>
    {
        new ArticleAllViewModel { Id = 1, Title = "Test Article 1", ImageUrl = "https://test.com/image1.jpg", ArticleType = "Test Type 1" },
        new ArticleAllViewModel { Id = 2, Title = "Test Article 2", ImageUrl = "https://test.com/image2.jpg", ArticleType = "Test Type 2" }
    };

        mockArticleService.Setup(s => s.GetUserArticlesAsync(userId)).ReturnsAsync(articles);
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.NameIdentifier, userId) })) } };

        // Act
        var result = await controller.MyArticles();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<ArticleAllViewModel>>(viewResult.Model);
        var model = viewResult.Model as List<ArticleAllViewModel>;
        CollectionAssert.AreEquivalent(articles, model);
    }

}
