using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TheWhiskyRealm.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Rating;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class RatingControllerUnitTests
{
    private Mock<IRatingService> mockRatingService;
    private Mock<IWhiskyService> mockWhiskyService;
    private RatingController controller;

    [SetUp]
    public void SetUp()
    {
        mockRatingService = new Mock<IRatingService>();
        mockWhiskyService = new Mock<IWhiskyService>();
        controller = new RatingController(mockRatingService.Object, mockWhiskyService.Object);
    }

    //[Test]
    //public async Task Rate_ReturnsCorrectRedirectToActionResult_WhenModelIsValidAndWhiskyExistsAndIsApproved()
    //{
    //    // Arrange
    //    var model = new RatingViewModel
    //    {
    //        Nose = 5,
    //        Taste = 5,
    //        Finish = 5,
    //        WhiskyId = 1
    //    };
    //    var userId = "TestUserId";

    //    mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(true);
    //    mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(model.WhiskyId)).ReturnsAsync(true);
    //    mockRatingService.Setup(s => s.RateAsync(userId, model)).Returns(Task.CompletedTask);

    //    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
    //    {
    //    new Claim(ClaimTypes.NameIdentifier, userId),
    //    }));

    //    controller.ControllerContext = new ControllerContext()
    //    {
    //        HttpContext = new DefaultHttpContext() { User = user }
    //    };

    //    // Act
    //    var result = await controller.Rate(model);

    //    // Assert
    //    Assert.IsInstanceOf<RedirectToActionResult>(result);
    //    var actionResult = result as RedirectToActionResult;
    //    Assert.AreEqual("Details", actionResult.ActionName);
    //    Assert.AreEqual("Whisky", actionResult.ControllerName);
    //    Assert.AreEqual(model.WhiskyId, actionResult.RouteValues["id"]);
    //}

    [Test]
    public async Task Rate_ReturnsNotFoundResult_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var model = new RatingViewModel
        {
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1
        };
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
{
    new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
}));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(false);

        // Act
        var result = await controller.Rate(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Rate_ReturnsNotFoundResult_WhenWhiskyIsNotApproved()
    {
        // Arrange
        var model = new RatingViewModel
        {
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1
        };

        var userId = "TestUserId";

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(model.WhiskyId)).ReturnsAsync(false);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Rate(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }


    [Test]
    public async Task Rate_ReturnsViewResultWithModel_WhenModelStateIsInvalid()
    {
        // Arrange
        var model = new RatingViewModel
        {
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1
        };

        var userId = "TestUserId";

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(model.WhiskyId)).ReturnsAsync(true);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        controller.ModelState.AddModelError("Error", "Test error");

        // Act
        var result = await controller.Rate(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

    [Test]
    public async Task Edit_ReturnsCorrectPartialViewResultWithModel_WhenRatingAndWhiskyExistAndUserIsAuthorized()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";
        var ratingEditViewModel = new RatingEditViewModel
        {
            Id = id,
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1,
            UserId = userId
        };

        mockRatingService.Setup(s => s.GetRatingAsync(userId, id)).ReturnsAsync(ratingEditViewModel);
        mockWhiskyService.Setup(s => s.WhiskyExistAsync(ratingEditViewModel.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(ratingEditViewModel.WhiskyId)).ReturnsAsync(true);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<PartialViewResult>(result);
        var partialViewResult = result as PartialViewResult;
        Assert.AreEqual("_RatingEditFormPartial", partialViewResult.ViewName);
        Assert.IsInstanceOf<RatingViewModel>(partialViewResult.Model);
        var model = partialViewResult.Model as RatingViewModel;
        Assert.AreEqual(id, model.WhiskyId);
        Assert.AreEqual(ratingEditViewModel.Nose, model.Nose);
        Assert.AreEqual(ratingEditViewModel.Taste, model.Taste);
        Assert.AreEqual(ratingEditViewModel.Finish, model.Finish);
    }

    [Test]
    public async Task Edit_ReturnsNotFoundResult_WhenRatingDoesNotExist()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        mockRatingService.Setup(s => s.GetRatingAsync(userId, id)).ReturnsAsync((RatingEditViewModel)null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
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
    public async Task Edit_ReturnsNotFoundResult_WhenWhiskyDoesNotExistOrIsNotApproved()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";
        var ratingEditViewModel = new RatingEditViewModel
        {
            Id = id,
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1,
            UserId = userId
        };

        mockRatingService.Setup(s => s.GetRatingAsync(userId, id)).ReturnsAsync(ratingEditViewModel);
        mockWhiskyService.Setup(s => s.WhiskyExistAsync(ratingEditViewModel.WhiskyId)).ReturnsAsync(false);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
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
    public async Task Update_ReturnsCorrectRedirectToActionResult_WhenModelIsValidAndWhiskyExistsAndIsApprovedAndRatingExists()
    {
        // Arrange
        var model = new RatingViewModel
        {
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1
        };
        var userId = "TestUserId";
        var ratingEditViewModel = new RatingEditViewModel
        {
            Id = 1,
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1,
            UserId = userId
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(model.WhiskyId)).ReturnsAsync(true);
        mockRatingService.Setup(s => s.GetRatingAsync(userId, model.WhiskyId)).ReturnsAsync(ratingEditViewModel);
        mockRatingService.Setup(s => s.EditRatingAsync(model, ratingEditViewModel.Id)).Returns(Task.CompletedTask);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Update(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", actionResult.ActionName);
        Assert.AreEqual("Whisky", actionResult.ControllerName);
        Assert.AreEqual(model.WhiskyId, actionResult.RouteValues["id"]);
    }

    [Test]
    public async Task Update_ReturnsNotFoundResult_WhenWhiskyDoesNotExistOrIsNotApproved()
    {
        // Arrange
        var model = new RatingViewModel
        {
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1
        };
        var userId = "TestUserId";

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(false);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Update(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Update_ReturnsNotFoundResult_WhenRatingDoesNotExist()
    {
        // Arrange
        var model = new RatingViewModel
        {
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1
        };
        var userId = "TestUserId";

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(model.WhiskyId)).ReturnsAsync(true);
        mockRatingService.Setup(s => s.GetRatingAsync(userId, model.WhiskyId)).ReturnsAsync((RatingEditViewModel)null);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Update(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Update_ReturnsRedirectToActionResult_WhenModelStateIsInvalid()
    {
        // Arrange
        var model = new RatingViewModel
        {
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1
        };
        var userId = "TestUserId";
        var ratingEditViewModel = new RatingEditViewModel
        {
            Id = 1,
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1,
            UserId = userId
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(model.WhiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(model.WhiskyId)).ReturnsAsync(true);
        mockRatingService.Setup(s => s.GetRatingAsync(userId, model.WhiskyId)).ReturnsAsync(ratingEditViewModel);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        controller.ModelState.AddModelError("Error", "Test error");

        // Act
        var result = await controller.Update(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", actionResult.ActionName);
        Assert.AreEqual("Whisky", actionResult.ControllerName);
        Assert.AreEqual(model.WhiskyId, actionResult.RouteValues["id"]);
    }
    [Test]
    public async Task MyRatings_ReturnsCorrectViewResultWithModel_WhenUserHasRatings()
    {
        // Arrange
        var userId = "TestUserId";
        var myRatingViewModels = new List<MyRatingViewModel>
    {
        new MyRatingViewModel
        {
            Nose = 5,
            Taste = 5,
            Finish = 5,
            WhiskyId = 1,
            WhiskyName = "Test Whisky"
        }
    };

        mockRatingService.Setup(s => s.GetRatingsByUserAsync(userId)).ReturnsAsync(myRatingViewModels);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.MyRatings();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(myRatingViewModels, viewResult.Model);
    }

}
