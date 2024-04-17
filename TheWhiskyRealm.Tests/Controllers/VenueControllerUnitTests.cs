using Microsoft.AspNetCore.Mvc;
using Moq;
using TheWhiskyRealm.Areas.Admin.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.City;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class VenueControllerUnitTests
{
    private Mock<IVenueService> mockVenueService;
    private Mock<ICityService> mockCityService;
    private Mock<IEventService> mockEventService;
    private VenueController controller;

    [SetUp]
    public void SetUp()
    {
        mockVenueService = new Mock<IVenueService>();
        mockCityService = new Mock<ICityService>();
        mockEventService = new Mock<IEventService>();
        controller = new VenueController(mockVenueService.Object, mockCityService.Object, mockEventService.Object);
    }

    [Test]
    public async Task Index_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var totalVenues = 20;
        var venues = new List<VenueViewModel>
    {
        new VenueViewModel { VenueId = 1, VenueName = "Test Venue 1", Capacity = 100 },
        new VenueViewModel { VenueId = 2, VenueName = "Test Venue 2", Capacity = 200 }
    };
        var expectedModel = new VenueIndexViewModel { Venues = venues, CurrentPage = 1, TotalPages = 2, PageSize = 10 };

        mockVenueService.Setup(s => s.GetTotalVenuesAsync()).ReturnsAsync(totalVenues);
        mockVenueService.Setup(s => s.GetVenuesAsync(expectedModel.CurrentPage, expectedModel.PageSize)).ReturnsAsync(venues);

        // Act
        var result = await controller.Index(expectedModel.CurrentPage, expectedModel.PageSize);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<VenueIndexViewModel>(viewResult.Model);
        var model = viewResult.Model as VenueIndexViewModel;
        Assert.AreEqual(expectedModel.CurrentPage, model.CurrentPage);
        Assert.AreEqual(expectedModel.TotalPages, model.TotalPages);
        Assert.AreEqual(expectedModel.PageSize, model.PageSize);
        CollectionAssert.AreEquivalent(expectedModel.Venues, model.Venues);
    }
    [Test]
    public async Task Add_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var cities = new List<CityViewModel>
    {
        new CityViewModel { Id = 1, Name = "Test City 1", Zip = "1000", Country = "Test Country 1" },
        new CityViewModel { Id = 2, Name = "Test City 2", Zip = "2000", Country = "Test Country 2" }
    };

        mockCityService.Setup(s => s.GetAllCitiesAsync()).ReturnsAsync(cities);

        // Act
        var result = await controller.Add();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<VenueFormViewModel>(viewResult.Model);
        var model = viewResult.Model as VenueFormViewModel;
        CollectionAssert.AreEquivalent(cities, model.Cities);
    }
    [Test]
    public async Task Add_WhenModelIsNull_ReturnsBadRequestResult()
    {
        // Act
        var result = await controller.Add(null);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task Add_WhenVenueExists_ReturnsViewResultWithModel()
    {
        // Arrange
        var model = new VenueFormViewModel { Id = 1, Name = "Test Venue", Capacity = 100, CityId = 1 };
        var cities = new List<CityViewModel>
    {
        new CityViewModel { Id = 1, Name = "Test City", Zip = "1000", Country = "Test Country" }
    };

        mockVenueService.Setup(x => x.VenueExistByNameAsync(model.Name, model.CityId, 0)).ReturnsAsync(true);
        mockCityService.Setup(x => x.GetAllCitiesAsync()).ReturnsAsync(cities);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
        Assert.IsTrue(controller.ModelState.ErrorCount > 0);
    }

    [Test]
    public async Task Add_WhenModelStateIsInvalid_ReturnsViewResultWithModel()
    {
        // Arrange
        var model = new VenueFormViewModel { Id = 1, Name = "Test Venue", Capacity = 100, CityId = 1 };
        var cities = new List<CityViewModel>
    {
        new CityViewModel { Id = 1, Name = "Test City", Zip = "1000", Country = "Test Country" }
    };

        controller.ModelState.AddModelError("Error", "Some error");

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

    [Test]
    public async Task Add_WhenModelIsValid_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new VenueFormViewModel { Id = 1, Name = "Test Venue", Capacity = 100, CityId = 1 };

        mockVenueService.Setup(x => x.VenueExistByNameAsync(model.Name, model.CityId,0)).ReturnsAsync(false);
        mockVenueService.Setup(x => x.AddVenueAsync(model)).ReturnsAsync(model.Id);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(VenueController.Info), actionResult.ActionName);
        Assert.AreEqual("Venue", actionResult.ControllerName);
        Assert.AreEqual(model.Id, actionResult.RouteValues["id"]);
    }
    [Test]
    public async Task Info_WhenIdIsInvalid_ReturnsNotFoundResult()
    {
        // Arrange
        var id = 1;

        mockVenueService.Setup(x => x.GetVenueByIdAsync(id)).ReturnsAsync((VenueFormViewModel)null);

        // Act
        var result = await controller.Info(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Info_WhenVenueExists_ReturnsViewResultWithModel()
    {
        // Arrange
        var id = 1;
        var venue = new VenueFormViewModel { Id = id, Name = "Test Venue", Capacity = 100, CityId = 1 };
        var pendingEvents = new List<EventViewModel>
    {
        new EventViewModel { Id = 1, Title = "Test Event 1", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1), Price = 10, AvailableSpots = 50, JoinedUsers = 10, OrganiserId = "1" },
        new EventViewModel { Id = 2, Title = "Test Event 2", StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(1), Price = 20, AvailableSpots = 100, JoinedUsers = 20, OrganiserId = "2" }
    };
        var pastEvents = new List<EventViewModel>
    {
        new EventViewModel { Id = 3, Title = "Test Event 3", StartDate = DateTime.Today.AddDays(-2), EndDate = DateTime.Today.AddDays(-1), Price = 15, AvailableSpots = 75, JoinedUsers = 15, OrganiserId = "3" }
    };

        mockVenueService.Setup(x => x.GetVenueByIdAsync(id)).ReturnsAsync(venue);
        mockEventService.Setup(x => x.GetAllEventsInVenueAsync(id)).ReturnsAsync(pendingEvents);
        mockEventService.Setup(x => x.GetAllPastEventsInVenueAsync(id)).ReturnsAsync(pastEvents);

        // Act
        var result = await controller.Info(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<VenueInfoViewModel>(viewResult.Model);
        var model = viewResult.Model as VenueInfoViewModel;
        Assert.AreEqual(venue.Name, model.VenueName);
        CollectionAssert.AreEquivalent(pendingEvents, model.PendingEvents);
        CollectionAssert.AreEquivalent(pastEvents, model.PastEvents);
    }
    [Test]
    public async Task Edit_WhenIdIsInvalid_ReturnsNotFoundResult()
    {
        // Arrange
        var id = 1;

        mockVenueService.Setup(x => x.GetVenueByIdAsync(id)).ReturnsAsync((VenueFormViewModel)null);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_WhenVenueExists_ReturnsViewResultWithModel()
    {
        // Arrange
        var id = 1;
        var venue = new VenueFormViewModel { Id = id, Name = "Test Venue", Capacity = 100, CityId = 1 };
        var cities = new List<CityViewModel>
    {
        new CityViewModel { Id = 1, Name = "Test City", Zip = "1000", Country = "Test Country" },
        new CityViewModel { Id = 2, Name = "Test City 2", Zip = "2000", Country = "Test Country 2" }
    };

        mockVenueService.Setup(x => x.GetVenueByIdAsync(id)).ReturnsAsync(venue);
        mockCityService.Setup(x => x.GetAllCitiesAsync()).ReturnsAsync(cities);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<VenueFormViewModel>(viewResult.Model);
        var model = viewResult.Model as VenueFormViewModel;
        Assert.AreEqual(venue, model);
        CollectionAssert.AreEquivalent(cities, model.Cities);
    }
    [Test]
    public async Task Edit_WhenModelIsNull_ReturnsBadRequestResult()
    {
        // Act
        var result = await controller.Edit(null);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task Edit_WhenVenueDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var model = new VenueFormViewModel { Id = 1, Name = "Test Venue", Capacity = 100, CityId = 1 };

        mockVenueService.Setup(x => x.GetVenueByIdAsync(model.Id)).ReturnsAsync((VenueFormViewModel)null);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_WhenCityDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var model = new VenueFormViewModel { Id = 1, Name = "Test Venue", Capacity = 100, CityId = 1 };
        var venue = new VenueFormViewModel { Id = model.Id, Name = model.Name, Capacity = model.Capacity, CityId = model.CityId };

        mockVenueService.Setup(x => x.GetVenueByIdAsync(model.Id)).ReturnsAsync(venue);
        mockCityService.Setup(x => x.CityExistAsync(model.CityId)).ReturnsAsync(false);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_WhenVenueExistsAndModelStateIsInvalid_ReturnsViewResultWithModel()
    {
        // Arrange
        var model = new VenueFormViewModel { Id = 1, Name = "Test Venue", Capacity = 100, CityId = 1 };
        var venue = new VenueFormViewModel { Id = model.Id, Name = model.Name, Capacity = model.Capacity, CityId = model.CityId };
        var cities = new List<CityViewModel>
    {
        new CityViewModel { Id = 1, Name = "Test City", Zip = "1000", Country = "Test Country" }
    };

        mockVenueService.Setup(x => x.GetVenueByIdAsync(model.Id)).ReturnsAsync(venue);
        mockCityService.Setup(x => x.CityExistAsync(model.CityId)).ReturnsAsync(true);
        mockCityService.Setup(x => x.GetAllCitiesAsync()).ReturnsAsync(cities);

        controller.ModelState.AddModelError("Error", "Some error");

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

    [Test]
    public async Task Edit_WhenVenueExistsAndModelStateIsValid_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new VenueFormViewModel { Id = 1, Name = "Test Venue", Capacity = 100, CityId = 1 };
        var venue = new VenueFormViewModel { Id = model.Id, Name = model.Name, Capacity = model.Capacity, CityId = model.CityId };

        mockVenueService.Setup(x => x.GetVenueByIdAsync(model.Id)).ReturnsAsync(venue);
        mockCityService.Setup(x => x.CityExistAsync(model.CityId)).ReturnsAsync(true);
        mockVenueService.Setup(x => x.VenueExistByNameAsync(model.Name, model.CityId, model.Id)).ReturnsAsync(false);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(VenueController.Info), actionResult.ActionName);
        Assert.AreEqual("Venue", actionResult.ControllerName);
        Assert.AreEqual(model.Id, actionResult.RouteValues["id"]);
    }

}

