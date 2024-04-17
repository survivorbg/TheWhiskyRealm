using Microsoft.AspNetCore.Mvc;
using Moq;
using TheWhiskyRealm.Areas.Admin.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Models.AdminArea.Distillery;
using TheWhiskyRealm.Core.Models.AdminArea.Region;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class RegionControllerUnitTests
{
    private Mock<IRegionService> mockRegionService;
    private Mock<ICountryService> mockCountryService;
    private Mock<IDistilleryService> mockDistilleryService;
    private RegionController controller;

    [SetUp]
    public void SetUp()
    {
        mockRegionService = new Mock<IRegionService>();
        mockCountryService = new Mock<ICountryService>();
        mockDistilleryService = new Mock<IDistilleryService>();
        controller = new RegionController(mockRegionService.Object, mockCountryService.Object, mockDistilleryService.Object);
    }

    [Test]
    public async Task Add_WithValidCountryId_ReturnsCorrectViewResult()
    {
        // Arrange
        int? countryId = 1;
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        var expectedModel = new AddRegionViewModel
        {
            CountryId = countryId,
            Countries = countries
        };

        mockCountryService.Setup(s => s.CountryExistsAsync((int)countryId)).ReturnsAsync(true);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await controller.Add(countryId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<AddRegionViewModel>(viewResult.Model);
        var model = viewResult.Model as AddRegionViewModel;
        Assert.AreEqual(expectedModel.CountryId, model.CountryId);
        CollectionAssert.AreEquivalent(expectedModel.Countries, model.Countries);
    }

    [Test]
    public async Task Add_WithInvalidCountryId_ReturnsBadRequest()
    {
        // Arrange
        int? countryId = 1;

        mockCountryService.Setup(s => s.CountryExistsAsync((int)countryId)).ReturnsAsync(false);

        // Act
        var result = await controller.Add(countryId);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task Add_WithNullCountryId_ReturnsCorrectViewResult()
    {
        // Arrange
        int? countryId = null;
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        var expectedModel = new AddRegionViewModel
        {
            CountryId = countryId,
            Countries = countries
        };

        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await controller.Add(countryId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<AddRegionViewModel>(viewResult.Model);
        var model = viewResult.Model as AddRegionViewModel;
        Assert.AreEqual(expectedModel.CountryId, model.CountryId);
        CollectionAssert.AreEquivalent(expectedModel.Countries, model.Countries);
    }
    [Test]
    public async Task Add_WithValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new AddRegionViewModel
        {
            Name = "Test Region",
            CountryId = 1
        };
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        mockRegionService.Setup(s => s.RegionWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId, 0)).ReturnsAsync(false);
        mockCountryService.Setup(s => s.CountryExistsAsync((int)model.CountryId)).ReturnsAsync(true);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);
        mockRegionService.Setup(s => s.AddRegionAsync(model.Name, (int)model.CountryId)).ReturnsAsync(1);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Info", redirectResult.ActionName);
        Assert.AreEqual("Region", redirectResult.ControllerName);
        Assert.AreEqual(1, redirectResult.RouteValues["id"]);
    }

    [Test]
    public async Task Add_WithInvalidModel_ReturnsViewResult()
    {
        // Arrange
        var model = new AddRegionViewModel
        {
            Name = "Test Region",
            CountryId = 1
        };
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        mockRegionService.Setup(s => s.RegionWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId, 0)).ReturnsAsync(true);
        mockCountryService.Setup(s => s.CountryExistsAsync((int)model.CountryId)).ReturnsAsync(true);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<AddRegionViewModel>(viewResult.Model);
        var returnedModel = viewResult.Model as AddRegionViewModel;
        Assert.AreEqual(model.Name, returnedModel.Name);
        Assert.AreEqual(model.CountryId, returnedModel.CountryId);
        CollectionAssert.AreEquivalent(countries, returnedModel.Countries);
    }
    [Test]
    public async Task Index_WithDefaultParameters_ReturnsCorrectViewResult()
    {
        // Arrange
        int currentPage = 1;
        int pageSize = 15;
        int totalRegions = 100;
        var regions = new List<RegionViewModel>()
        {

        new RegionViewModel { Id = 1, Name = "Test Region 1", CountryName = "Test Country 1" },
        new RegionViewModel { Id = 2, Name = "Test Region 2", CountryName = "Test Country 1" },
        new RegionViewModel { Id = 3, Name = "Test Region 3", CountryName = "Test Country 2" }
        };

        var expectedModel = new RegionIndexViewModel
        {
            Regions = regions,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalRegions / (double)pageSize)
        };

        mockRegionService.Setup(s => s.GetTotalRegionsAsync()).ReturnsAsync(totalRegions);
        mockRegionService.Setup(s => s.GetAllRegionsAsync(currentPage, pageSize)).ReturnsAsync(regions);

        // Act
        var result = await controller.Index(currentPage, pageSize);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<RegionIndexViewModel>(viewResult.Model);
        var model = viewResult.Model as RegionIndexViewModel;
        Assert.AreEqual(expectedModel.CurrentPage, model.CurrentPage);
        Assert.AreEqual(expectedModel.TotalPages, model.TotalPages);
        CollectionAssert.AreEquivalent(expectedModel.Regions, model.Regions);
    }
    [Test]
    public async Task Edit_WithValidId_ReturnsCorrectViewResult()
    {
        // Arrange
        int id = 1;
        var region = new EditRegionViewModel
        {
            Id = id,
            Name = "Test Region",
            CountryId = 1
        };
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        mockRegionService.Setup(s => s.GetRegionByIdAsync(id)).ReturnsAsync(region);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<EditRegionViewModel>(viewResult.Model);
        var model = viewResult.Model as EditRegionViewModel;
        Assert.AreEqual(region.Id, model.Id);
        Assert.AreEqual(region.Name, model.Name);
        Assert.AreEqual(region.CountryId, model.CountryId);
        CollectionAssert.AreEquivalent(countries, model.Countries);
    }

    [Test]
    public async Task Edit_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        int id = 1;

        mockRegionService.Setup(s => s.GetRegionByIdAsync(id)).ReturnsAsync((EditRegionViewModel)null);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Edit_WithValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new EditRegionViewModel
        {
            Id = 1,
            Name = "Test Region",
            CountryId = 1,
            Countries = new List<CountryViewModel>
        {
            new CountryViewModel { Id = 1, Name = "Test Country" }
        }
        };

        mockRegionService.Setup(s => s.GetRegionByIdAsync(model.Id)).ReturnsAsync(model);
        mockCountryService.Setup(s => s.CountryExistsAsync(model.CountryId)).ReturnsAsync(true);
        mockRegionService.Setup(s => s.RegionWithThisNameAndCountryExistsAsync(model.Name, model.CountryId, model.Id)).ReturnsAsync(false);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(model.Countries);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Info", redirectResult.ActionName);
        Assert.AreEqual("Region", redirectResult.ControllerName);
        Assert.AreEqual(model.Id, redirectResult.RouteValues["id"]);
    }

    [Test]
    public async Task Edit_WithInvalidModel_ReturnsViewResult()
    {
        // Arrange
        var model = new EditRegionViewModel
        {
            Id = 1,
            Name = "Test Region",
            CountryId = 1,
            Countries = new List<CountryViewModel>
        {
            new CountryViewModel { Id = 1, Name = "Test Country" }
        }
        };

        mockRegionService.Setup(s => s.GetRegionByIdAsync(model.Id)).ReturnsAsync(model);
        mockCountryService.Setup(s => s.CountryExistsAsync(model.CountryId)).ReturnsAsync(true);
        mockRegionService.Setup(s => s.RegionWithThisNameAndCountryExistsAsync(model.Name, model.CountryId, model.Id)).ReturnsAsync(true);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(model.Countries);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<EditRegionViewModel>(viewResult.Model);
        var returnedModel = viewResult.Model as EditRegionViewModel;
        Assert.AreEqual(model.Id, returnedModel.Id);
        Assert.AreEqual(model.Name, returnedModel.Name);
        Assert.AreEqual(model.CountryId, returnedModel.CountryId);
        CollectionAssert.AreEquivalent(model.Countries, returnedModel.Countries);
    }
    [Test]
    public async Task Edit_WithNullModel_ReturnsBadRequest()
    {
        // Arrange
        EditRegionViewModel model = null;

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task Edit_WithNonExistentRegion_ReturnsNotFoundResult()
    {
        // Arrange
        var model = new EditRegionViewModel
        {
            Id = 1,
            Name = "Test Region",
            CountryId = 1,
            Countries = new List<CountryViewModel>
        {
            new CountryViewModel { Id = 1, Name = "Test Country" }
        }
        };

        mockRegionService.Setup(s => s.GetRegionByIdAsync(model.Id)).ReturnsAsync((EditRegionViewModel)null);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Info_WithValidId_ReturnsCorrectViewResult()
    {
        // Arrange
        int id = 1;
        var region = new EditRegionViewModel
        {
            Id = id,
            Name = "Test Region",
            CountryId = 1
        };
        var distilleries = new List<DistilleryRegionViewModel>
    {
        new DistilleryRegionViewModel { Id = 1, Name = "Test Distillery 1", YearFounded = 2000 },
        new DistilleryRegionViewModel { Id = 2, Name = "Test Distillery 2", YearFounded = 2005 }
    };

        var expectedModel = new RegionInfoViewModel
        {
            Id = id,
            Name = region.Name,
            Distilleries = distilleries
        };

        mockRegionService.Setup(s => s.GetRegionByIdAsync(id)).ReturnsAsync(region);
        mockDistilleryService.Setup(s => s.GetAllDistilleriesAsync(id)).ReturnsAsync(distilleries);

        // Act
        var result = await controller.Info(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<RegionInfoViewModel>(viewResult.Model);
        var model = viewResult.Model as RegionInfoViewModel;
        Assert.AreEqual(expectedModel.Id, model.Id);
        Assert.AreEqual(expectedModel.Name, model.Name);
        CollectionAssert.AreEquivalent(expectedModel.Distilleries, model.Distilleries);
    }


    [Test]
    public async Task Info_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        int id = 1;

        mockRegionService.Setup(s => s.GetRegionByIdAsync(id)).ReturnsAsync((EditRegionViewModel)null);

        // Act
        var result = await controller.Info(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

}
