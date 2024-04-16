using Microsoft.AspNetCore.Mvc;
using Moq;
using TheWhiskyRealm.Areas.Admin.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Models.AdminArea.Region;


namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class CountryControllerUnitTests
{
    private Mock<ICountryService> mockCountryService;
    private Mock<IRegionService> mockRegionService;
    private CountryController controller;

    [SetUp]
    public void SetUp()
    {
        mockCountryService = new Mock<ICountryService>();
        mockRegionService = new Mock<IRegionService>();
        controller = new CountryController(mockCountryService.Object, mockRegionService.Object);
    }

    [Test]
    public async Task Info_WithValidId_ReturnsCorrectViewResult()
    {
        // Arrange
        var countryId = 1;
        var countryName = "Test Country";
        var regions = new List<RegionCountryViewModel>();
        var country = new CountryViewModel { Id = countryId, Name = countryName };
        var expectedModel = new CountryInfoViewModel { Id = countryId, Name = countryName, Regions = regions };

        mockCountryService.Setup(s => s.GetByIdAsync(countryId)).ReturnsAsync(country);
        mockRegionService.Setup(s => s.GetAllRegionsByCountryIdAsync(countryId)).ReturnsAsync(regions);

        // Act
        var result = await controller.Info(countryId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CountryInfoViewModel>(viewResult.Model);
        var model = viewResult.Model as CountryInfoViewModel;
        Assert.AreEqual(expectedModel.Id, model.Id);
        Assert.AreEqual(expectedModel.Name, model.Name);
        CollectionAssert.AreEquivalent(expectedModel.Regions, model.Regions);
    }

    [Test]
    public async Task Info_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var countryId = 1;

        mockCountryService.Setup(s => s.GetByIdAsync(countryId)).ReturnsAsync((CountryViewModel)null);

        // Act
        var result = await controller.Info(countryId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Index_WithValidParameters_ReturnsCorrectViewResult()
    {
        // Arrange
        var currentPage = 1;
        var pageSize = 10;
        var totalCountries = 5;
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Country 1" },
        new CountryViewModel { Id = 2, Name = "Country 2" },
    };
        var expectedModel = new CountryIndexViewModel
        {
            Countries = countries,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalCountries / (double)pageSize),
            PageSize = pageSize
        };

        mockCountryService.Setup(s => s.GetTotalCountriesAsync()).ReturnsAsync(totalCountries);
        mockCountryService.Setup(s => s.GetAllCountriesAsync(currentPage, pageSize)).ReturnsAsync(countries);

        // Act
        var result = await controller.Index(currentPage, pageSize);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CountryIndexViewModel>(viewResult.Model);
        var model = viewResult.Model as CountryIndexViewModel;
        Assert.AreEqual(expectedModel.CurrentPage, model.CurrentPage);
        Assert.AreEqual(expectedModel.TotalPages, model.TotalPages);
        Assert.AreEqual(expectedModel.PageSize, model.PageSize);
        CollectionAssert.AreEquivalent(expectedModel.Countries, model.Countries);
    }

    [Test]
    public async Task Add_WithValidName_AddsCountryAndRedirectsToIndex()
    {
        // Arrange
        var countryName = "Test Country";

        mockCountryService.Setup(s => s.CountryWithNameExistsAsync(countryName,0)).ReturnsAsync(false);

        // Act
        var result = await controller.Add(countryName);

        // Assert
        mockCountryService.Verify(s => s.AddCountryAsync(countryName), Times.Once);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(controller.Index), redirectResult.ActionName);
    }

    [Test]
    public async Task Add_WithExistingName_ReturnsBadRequest()
    {
        // Arrange
        var countryName = "Test Country";

        mockCountryService.Setup(s => s.CountryWithNameExistsAsync(countryName,0)).ReturnsAsync(true);

        // Act
        var result = await controller.Add(countryName);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task Edit_WithValidId_ReturnsCorrectViewResult()
    {
        // Arrange
        var countryId = 1;
        var countryName = "Test Country";
        var country = new CountryViewModel { Id = countryId, Name = countryName };

        mockCountryService.Setup(s => s.GetByIdAsync(countryId)).ReturnsAsync(country);

        // Act
        var result = await controller.Edit(countryId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CountryViewModel>(viewResult.Model);
        var model = viewResult.Model as CountryViewModel;
        Assert.AreEqual(country, model);
    }

    [Test]
    public async Task Edit_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var countryId = 1;

        mockCountryService.Setup(s => s.GetByIdAsync(countryId)).ReturnsAsync((CountryViewModel)null);

        // Act
        var result = await controller.Edit(countryId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_WithValidModel_EditsCountryAndRedirectsToIndex()
    {
        // Arrange
        var countryId = 1;
        var countryName = "Test Country";
        var model = new CountryViewModel { Id = countryId, Name = countryName };

        mockCountryService.Setup(s => s.GetByIdAsync(countryId)).ReturnsAsync(model);
        mockCountryService.Setup(s => s.CountryWithNameExistsAsync(countryName, countryId)).ReturnsAsync(false);

        // Act
        var result = await controller.Edit(model);

        // Assert
        mockCountryService.Verify(s => s.EditAsync(model), Times.Once);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(controller.Index), redirectResult.ActionName);
    }

    [Test]
    public async Task Edit_WithInvalidModel_ReturnsViewResultWithModel()
    {
        // Arrange
        var countryId = 1;
        var countryName = "Test Country";
        var model = new CountryViewModel { Id = countryId, Name = countryName };

        mockCountryService.Setup(s => s.GetByIdAsync(countryId)).ReturnsAsync(model);
        mockCountryService.Setup(s => s.CountryWithNameExistsAsync(countryName, countryId)).ReturnsAsync(true);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

}

