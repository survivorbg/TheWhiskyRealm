using Microsoft.AspNetCore.Mvc;
using Moq;
using TheWhiskyRealm.Areas.Admin.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
using TheWhiskyRealm.Core.Models.AdminArea.Whisky;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class DistilleryControllerUnitTests
{
    private Mock<IDistilleryService> mockDistilleryService;
    private Mock<IWhiskyService> mockWhiskyService;
    private Mock<IRegionService> mockRegionService;
    private DistilleryController controller;

    [SetUp]
    public void SetUp()
    {
        mockDistilleryService = new Mock<IDistilleryService>();
        mockWhiskyService = new Mock<IWhiskyService>();
        mockRegionService = new Mock<IRegionService>();
        controller = new DistilleryController(mockDistilleryService.Object, mockWhiskyService.Object, mockRegionService.Object);
    }

    [Test]
    public async Task Index_WithDefaultParameters_ReturnsCorrectViewResult()
    {
        // Arrange
        var currentPage = 1;
        var pageSize = 20;
        var sortOrder = "";
        var totalDistilleries = 100;
        var distilleries = new List<DistilleryViewModel>
    {
        new DistilleryViewModel { Id = 1, YearFounded = 2000, Name = "Test Distillery 1", Region = "Test Region 1", Country = "Test Country 1" },
        new DistilleryViewModel { Id = 2, YearFounded = 2001, Name = "Test Distillery 2", Region = "Test Region 2", Country = "Test Country 2" }
    };
        var expectedModel = new DistilleryIndexViewModel
        {
            Distilleries = distilleries,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalDistilleries / (double)pageSize)
        };

        mockDistilleryService.Setup(s => s.GetTotalDistilleriesAsync()).ReturnsAsync(totalDistilleries);
        mockDistilleryService.Setup(s => s.GetAllDistilleriesAsync(currentPage, pageSize, sortOrder)).ReturnsAsync(distilleries);

        // Act
        var result = await controller.Index(currentPage, pageSize, sortOrder);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<DistilleryIndexViewModel>(viewResult.Model);
        var model = viewResult.Model as DistilleryIndexViewModel;
        Assert.AreEqual(expectedModel.CurrentPage, model.CurrentPage);
        Assert.AreEqual(expectedModel.TotalPages, model.TotalPages);
        CollectionAssert.AreEquivalent(expectedModel.Distilleries, model.Distilleries);
    }
    [Test]
    public async Task Info_WithValidId_ReturnsCorrectViewResult()
    {
        // Arrange
        var distilleryId = 1;
        var distilleryName = "Test Distillery";
        var regionName = "Test Region";
        var countryName = "Test Country";
        var yearFounded = 2000;
        var whiskies = new List<WhiskyDistilleryViewModel>
    {
        new WhiskyDistilleryViewModel { Id = 1, Name = "Test Whisky 1", IsApproved = "Yes" },
        new WhiskyDistilleryViewModel { Id = 2, Name = "Test Whisky 2", IsApproved = "No" }
    };
        var distillery = new DistilleryInfoModel { Id = distilleryId, Name = distilleryName, Region = regionName, Country = countryName, YearFounded = yearFounded, Whiskies = whiskies };
        var expectedModel = new DistilleryInfoModel { Id = distilleryId, Name = distilleryName, Region = regionName, Country = countryName, YearFounded = yearFounded, Whiskies = whiskies };

        mockDistilleryService.Setup(s => s.GetDistilleryInfoAsync(distilleryId)).ReturnsAsync(distillery);
        mockWhiskyService.Setup(s => s.GetWhiskiesByDistilleryIdAsync(distilleryId)).ReturnsAsync(whiskies);

        // Act
        var result = await controller.Info(distilleryId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<DistilleryInfoModel>(viewResult.Model);
        var model = viewResult.Model as DistilleryInfoModel;
        Assert.AreEqual(expectedModel.Id, model.Id);
        Assert.AreEqual(expectedModel.Name, model.Name);
        Assert.AreEqual(expectedModel.Region, model.Region);
        Assert.AreEqual(expectedModel.Country, model.Country);
        Assert.AreEqual(expectedModel.YearFounded, model.YearFounded);
        CollectionAssert.AreEquivalent(expectedModel.Whiskies, model.Whiskies);
    }
    [Test]
    public async Task Info_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var distilleryId = 1;

        mockDistilleryService.Setup(s => s.GetDistilleryInfoAsync(distilleryId)).ReturnsAsync((DistilleryInfoModel)null);

        // Act
        var result = await controller.Info(distilleryId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Add_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var regions = new List<RegionViewModel>
    {
        new RegionViewModel { Id = 1, Name = "Test Region 1", CountryName = "Test Country 1" },
        new RegionViewModel { Id = 2, Name = "Test Region 2", CountryName = "Test Country 2" }
    };
        var expectedModel = new DistilleryFormViewModel { Regions = regions };

        mockRegionService.Setup(s => s.GetAllRegionsAsync()).ReturnsAsync(regions);

        // Act
        var result = await controller.Add();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<DistilleryFormViewModel>(viewResult.Model);
        var model = viewResult.Model as DistilleryFormViewModel;
        CollectionAssert.AreEquivalent(expectedModel.Regions, model.Regions);
    }

    [Test]
    public async Task Add_WithValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var distilleryFormViewModel = new DistilleryFormViewModel
        {
            Id = 1,
            Name = "Test Distillery",
            YearFounded = 2000,
            RegionId = 1
        };

        mockDistilleryService.Setup(s => s.DistilleryExistByName(distilleryFormViewModel.Name, 0)).ReturnsAsync(false);
        mockDistilleryService.Setup(s => s.AddDistilleryAsync(distilleryFormViewModel)).ReturnsAsync(distilleryFormViewModel.Id);

        // Act
        var result = await controller.Add(distilleryFormViewModel);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("Info", redirectToActionResult.ActionName);
        Assert.AreEqual("Distillery", redirectToActionResult.ControllerName);
        Assert.AreEqual(distilleryFormViewModel.Id, redirectToActionResult.RouteValues["id"]);
    }

    [Test]
    public async Task Add_WithInvalidModel_ReturnsViewResultWithModel()
    {
        // Arrange
        var distilleryFormViewModel = new DistilleryFormViewModel
        {
            Id = 1,
            Name = "Test Distillery",
            YearFounded = 2000,
            RegionId = 1
        };
        var regions = new List<RegionViewModel>
    {
        new RegionViewModel { Id = 1, Name = "Test Region 1", CountryName = "Test Country 1" },
        new RegionViewModel { Id = 2, Name = "Test Region 2", CountryName = "Test Country 2" }
    };
        var expectedModel = new DistilleryFormViewModel { Id = distilleryFormViewModel.Id, Name = distilleryFormViewModel.Name, YearFounded = distilleryFormViewModel.YearFounded, RegionId = distilleryFormViewModel.RegionId, Regions = regions };

        mockDistilleryService.Setup(s => s.DistilleryExistByName(distilleryFormViewModel.Name,0)).ReturnsAsync(true);
        mockRegionService.Setup(s => s.GetAllRegionsAsync()).ReturnsAsync(regions);

        // Act
        var result = await controller.Add(distilleryFormViewModel);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<DistilleryFormViewModel>(viewResult.Model);
        var model = viewResult.Model as DistilleryFormViewModel;
        Assert.AreEqual(expectedModel.Id, model.Id);
        Assert.AreEqual(expectedModel.Name, model.Name);
        Assert.AreEqual(expectedModel.YearFounded, model.YearFounded);
        Assert.AreEqual(expectedModel.RegionId, model.RegionId);
        CollectionAssert.AreEquivalent(expectedModel.Regions, model.Regions);
    }

    [Test]
    public async Task Edit_WithValidId_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var distilleryId = 1;
        var distilleryFormViewModel = new DistilleryFormViewModel
        {
            Id = distilleryId,
            Name = "Test Distillery",
            YearFounded = 2000,
            RegionId = 1
        };
        var regions = new List<RegionViewModel>
    {
        new RegionViewModel { Id = 1, Name = "Test Region 1", CountryName = "Test Country 1" },
        new RegionViewModel { Id = 2, Name = "Test Region 2", CountryName = "Test Country 2" }
    };
        var expectedModel = new DistilleryFormViewModel { Id = distilleryId, Name = "Test Distillery", YearFounded = 2000, RegionId = 1, Regions = regions };

        mockDistilleryService.Setup(s => s.GetDistilleryByIdAsync(distilleryId)).ReturnsAsync(distilleryFormViewModel);
        mockRegionService.Setup(s => s.GetAllRegionsAsync()).ReturnsAsync(regions);

        // Act
        var result = await controller.Edit(distilleryId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<DistilleryFormViewModel>(viewResult.Model);
        var model = viewResult.Model as DistilleryFormViewModel;
        Assert.AreEqual(expectedModel.Id, model.Id);
        Assert.AreEqual(expectedModel.Name, model.Name);
        Assert.AreEqual(expectedModel.YearFounded, model.YearFounded);
        Assert.AreEqual(expectedModel.RegionId, model.RegionId);
        CollectionAssert.AreEquivalent(expectedModel.Regions, model.Regions);
    }

    [Test]
    public async Task Edit_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var distilleryId = 1;

        mockDistilleryService.Setup(s => s.GetDistilleryByIdAsync(distilleryId)).ReturnsAsync((DistilleryFormViewModel)null);

        // Act
        var result = await controller.Edit(distilleryId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Edit_WithValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var distilleryFormViewModel = new DistilleryFormViewModel
        {
            Id = 1,
            Name = "Test Distillery",
            YearFounded = 2000,
            RegionId = 1
        };

        mockDistilleryService.Setup(s => s.GetDistilleryByIdAsync(distilleryFormViewModel.Id)).ReturnsAsync(distilleryFormViewModel);
        mockDistilleryService.Setup(s => s.DistilleryExistByName(distilleryFormViewModel.Name, distilleryFormViewModel.Id)).ReturnsAsync(false);
        mockRegionService.Setup(s => s.RegionExistsAsync(distilleryFormViewModel.RegionId)).ReturnsAsync(true);

        // Act
        var result = await controller.Edit(distilleryFormViewModel);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("Info", redirectToActionResult.ActionName);
        Assert.AreEqual("Distillery", redirectToActionResult.ControllerName);
        Assert.AreEqual(distilleryFormViewModel.Id, redirectToActionResult.RouteValues["id"]);
    }

    [Test]
    public async Task Edit_WithInvalidModel_ReturnsViewResultWithModel()
    {
        // Arrange
        var distilleryFormViewModel = new DistilleryFormViewModel
        {
            Id = 1,
            Name = "Test Distillery",
            YearFounded = 2000,
            RegionId = 1
        };
        var regions = new List<RegionViewModel>
    {
        new RegionViewModel { Id = 1, Name = "Test Region 1", CountryName = "Test Country 1" },
        new RegionViewModel { Id = 2, Name = "Test Region 2", CountryName = "Test Country 2" }
    };
        var expectedModel = new DistilleryFormViewModel { Id = distilleryFormViewModel.Id, Name = distilleryFormViewModel.Name, YearFounded = distilleryFormViewModel.YearFounded, RegionId = distilleryFormViewModel.RegionId, Regions = regions };

        mockDistilleryService.Setup(s => s.GetDistilleryByIdAsync(distilleryFormViewModel.Id)).ReturnsAsync(distilleryFormViewModel);
        mockDistilleryService.Setup(s => s.DistilleryExistByName(distilleryFormViewModel.Name, distilleryFormViewModel.Id)).ReturnsAsync(true);
        mockRegionService.Setup(s => s.GetAllRegionsAsync()).ReturnsAsync(regions);
        mockRegionService.Setup(s => s.RegionExistsAsync(distilleryFormViewModel.RegionId)).ReturnsAsync(true);

        // Act
        var result = await controller.Edit(distilleryFormViewModel);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<DistilleryFormViewModel>(viewResult.Model);
        var model = viewResult.Model as DistilleryFormViewModel;
        Assert.AreEqual(expectedModel.Id, model.Id);
        Assert.AreEqual(expectedModel.Name, model.Name);
        Assert.AreEqual(expectedModel.YearFounded, model.YearFounded);
        Assert.AreEqual(expectedModel.RegionId, model.RegionId);
        CollectionAssert.AreEquivalent(expectedModel.Regions, model.Regions);
    }

    [Test]
    public async Task Edit_WithNullModel_ReturnsBadRequestResult()
    {
        // Act
        var result = await controller.Edit(null);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task Edit_WithInvalidDistilleryId_ReturnsNotFoundResult()
    {
        // Arrange
        var distilleryFormViewModel = new DistilleryFormViewModel { Id = 1 };

        mockDistilleryService.Setup(s => s.GetDistilleryByIdAsync(distilleryFormViewModel.Id)).ReturnsAsync((DistilleryFormViewModel)null);

        // Act
        var result = await controller.Edit(distilleryFormViewModel);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_WithInvalidRegionId_ReturnsBadRequestResult()
    {
        // Arrange
        var distilleryFormViewModel = new DistilleryFormViewModel { Id = 1, RegionId = 1 };

        mockDistilleryService.Setup(s => s.GetDistilleryByIdAsync(distilleryFormViewModel.Id)).ReturnsAsync(distilleryFormViewModel);
        mockRegionService.Setup(s => s.RegionExistsAsync(distilleryFormViewModel.RegionId)).ReturnsAsync(false);

        // Act
        var result = await controller.Edit(distilleryFormViewModel);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

}
