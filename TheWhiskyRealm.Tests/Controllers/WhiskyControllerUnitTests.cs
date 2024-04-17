using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TheWhiskyRealm.Areas.Admin.Controllers;
using TheWhiskyRealm.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class WhiskyControllerUnitTests
{
    private Mock<IWhiskyService> mockWhiskyService;
    private Mock<IWhiskyTypeService> mockWhiskyTypeService;
    private Mock<IRegionService> mockRegionService;
    private Mock<IDistilleryService> mockDistilleryService;
    private Mock<IReviewService> mockReviewService;
    private Mock<IAwardService> mockAwardService;
    private WhiskyController controller;

    [SetUp]
    public void SetUp()
    {
        mockWhiskyService = new Mock<IWhiskyService>();
        mockWhiskyTypeService = new Mock<IWhiskyTypeService>();
        mockRegionService = new Mock<IRegionService>();
        mockDistilleryService = new Mock<IDistilleryService>();
        mockReviewService = new Mock<IReviewService>();
        mockAwardService = new Mock<IAwardService>();
        controller = new WhiskyController(mockWhiskyService.Object, mockWhiskyTypeService.Object, mockRegionService.Object, mockDistilleryService.Object, mockReviewService.Object, mockAwardService.Object);
    }

    [Test]
    public async Task All_ReturnsCorrectViewResult()
    {
        // Arrange
        var whiskies = new List<AllWhiskyModel>
    {
        new AllWhiskyModel { Id = 1, Name = "Test Whisky 1", Age = 12, AlcoholPercentage = 40.0, WhiskyType = "Type 1", Reviews = 10, ImageURL = "url1" },
        new AllWhiskyModel { Id = 2, Name = "Test Whisky 2", Age = 15, AlcoholPercentage = 43.0, WhiskyType = "Type 2", Reviews = 20, ImageURL = "url2" },
    };

        mockWhiskyService.Setup(s => s.GetPagedWhiskiesAsync(0, 9)).ReturnsAsync(whiskies);

        // Act
        var result = await controller.All();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<AllWhiskyModel>>(viewResult.Model);
        var model = viewResult.Model as List<AllWhiskyModel>;
        CollectionAssert.AreEquivalent(whiskies, model);
    }

    [Test]
    public async Task LoadMoreWhiskies_ReturnsCorrectPartialViewResult()
    {
        // Arrange
        var skip = 0;
        var take = 9;
        var sortOrder = "";
        var whiskies = new List<AllWhiskyModel>
    {
        new AllWhiskyModel { Id = 1, Name = "Test Whisky 1", Age = 12, AlcoholPercentage = 40.0, WhiskyType = "Type 1", Reviews = 10, ImageURL = "url1" },
        new AllWhiskyModel { Id = 2, Name = "Test Whisky 2", Age = 15, AlcoholPercentage = 43.0, WhiskyType = "Type 2", Reviews = 20, ImageURL = "url2" },
    };

        mockWhiskyService.Setup(s => s.GetPagedWhiskiesAsync(skip, take, sortOrder)).ReturnsAsync(whiskies);

        // Act
        var result = await controller.LoadMoreWhiskies(skip, take, sortOrder);

        // Assert
        Assert.IsInstanceOf<PartialViewResult>(result);
        var partialViewResult = result as PartialViewResult;
        Assert.AreEqual("_WhiskiesPartial", partialViewResult.ViewName);
        Assert.IsInstanceOf<List<AllWhiskyModel>>(partialViewResult.Model);
        var model = partialViewResult.Model as List<AllWhiskyModel>;
        CollectionAssert.AreEquivalent(whiskies, model);
    }
    [Test]
    public async Task Add_ReturnsCorrectViewResult()
    {
        // Arrange
        var whiskyTypes = new List<WhiskyTypeViewModel>
    {
        new WhiskyTypeViewModel { Id = 1, Name = "Type 1" },
        new WhiskyTypeViewModel { Id = 2, Name = "Type 2" },
        // Add more whisky types as needed
    };

        var distilleries = new List<DistilleryAddWhiskyViewModel>
    {
        new DistilleryAddWhiskyViewModel { DistilleryId = 1, Name = "Distillery 1", Country = "Country 1" },
        new DistilleryAddWhiskyViewModel { DistilleryId = 2, Name = "Distillery 2", Country = "Country 2" },
        // Add more distilleries as needed
    };

        mockWhiskyTypeService.Setup(s => s.GetAllWhiskyTypesAsync()).ReturnsAsync(whiskyTypes);
        mockDistilleryService.Setup(s => s.GetAllDistilleriesAsync()).ReturnsAsync(distilleries);

        // Act
        var result = await controller.Add();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<WhiskyFormModel>(viewResult.Model);
        var model = viewResult.Model as WhiskyFormModel;
        CollectionAssert.AreEquivalent(whiskyTypes, model.WhiskyTypes);
        CollectionAssert.AreEquivalent(distilleries, model.Distilleries);
    }

    [Test]
    public async Task Add_WithValidModelAndAdminUser_ReturnsRedirectToActionResult()
    {
        // Arrange
        var whisky = new WhiskyFormModel
        {
            Name = "Test Whisky",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "This is a test whisky.",
            ImageURL = "url1",
            IsApproved = true,
            PublishedBy = "Admin",
            DistilleryId = 1,
            WhiskyTypeId = 1,
            Distilleries = new List<DistilleryAddWhiskyViewModel>(),
            WhiskyTypes = new List<WhiskyTypeViewModel>()
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
        new Claim(ClaimTypes.Role, "Administrator"),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockDistilleryService.Setup(s => s.DistilleryExistsAsync(whisky.DistilleryId)).ReturnsAsync(true);
        mockWhiskyTypeService.Setup(s => s.WhiskyTypeExistsAsync(whisky.WhiskyTypeId)).ReturnsAsync(true);
        mockWhiskyTypeService.Setup(s => s.GetWhiskyTypeNameAsync(whisky.WhiskyTypeId)).ReturnsAsync("Bourbon");

        // Act
        var result = await controller.Add(whisky);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("All", redirectToActionResult.ActionName);
    }
    [Test]
    public async Task Edit_WithValidIdAndAdminUser_ReturnsCorrectViewResult()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "test-user-id";
        var whisky = new WhiskyFormModel
        {
            Name = "Test Whisky",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "This is a test whisky.",
            ImageURL = "url1",
            IsApproved = true,
            PublishedBy = userId,
            DistilleryId = 1,
            WhiskyTypeId = 1,
            Distilleries = new List<DistilleryAddWhiskyViewModel>(),
            WhiskyTypes = new List<WhiskyTypeViewModel>()
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "Administrator"),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.GetWhiskyByIdForEditAsync(whiskyId)).ReturnsAsync(whisky);

        // Act
        var result = await controller.Edit(whiskyId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<WhiskyFormModel>(viewResult.Model);
        var model = viewResult.Model as WhiskyFormModel;
        Assert.AreEqual(whisky.Name, model.Name);
        CollectionAssert.AreEquivalent(whisky.Distilleries, model.Distilleries);
        CollectionAssert.AreEquivalent(whisky.WhiskyTypes, model.WhiskyTypes);
    }
    [Test]
    public async Task Edit_WithValidModelAndAdminUser_ReturnsRedirectToActionResult()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "test-user-id";
        var whisky = new WhiskyFormModel
        {
            Name = "Test Whisky",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "This is a test whisky.",
            ImageURL = "url1",
            IsApproved = true,
            PublishedBy = userId,
            DistilleryId = 1,
            WhiskyTypeId = 1,
            Distilleries = new List<DistilleryAddWhiskyViewModel>(),
            WhiskyTypes = new List<WhiskyTypeViewModel>()
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "Administrator"),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(userId);
        mockDistilleryService.Setup(s => s.DistilleryExistsAsync(whisky.DistilleryId)).ReturnsAsync(true);
        mockWhiskyTypeService.Setup(s => s.WhiskyTypeExistsAsync(whisky.WhiskyTypeId)).ReturnsAsync(true);
        mockWhiskyTypeService.Setup(s => s.GetWhiskyTypeNameAsync(whisky.WhiskyTypeId)).ReturnsAsync("Bourbon");

        // Act
        var result = await controller.Edit(whiskyId, whisky);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(whiskyId, redirectToActionResult.RouteValues["id"]);
    }
    [Test]
    public async Task Delete_WithValidId_ReturnsCorrectViewResult()
    {
        // Arrange
        var whiskyId = 1;
        var whisky = new WhiskyFormModel
        {
            Name = "Test Whisky",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "This is a test whisky.",
            ImageURL = "url1",
            IsApproved = true,
            PublishedBy = "Admin",
            DistilleryId = 1,
            WhiskyTypeId = 1,
            Distilleries = new List<DistilleryAddWhiskyViewModel>(),
            WhiskyTypes = new List<WhiskyTypeViewModel>()
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.GetWhiskyByIdForEditAsync(whiskyId)).ReturnsAsync(whisky);

        // Act
        var result = await controller.Delete(whiskyId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<WhiskyFormModel>(viewResult.Model);
        var model = viewResult.Model as WhiskyFormModel;
        Assert.AreEqual(whisky.Name, model.Name);
        CollectionAssert.AreEquivalent(whisky.Distilleries, model.Distilleries);
        CollectionAssert.AreEquivalent(whisky.WhiskyTypes, model.WhiskyTypes);
    }
    [Test]
    public async Task Delete_WithValidId_ReturnsRedirectToActionResult()
    {
        // Arrange
        var whiskyId = 1;

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);

        // Act
        var result = await controller.Delete(whiskyId, new WhiskyFormModel());

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("All", redirectToActionResult.ActionName);
    }
    [Test]
    public async Task AddToFavourites_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "test-user-id";

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyInFavouritesAsync(userId, whiskyId)).ReturnsAsync(false);

        // Act
        var result = await controller.AddToFavourites(whiskyId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task AddToFavourites_WithWhiskyAlreadyInFavourites_ReturnsBadRequestResult()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "test-user-id";

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyInFavouritesAsync(userId, whiskyId)).ReturnsAsync(true);

        // Act
        var result = await controller.AddToFavourites(whiskyId);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }
    [Test]
    public async Task RemoveFromFavourites_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "test-user-id";

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyInFavouritesAsync(userId, whiskyId)).ReturnsAsync(true);

        // Act
        var result = await controller.RemoveFromFavourites(whiskyId);

        // Assert
        Assert.IsInstanceOf<OkResult>(result);
    }

    [Test]
    public async Task RemoveFromFavourites_WithWhiskyNotInFavourites_ReturnsBadRequestResult()
    {
        // Arrange
        var whiskyId = 1;
        var userId = "test-user-id";

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyIsApprovedAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.WhiskyInFavouritesAsync(userId, whiskyId)).ReturnsAsync(false);

        // Act
        var result = await controller.RemoveFromFavourites(whiskyId);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }
    [Test]
    public async Task Approve_WithValidId_ReturnsRedirectToActionResult()
    {
        // Arrange
        var whiskyId = 1;
        var whisky = new WhiskyFormModel
        {
            Name = "Test Whisky",
            Age = 12,
            AlcoholPercentage = 40.0,
            Description = "This is a test whisky.",
            ImageURL = "url1",
            IsApproved = false,
            PublishedBy = "Admin",
            DistilleryId = 1,
            WhiskyTypeId = 1,
            Distilleries = new List<DistilleryAddWhiskyViewModel>(),
            WhiskyTypes = new List<WhiskyTypeViewModel>()
        };

        mockWhiskyService.Setup(s => s.GetWhiskyByIdForEditAsync(whiskyId)).ReturnsAsync(whisky);

        // Act
        var result = await controller.Approve(whiskyId);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual(whiskyId, redirectToActionResult.RouteValues["id"]);
    }

    [Test]
    public async Task Approve_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var whiskyId = 1;

        mockWhiskyService.Setup(s => s.GetWhiskyByIdForEditAsync(whiskyId)).ReturnsAsync((WhiskyFormModel)null);

        // Act
        var result = await controller.Approve(whiskyId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

}
