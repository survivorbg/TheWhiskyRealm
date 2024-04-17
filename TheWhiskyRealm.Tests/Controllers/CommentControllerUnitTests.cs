using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TheWhiskyRealm.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Article;
using TheWhiskyRealm.Core.Models.Comment;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class CommentControllerUnitTests
{
    private Mock<ICommentService> mockCommentService;
    private Mock<IArticleService> mockArticleService;
    private CommentController controller;

    [SetUp]
    public void SetUp()
    {
        mockCommentService = new Mock<ICommentService>();
        mockArticleService = new Mock<IArticleService>();
        controller = new CommentController(mockCommentService.Object, mockArticleService.Object);
    }

    [Test]
    public async Task Add_ReturnsCorrectRedirectToActionResult_WhenModelIsValid()
    {
        // Arrange
        var model = new CommentAddViewModel
        {
            ArticleId = 1,
            Content = "Test content"
        };

        mockArticleService.Setup(s => s.ArticleExistsAsync(model.ArticleId)).ReturnsAsync(true);
        mockCommentService.Setup(s => s.AddCommentAsync(model, It.IsAny<string>())).Returns(Task.CompletedTask);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", actionResult.ActionName);
        Assert.AreEqual("Article", actionResult.ControllerName);
        Assert.AreEqual(model.ArticleId, actionResult.RouteValues["id"]);
    }

    [Test]
    public async Task Add_ReturnsViewResultWithModel_WhenModelStateIsInvalid()
    {
        // Arrange
        var model = new CommentAddViewModel
        {
            ArticleId = 1,
            Content = "Test content"
        };
        mockArticleService.Setup(s => s.ArticleExistsAsync(model.ArticleId)).ReturnsAsync(true);

        controller.ModelState.AddModelError("Error", "Test error");

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

    [Test]
    public async Task Add_ReturnsNotFoundResult_WhenArticleDoesNotExist()
    {
        // Arrange
        var model = new CommentAddViewModel
        {
            ArticleId = 1,
            Content = "Test content"
        };

        mockArticleService.Setup(s => s.ArticleExistsAsync(model.ArticleId)).ReturnsAsync(false);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Edit_ReturnsCorrectViewResultWithModel_WhenCommentAndArticleExistAndUserIsAuthorized()
    {
        // Arrange
        var id = 1;
        var commentViewModel = new CommentViewModel
        {
            Id = id,
            Content = "Test content",
            ArticleId = 1
        };
        var articleDetailsViewModel = new ArticleDetailsViewModel
        {
            Id = commentViewModel.ArticleId,
            Title = "Test title"
        };
        var userId = "TestUserId";

        mockCommentService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync(commentViewModel);
        mockCommentService.Setup(s => s.GetCommentAuthorIdAsync(id)).ReturnsAsync(userId);
        mockArticleService.Setup(s => s.GetArticleDetailsAsync(commentViewModel.ArticleId)).ReturnsAsync(articleDetailsViewModel);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "Admin")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CommentEditViewModel>(viewResult.Model);
        var model = viewResult.Model as CommentEditViewModel;
        Assert.AreEqual(id, model.Id);
        Assert.AreEqual(commentViewModel.Content, model.Content);
        Assert.AreEqual(articleDetailsViewModel.Title, model.ArticleTitle);
    }

    [Test]
    public async Task Edit_ReturnsNotFoundResult_WhenCommentDoesNotExist()
    {
        // Arrange
        var id = 1;

        mockCommentService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync((CommentViewModel)null);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = 1;
        var commentViewModel = new CommentViewModel
        {
            Id = id,
            Content = "Test content",
            ArticleId = 1
        };

        mockCommentService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync(commentViewModel);
        mockCommentService.Setup(s => s.GetCommentAuthorIdAsync(id)).ReturnsAsync("AnotherUserId");

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }

    [Test]
    public async Task Edit_ReturnsNotFoundResult_WhenArticleDoesNotExist()
    {
        // Arrange
        var id = 1;
        var commentViewModel = new CommentViewModel
        {
            Id = id,
            Content = "Test content",
            ArticleId = 1
        };

        mockCommentService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync(commentViewModel);
        mockCommentService.Setup(s => s.GetCommentAuthorIdAsync(id)).ReturnsAsync("TestUserId");
        mockArticleService.Setup(s => s.GetArticleDetailsAsync(commentViewModel.ArticleId)).ReturnsAsync((ArticleDetailsViewModel)null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        new Claim(ClaimTypes.Role, "Admin")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Edit_ReturnsCorrectRedirectToActionResult_WhenModelIsValidAndUserIsAuthorized()
    {
        // Arrange
        var model = new CommentEditViewModel
        {
            Id = 1,
            Content = "Test content",
            ArticleId = 1
        };
        var userId = "TestUserId";

        mockCommentService.Setup(s => s.CommentExistsAsync(model.Id)).ReturnsAsync(true);
        mockArticleService.Setup(s => s.ArticleExistsAsync(model.ArticleId)).ReturnsAsync(true);
        mockCommentService.Setup(s => s.GetCommentAuthorIdAsync(model.Id)).ReturnsAsync(userId);
        mockCommentService.Setup(s => s.EditCommentAsync(model)).Returns(Task.CompletedTask);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "Admin")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", actionResult.ActionName);
        Assert.AreEqual("Article", actionResult.ControllerName);
        Assert.AreEqual(model.ArticleId, actionResult.RouteValues["id"]);
    }

    [Test]
    public async Task EditPost_ReturnsNotFoundResult_WhenCommentDoesNotExist()
    {
        // Arrange
        var model = new CommentEditViewModel
        {
            Id = 1,
            Content = "Test content",
            ArticleId = 1
        };

        mockCommentService.Setup(s => s.CommentExistsAsync(model.Id)).ReturnsAsync(false);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task EditPost_ReturnsNotFoundResult_WhenArticleDoesNotExist()
    {
        // Arrange
        var model = new CommentEditViewModel
        {
            Id = 1,
            Content = "Test content",
            ArticleId = 1
        };

        mockCommentService.Setup(s => s.CommentExistsAsync(model.Id)).ReturnsAsync(true);
        mockArticleService.Setup(s => s.ArticleExistsAsync(model.ArticleId)).ReturnsAsync(false);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task EditPost_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var model = new CommentEditViewModel
        {
            Id = 1,
            Content = "Test content",
            ArticleId = 1
        };

        mockCommentService.Setup(s => s.CommentExistsAsync(model.Id)).ReturnsAsync(true);
        mockArticleService.Setup(s => s.ArticleExistsAsync(model.ArticleId)).ReturnsAsync(true);
        mockCommentService.Setup(s => s.GetCommentAuthorIdAsync(model.Id)).ReturnsAsync("AnotherUserId");

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }

    [Test]
    public async Task Edit_ReturnsViewResultWithModel_WhenModelStateIsInvalid()
    {
        // Arrange
        var model = new CommentEditViewModel
        {
            Id = 1,
            Content = "Test content",
            ArticleId = 1
        };

        var userId = "TestUserId";

        mockCommentService.Setup(s => s.CommentExistsAsync(model.Id)).ReturnsAsync(true);
        mockArticleService.Setup(s => s.ArticleExistsAsync(model.ArticleId)).ReturnsAsync(true);
        mockCommentService.Setup(s => s.GetCommentAuthorIdAsync(model.Id)).ReturnsAsync(userId);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "Admin")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        controller.ModelState.AddModelError("Error", "Test error");

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }
    [Test]
    public async Task Delete_ReturnsCorrectRedirectToActionResult_WhenCommentAndArticleExistAndUserIsAuthorized()
    {
        // Arrange
        var id = 1;
        var commentViewModel = new CommentViewModel
        {
            Id = id,
            Content = "Test content",
            ArticleId = 1
        };
        var userId = "TestUserId";

        mockCommentService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync(commentViewModel);
        mockArticleService.Setup(s => s.ArticleExistsAsync(commentViewModel.ArticleId)).ReturnsAsync(true);
        mockCommentService.Setup(s => s.GetCommentAuthorIdAsync(id)).ReturnsAsync(userId);
        mockCommentService.Setup(s => s.DeleteCommentAsync(id)).Returns(Task.CompletedTask);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "Admin")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", actionResult.ActionName);
        Assert.AreEqual("Article", actionResult.ControllerName);
        Assert.AreEqual(commentViewModel.ArticleId, actionResult.RouteValues["id"]);
    }

    [Test]
    public async Task Delete_ReturnsNotFoundResult_WhenCommentDoesNotExist()
    {
        // Arrange
        var id = 1;

        mockCommentService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync((CommentViewModel)null);

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Delete_ReturnsNotFoundResult_WhenArticleDoesNotExist()
    {
        // Arrange
        var id = 1;
        var commentViewModel = new CommentViewModel
        {
            Id = id,
            Content = "Test content",
            ArticleId = 1
        };

        mockCommentService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync(commentViewModel);
        mockArticleService.Setup(s => s.ArticleExistsAsync(commentViewModel.ArticleId)).ReturnsAsync(false);

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Delete_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = 1;
        var commentViewModel = new CommentViewModel
        {
            Id = id,
            Content = "Test content",
            ArticleId = 1
        };

        mockCommentService.Setup(s => s.GetCommentByIdAsync(id)).ReturnsAsync(commentViewModel);
        mockArticleService.Setup(s => s.ArticleExistsAsync(commentViewModel.ArticleId)).ReturnsAsync(true);
        mockCommentService.Setup(s => s.GetCommentAuthorIdAsync(id)).ReturnsAsync("AnotherUserId");

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }


}
