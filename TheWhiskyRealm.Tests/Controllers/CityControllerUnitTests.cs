using Microsoft.AspNetCore.Mvc;
using Moq;
using TheWhiskyRealm.Areas.Admin.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.City;
using TheWhiskyRealm.Core.Models.AdminArea.Country;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class CityControllerUnitTests
{
    private Mock<ICountryService> mockCountryService;
    private Mock<ICityService> mockCityService;
    private Mock<IVenueService> mockVenueService;
    private CityController controller;

    [SetUp]
    public void SetUp()
    {
        mockCountryService = new Mock<ICountryService>();
        mockCityService = new Mock<ICityService>();
        mockVenueService = new Mock<IVenueService>();
        controller = new CityController(mockCountryService.Object, mockCityService.Object, mockVenueService.Object);
    }

    [Test]
    public async Task Index_WithDefaultParameters_ReturnsCorrectViewResult()
    {
        // Arrange
        int currentPage = 1;
        int pageSize = 10;
        int totalCities = 100;
        var cities = new List<CityViewModel>(); 

        var expectedModel = new CityIndexViewModel
        {
            Cities = cities,
            CurrentPage = currentPage,
            TotalPages = (int)Math.Ceiling(totalCities / (double)pageSize)
        };

        mockCityService.Setup(s => s.GetTotalCitiesAsync()).ReturnsAsync(totalCities);
        mockCityService.Setup(s => s.GetAllCitiesAsync(currentPage, pageSize)).ReturnsAsync(cities);

        // Act
        var result = await controller.Index(currentPage, pageSize);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CityIndexViewModel>(viewResult.Model);
        var model = viewResult.Model as CityIndexViewModel;
        Assert.AreEqual(expectedModel.CurrentPage, model.CurrentPage);
        Assert.AreEqual(expectedModel.TotalPages, model.TotalPages);
        CollectionAssert.AreEquivalent(expectedModel.Cities, model.Cities);
    }

    [Test]
    public async Task Add_WithValidCountryId_ReturnsCorrectViewResult()
    {
        // Arrange
        int? countryId = 1;
        var countries = new List<CountryViewModel>(); 

        var expectedModel = new CityFormViewModel
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
        Assert.IsInstanceOf<CityFormViewModel>(viewResult.Model);
        var model = viewResult.Model as CityFormViewModel;
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
    public async Task Add_WithValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new CityFormViewModel
        {
            Name = "Test City",
            CountryId = 1,
            Zip = "1000"
        };
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        mockCountryService.Setup(s => s.CountryExistsAsync((int)model.CountryId)).ReturnsAsync(true);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);
        mockCityService.Setup(s => s.CityWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId,0)).ReturnsAsync(false);
        mockCityService.Setup(s => s.AddCityAsync(model.Name, (int)model.CountryId, model.Zip)).ReturnsAsync(1);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Info", redirectResult.ActionName);
        Assert.AreEqual("City", redirectResult.ControllerName);
        Assert.AreEqual(1, redirectResult.RouteValues["id"]);
    }

    [Test]
    public async Task Add_WithInvalidCountryId_ReturnsViewResult()
    {
        // Arrange
        var model = new CityFormViewModel
        {
            Name = "Test City",
            CountryId = 1,
            Zip = "1000"
        };
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        mockCountryService.Setup(s => s.CountryExistsAsync((int)model.CountryId)).ReturnsAsync(false);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CityFormViewModel>(viewResult.Model);
        var returnedModel = viewResult.Model as CityFormViewModel;
        CollectionAssert.AreEquivalent(countries, returnedModel.Countries);
    }

    [Test]
    public async Task Edit_WithValidId_ReturnsCorrectViewResult()
    {
        // Arrange
        int id = 1;
        var city = new CityFormViewModel
        {
            Id = id,
            Name = "Test City",
            CountryId = 1,
            Zip = "1000"
        };
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        mockCityService.Setup(s => s.GetCityByIdAsync(id)).ReturnsAsync(city);
        mockCountryService.Setup(s => s.CountryExistsAsync((int)city.CountryId)).ReturnsAsync(true);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CityFormViewModel>(viewResult.Model);
        var model = viewResult.Model as CityFormViewModel;
        Assert.AreEqual(city.Id, model.Id);
        Assert.AreEqual(city.Name, model.Name);
        Assert.AreEqual(city.CountryId, model.CountryId);
        Assert.AreEqual(city.Zip, model.Zip);
        CollectionAssert.AreEquivalent(countries, model.Countries);
    }

    [Test]
    public async Task Edit_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        int id = 1;

        mockCityService.Setup(s => s.GetCityByIdAsync(id)).ReturnsAsync((CityFormViewModel)null);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Edit_WithValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new CityFormViewModel
        {
            Id = 1,
            Name = "Test City",
            CountryId = 1,
            Zip = "1000"
        };
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        mockCityService.Setup(s => s.GetCityByIdAsync(model.Id)).ReturnsAsync(model);
        mockCountryService.Setup(s => s.CountryExistsAsync((int)model.CountryId)).ReturnsAsync(true);
        mockCityService.Setup(s => s.CityWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId, model.Id)).ReturnsAsync(false);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectResult = result as RedirectToActionResult;
        Assert.AreEqual("Info", redirectResult.ActionName);
        Assert.AreEqual("City", redirectResult.ControllerName);
        Assert.AreEqual(model.Id, redirectResult.RouteValues["id"]);
    }

    [Test]
    public async Task Edit_WithInvalidModel_ReturnsViewResult()
    {
        // Arrange
        var model = new CityFormViewModel
        {
            Id = 1,
            Name = "Test City",
            CountryId = 1,
            Zip = "1000"
        };
        var countries = new List<CountryViewModel>
    {
        new CountryViewModel { Id = 1, Name = "Test Country" }
    };

        mockCityService.Setup(s => s.GetCityByIdAsync(model.Id)).ReturnsAsync(model);
        mockCountryService.Setup(s => s.CountryExistsAsync((int)model.CountryId)).ReturnsAsync(true);
        mockCityService.Setup(s => s.CityWithThisNameAndCountryExistsAsync(model.Name, (int)model.CountryId, model.Id)).ReturnsAsync(true);
        mockCountryService.Setup(s => s.GetAllCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CityFormViewModel>(viewResult.Model);
        var returnedModel = viewResult.Model as CityFormViewModel;
        Assert.AreEqual(model.Id, returnedModel.Id);
        Assert.AreEqual(model.Name, returnedModel.Name);
        Assert.AreEqual(model.CountryId, returnedModel.CountryId);
        Assert.AreEqual(model.Zip, returnedModel.Zip);
        CollectionAssert.AreEquivalent(countries, returnedModel.Countries);
    }
    [Test]
    public async Task Info_WithValidId_ReturnsCorrectViewResult()
    {
        // Arrange
        int id = 1;
        var city = new CityFormViewModel
        {
            Id = id,
            Name = "Test City",
            CountryId = 1,
            Zip = "1000"
        };
        var venues = new List<VenueViewModel>(); // Add some test venues here

        var expectedModel = new CityInfoViewModel
        {
            Id = id,
            Name = city.Name,
            Zip = city.Zip,
            Venues = venues
        };

        mockCityService.Setup(s => s.GetCityByIdAsync(id)).ReturnsAsync(city);
        mockVenueService.Setup(s => s.GetVenuesByCityAsync(id)).ReturnsAsync(venues);

        // Act
        var result = await controller.Info(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<CityInfoViewModel>(viewResult.Model);
        var model = viewResult.Model as CityInfoViewModel;
        Assert.AreEqual(expectedModel.Id, model.Id);
        Assert.AreEqual(expectedModel.Name, model.Name);
        Assert.AreEqual(expectedModel.Zip, model.Zip);
        CollectionAssert.AreEquivalent(expectedModel.Venues, model.Venues);
    }

    [Test]
    public async Task Info_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        int id = 1;

        mockCityService.Setup(s => s.GetCityByIdAsync(id)).ReturnsAsync((CityFormViewModel)null);

        // Act
        var result = await controller.Info(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

}
