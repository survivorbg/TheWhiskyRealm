using Microsoft.AspNetCore.Mvc;
using Moq;
using TheWhiskyRealm.Areas.Admin.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Whisky;
using TheWhiskyRealm.Core.Models.Whisky.Add;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class ForApproveControllerUnitTests
{
    private Mock<IWhiskyService> mockWhiskyService;
    private ForApprove controller;

    [SetUp]
    public void SetUp()
    {
        mockWhiskyService = new Mock<IWhiskyService>();
        controller = new ForApprove(mockWhiskyService.Object);
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
    [Test]
    public async Task Index_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var whiskies = new List<AllWhiskyModel>
    {
        new AllWhiskyModel { Id = 1, Name = "Test Whisky 1" },
        new AllWhiskyModel { Id = 2, Name = "Test Whisky 2" }
    };

        mockWhiskyService.Setup(s => s.GetAllWhiskiesForApproveAsync()).ReturnsAsync(whiskies);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<AllWhiskyModel>>(viewResult.Model);
        var model = viewResult.Model as List<AllWhiskyModel>;
        CollectionAssert.AreEquivalent(whiskies, model);
    }

}
