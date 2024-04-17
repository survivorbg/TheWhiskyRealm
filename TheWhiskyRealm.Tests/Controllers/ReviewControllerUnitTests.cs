using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TheWhiskyRealm.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Review;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class ReviewControllerUnitTests
{
    private Mock<IWhiskyService> mockWhiskyService;
    private Mock<IReviewService> mockReviewService;
    private ReviewController controller;

    [SetUp]
    public void SetUp()
    {
        mockWhiskyService = new Mock<IWhiskyService>();
        mockReviewService = new Mock<IReviewService>();
        controller = new ReviewController(mockWhiskyService.Object, mockReviewService.Object);
    }

    [Test]
    public async Task Add_ReturnsNotFound_WhenWhiskyDoesNotExistOrIsNotApproved()
    {
        // Arrange
        var model = new ReviewFormModel
        {
            Title = "Test Review",
            Content = "This is a test review.",
            Recommend = true,
            WhiskyId = 1
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(false);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Add_ReturnsRedirectToAction_WhenAllDataIsValid()
    {
        // Arrange
        var model = new ReviewFormModel
        {
            Title = "Test Review",
            Content = "This is a test review.",
            Recommend = true,
            WhiskyId = 1
        };

        var userId = "TestUserId";

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(model.WhiskyId)).ReturnsAsync(true);
        mockReviewService.Setup(s => s.UserAlreadyReviewedWhiskyAsync(userId, model.WhiskyId)).ReturnsAsync(false);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", redirectResult.ActionName);
        Assert.AreEqual("Whisky", redirectResult.ControllerName);
        Assert.AreEqual(model.WhiskyId, redirectResult.RouteValues["id"]);
    }

    [Test]
    public async Task Add_ReturnsRedirectToEdit_WhenUserAlreadyReviewedWhisky()
    {
        // Arrange
        var model = new ReviewFormModel
        {
            Title = "Test Review",
            Content = "This is a test review.",
            Recommend = true,
            WhiskyId = 1
        };

        var userId = "TestUserId";
        var reviewId = 1;

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(model.WhiskyId)).ReturnsAsync(true);
        mockReviewService.Setup(s => s.UserAlreadyReviewedWhiskyAsync(userId, model.WhiskyId)).ReturnsAsync(true);
        mockReviewService.Setup(s => s.GetReviewIdAsync(userId, model.WhiskyId)).ReturnsAsync(reviewId);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Edit", redirectResult.ActionName);
        Assert.AreEqual(reviewId, redirectResult.RouteValues["id"]);
    }
    [Test]
    public async Task Edit_ReturnsViewResultWithCorrectModel_WhenAllDataIsValid()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        var review = new EditReviewFormModel
        {
            Title = "Test Review",
            Content = "This is a test review.",
            Recommend = true,
            WhiskyId = 1,
            UserId = userId,
            Id = id
        };

        mockReviewService.Setup(s => s.GetReviewAsync(id)).ReturnsAsync(review);
        mockWhiskyService.Setup(s => s.WhiskyExistAsync(review.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(review.WhiskyId)).ReturnsAsync(true);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
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
        Assert.IsInstanceOf<ReviewFormModel>(viewResult.Model);
        var model = viewResult.Model as ReviewFormModel;
        Assert.AreEqual(review.Title, model.Title);
        Assert.AreEqual(review.Content, model.Content);
        Assert.AreEqual(review.Recommend, model.Recommend);
        Assert.AreEqual(review.WhiskyId, model.WhiskyId);
    }

    [Test]
    public async Task Edit_ReturnsNotFound_WhenReviewDoesNotExist()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        mockReviewService.Setup(s => s.GetReviewAsync(id)).ReturnsAsync((EditReviewFormModel)null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
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
    public async Task EditPost_ReturnsNotFound_WhenReviewDoesNotExist()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        mockReviewService.Setup(s => s.GetReviewAsync(id)).ReturnsAsync((EditReviewFormModel)null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new ReviewFormModel
        {
            Title = "Test Review",
            Content = "This is a test review.",
            Recommend = true,
            WhiskyId = 1
        };

        // Act
        var result = await controller.Edit(id, model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Edit_ReturnsRedirectToAction_WhenAllDataIsValid()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        var review = new EditReviewFormModel
        {
            Title = "Test Review",
            Content = "This is a test review.",
            Recommend = true,
            WhiskyId = 1,
            UserId = userId,
            Id = id
        };

        mockReviewService.Setup(s => s.GetReviewAsync(id)).ReturnsAsync(review);
        mockWhiskyService.Setup(s => s.WhiskyExistAsync(review.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(review.WhiskyId)).ReturnsAsync(true);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new ReviewFormModel
        {
            Title = "Test Review",
            Content = "This is a test review.",
            Recommend = true,
            WhiskyId = 1
        };

        // Act
        var result = await controller.Edit(id, model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", redirectResult.ActionName);
        Assert.AreEqual("Whisky", redirectResult.ControllerName);
        Assert.AreEqual(model.WhiskyId, redirectResult.RouteValues["id"]);
    }
    [Test]
    public async Task Delete_ReturnsNotFound_WhenReviewDoesNotExist()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        mockReviewService.Setup(s => s.GetReviewAsync(id)).ReturnsAsync((EditReviewFormModel)null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Delete_ReturnsRedirectToAction_WhenAllDataIsValid()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        var review = new EditReviewFormModel
        {
            Title = "Test Review",
            Content = "This is a test review.",
            Recommend = true,
            WhiskyId = 1,
            UserId = userId,
            Id = id
        };

        mockReviewService.Setup(s => s.GetReviewAsync(id)).ReturnsAsync(review);
        mockWhiskyService.Setup(s => s.WhiskyExistAsync(review.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(review.WhiskyId)).ReturnsAsync(true);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "User")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", redirectResult.ActionName);
        Assert.AreEqual("Whisky", redirectResult.ControllerName);
        Assert.AreEqual(review.WhiskyId, redirectResult.RouteValues["id"]);
    }
    [Test]
    public async Task MyReviews_ReturnsViewResultWithCorrectModel_WhenCalled()
    {
        // Arrange
        var userId = "TestUserId";

        var reviews = new List<MyReviewModel>
    {
        new MyReviewModel
        {
            Id = 1,
            Title = "Test Review 1",
            Content = "This is a test review 1.",
            Recommend = true,
            WhiskyId = 1,
            WhiskyName = "Test Whisky 1"
        },
        new MyReviewModel
        {
            Id = 2,
            Title = "Test Review 2",
            Content = "This is a test review 2.",
            Recommend = false,
            WhiskyId = 2,
            WhiskyName = "Test Whisky 2"
        }
    };

        mockReviewService.Setup(s => s.AllUserReviewsAsync(userId)).ReturnsAsync(reviews);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.MyReviews();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<MyReviewModel>>(viewResult.Model);
        var model = viewResult.Model as List<MyReviewModel>;
        Assert.AreEqual(reviews, model);
    }

}
