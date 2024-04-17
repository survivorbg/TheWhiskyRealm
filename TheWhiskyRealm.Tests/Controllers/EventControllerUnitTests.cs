using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TheWhiskyRealm.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Venue;
using TheWhiskyRealm.Core.Models.Event;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class EventControllerUnitTests
{
    private Mock<IEventService> mockEventService;
    private Mock<IVenueService> mockVenueService;
    private EventController controller;

    [SetUp]
    public void SetUp()
    {
        mockEventService = new Mock<IEventService>();
        mockVenueService = new Mock<IVenueService>();
        controller = new EventController(mockEventService.Object, mockVenueService.Object);
    }
    [Test]
    public async Task Index_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var allEventsViewModel = new List<AllEventViewModel>
    {
        new AllEventViewModel
        {
            Id = 1,
            Title = "Test Event",
            StartDate = DateTime.Now.AddDays(1).ToString("hh:mm dddd, dd MMMM yyyy"),
            Price = 100,
            VenueName = "Test Venue",
            AvailableSpots = 50
        }
    };

        mockEventService.Setup(s => s.GetAllEventsAsync()).ReturnsAsync(allEventsViewModel);

        // Act
        var result = await controller.Index();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<AllEventViewModel>>(viewResult.Model);
        var model = viewResult.Model as List<AllEventViewModel>;
        Assert.AreEqual(allEventsViewModel, model);
    }
    [Test]
    public async Task PastEvents_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var allPastEventsViewModel = new List<AllEventViewModel>
    {
        new AllEventViewModel
        {
            Id = 1,
            Title = "Past Test Event",
            StartDate = DateTime.Now.AddDays(-1).ToString("hh:mm dddd, dd MMMM yyyy"),
            Price = 100,
            VenueName = "Past Test Venue",
            AvailableSpots = 50
        }
    };

        mockEventService.Setup(s => s.GetAllPastEventsAsync()).ReturnsAsync(allPastEventsViewModel);

        // Act
        var result = await controller.PastEvents();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<AllEventViewModel>>(viewResult.Model);
        var model = viewResult.Model as List<AllEventViewModel>;
        Assert.AreEqual(allPastEventsViewModel, model);
    }
    [Test]
    public async Task Edit_ReturnsCorrectViewResultWithModel_WhenModelIsNotNull()
    {
        // Arrange
        var id = 1;
        var eventEditViewModel = new EventEditViewModel
        {
            Id = id,
            Title = "Test Event",
            Description = "Test Description",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            Price = 100,
            VenueId = 1,
            Venues = new List<VenueViewModel>
        {
            new VenueViewModel
            {
                VenueId = 1,
                VenueName = "Test Venue",
                Capacity = 100
            }
        }
        };

        mockEventService.Setup(s => s.GetEventForEditAsync(id)).ReturnsAsync(eventEditViewModel);
        mockEventService.Setup(s => s.GetOrganiserIdAsync(id)).ReturnsAsync("TestUserId");
        mockEventService.Setup(s => s.HasAlreadyStartedAsync(id)).ReturnsAsync(false);
        mockVenueService.Setup(s => s.VenueExistAsync(eventEditViewModel.VenueId)).ReturnsAsync(true);
        mockEventService.Setup(s => s.GetJoinedUsersCountAsync(id)).ReturnsAsync(50);
        mockVenueService.Setup(s => s.GetVenuesWithMoreCapacityAsync(50)).ReturnsAsync(eventEditViewModel.Venues);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        new Claim(ClaimTypes.Role, "Administrator")
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
        Assert.IsInstanceOf<EventEditViewModel>(viewResult.Model);
        var model = viewResult.Model as EventEditViewModel;
        Assert.AreEqual(eventEditViewModel, model);
    }
    [Test]
    public async Task Edit_ReturnsNotFound_WhenModelIsNull()
    {
        // Arrange
        var id = 1;

        mockEventService.Setup(s => s.GetEventForEditAsync(id)).ReturnsAsync((EventEditViewModel)null);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Edit_ReturnsNotFound_WhenEventIsNull()
    {
        // Arrange
        var id = 1;
        var eventEditViewModel = new EventEditViewModel
        {
            Id = id,
            // other properties...
        };

        mockEventService.Setup(s => s.GetEventAsync(id)).ReturnsAsync((EventDetailsViewModel)null);

        // Act
        var result = await controller.Edit(eventEditViewModel);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelIsValid()
    {
        // Arrange
        var id = 1;
        var eventEditViewModel = new EventEditViewModel
        {
            Id = id,
            Title = "Test Event",
            Description = "Test Description",
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(3),
            Price = 100,
            VenueId = 1,
            Venues = new List<VenueViewModel>
        {
            new VenueViewModel
            {
                VenueId = 1,
                VenueName = "Test Venue",
                Capacity = 100
            }
        }
        };

        var eventDetailsViewModel = new EventDetailsViewModel
        {
            Id = id,
            Title = "Test Event",
            Description = "Test Description",
            OrganiserName = "Test Organiser",
            StartDate = DateTime.Now.AddDays(2).ToString("hh:mm dddd, dd MMMM yyyy"),
            EndDate = DateTime.Now.AddDays(3).ToString("hh:mm dddd, dd MMMM yyyy"),
            Price = 100,
            VenueName = "Test Venue",
            AvailableSpots = 50,
            JoinedUsers = new List<string> { "User1", "User2" }
        };

        mockEventService.Setup(s => s.GetEventAsync(id)).ReturnsAsync(eventDetailsViewModel);
        mockEventService.Setup(s => s.HasAlreadyStartedAsync(id)).ReturnsAsync(false);
        mockEventService.Setup(s => s.GetOrganiserIdAsync(id)).ReturnsAsync("TestUserId");
        mockVenueService.Setup(s => s.VenueExistAsync(eventEditViewModel.VenueId)).ReturnsAsync(true);
        mockEventService.Setup(s => s.GetJoinedUsersCountAsync(id)).ReturnsAsync(50);
        mockVenueService.Setup(s => s.GetVenuesWithMoreCapacityAsync(50)).ReturnsAsync(eventEditViewModel.Venues);
        mockVenueService.Setup(s => s.GetVenueCapacityAsync(eventEditViewModel.VenueId)).ReturnsAsync(100);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        new Claim(ClaimTypes.Role, "Administrator")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Edit(eventEditViewModel);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
    }
    [Test]
    public async Task Add_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var eventAddViewModel = new EventAddViewModel
        {
            Venues = new List<VenueViewModel>
        {
            new VenueViewModel
            {
                VenueId = 1,
                VenueName = "Test Venue",
                Capacity = 100
            }
        }
        };

        mockVenueService.Setup(s => s.GetVenuesAsync()).ReturnsAsync(eventAddViewModel.Venues);

        // Act
        var result = await controller.Add();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<EventAddViewModel>(viewResult.Model);
        var model = viewResult.Model as EventAddViewModel;
        Assert.AreEqual(eventAddViewModel.Venues, model.Venues);
    }
    [Test]
    public async Task Add_ReturnsViewResultWithModel_WhenModelStateIsInvalid()
    {
        // Arrange
        var eventAddViewModel = new EventAddViewModel
        {
            Title = "Test Event",
            Description = "Test Description",
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            Price = 100,
            VenueId = 1,
            Venues = new List<VenueViewModel>
        {
            new VenueViewModel
            {
                VenueId = 1,
                VenueName = "Test Venue",
                Capacity = 100
            }
        }
        };

        mockVenueService.Setup(s => s.GetVenuesAsync()).ReturnsAsync(eventAddViewModel.Venues);
        mockVenueService.Setup(s => s.VenueExistAsync(eventAddViewModel.VenueId)).ReturnsAsync(false);

        // Act
        var result = await controller.Add(eventAddViewModel);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<EventAddViewModel>(viewResult.Model);
        var model = viewResult.Model as EventAddViewModel;
        Assert.AreEqual(eventAddViewModel, model);
    }

    [Test]
    public async Task Add_ReturnsRedirectToActionResult_WhenModelIsValid()
    {
        // Arrange
        var eventAddViewModel = new EventAddViewModel
        {
            Title = "Test Event",
            Description = "Test Description",
            StartDate = DateTime.Now.AddDays(2),
            EndDate = DateTime.Now.AddDays(3),
            Price = 100,
            VenueId = 1,
            Venues = new List<VenueViewModel>
        {
            new VenueViewModel
            {
                VenueId = 1,
                VenueName = "Test Venue",
                Capacity = 100
            }
        }
        };

        mockVenueService.Setup(s => s.VenueExistAsync(eventAddViewModel.VenueId)).ReturnsAsync(true);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        new Claim(ClaimTypes.Role, "Administrator")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Add(eventAddViewModel);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
    }
    [Test]
    public async Task Join_ReturnsBadRequest_WhenUserIsAlreadyJoined()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        mockEventService.Setup(s => s.EventExistAsync(id)).ReturnsAsync(true);
        mockEventService.Setup(s => s.IsUserAlreadyJoinedAsync(id, userId)).ReturnsAsync(true);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "UserRole")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Join(id);

        // Assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }

    [Test]
    public async Task Join_ReturnsRedirectToActionResult_WhenUserCanJoinEvent()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        mockEventService.Setup(s => s.EventExistAsync(id)).ReturnsAsync(true);
        mockEventService.Setup(s => s.IsUserAlreadyJoinedAsync(id, userId)).ReturnsAsync(false);
        mockEventService.Setup(s => s.GetOrganiserIdAsync(id)).ReturnsAsync("AnotherUserId");
        mockEventService.Setup(s => s.HasAvaialbleSpotsAsync(id)).ReturnsAsync(true);
        mockEventService.Setup(s => s.HasAlreadyStartedAsync(id)).ReturnsAsync(false);

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
        var result = await controller.Join(id);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
    }
    [Test]
    public async Task Leave_ReturnsNotFound_WhenEventDoesNotExist()
    {
        // Arrange
        var id = 1;

        mockEventService.Setup(s => s.EventExistAsync(id)).ReturnsAsync(false);

        // Act
        var result = await controller.Leave(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Leave_ReturnsRedirectToActionResult_WhenUserCanLeaveEvent()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        mockEventService.Setup(s => s.EventExistAsync(id)).ReturnsAsync(true);
        mockEventService.Setup(s => s.IsUserAlreadyJoinedAsync(id, userId)).ReturnsAsync(true);
        mockEventService.Setup(s => s.HasAlreadyStartedAsync(id)).ReturnsAsync(false);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "UserRole")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Leave(id);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
    }
    [Test]
    public async Task MyEvents_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var userId = "TestUserId";
        var eventDetailsViewModel = new List<EventDetailsViewModel>
    {
        new EventDetailsViewModel
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            OrganiserName = "Test Organiser",
            StartDate = DateTime.Now.AddDays(1).ToString("hh:mm dddd, dd MMMM yyyy"),
            EndDate = DateTime.Now.AddDays(2).ToString("hh:mm dddd, dd MMMM yyyy"),
            Price = 100,
            VenueName = "Test Venue",
            AvailableSpots = 50,
            JoinedUsers = new List<string> { "User1", "User2" }
        }
    };

        mockEventService.Setup(s => s.GetUserEventsAsync(userId)).ReturnsAsync(eventDetailsViewModel);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "UserRole")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.MyEvents();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<EventDetailsViewModel>>(viewResult.Model);
        var model = viewResult.Model as List<EventDetailsViewModel>;
        Assert.AreEqual(eventDetailsViewModel, model);
    }
    [Test]
    public async Task OrganisedEvents_ReturnsViewResultWithEmptyModel_WhenNoEventsAreOrganisedByUser()
    {
        // Arrange
        var userId = "TestUserId";

        mockEventService.Setup(s => s.GetEventsOrganisedByUserAsync(userId)).ReturnsAsync(new List<EventDetailsViewModel>());

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
        var result = await controller.OrganisedEvents();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<EventDetailsViewModel>>(viewResult.Model);
        var model = viewResult.Model as List<EventDetailsViewModel>;
        Assert.IsEmpty(model);
    }
    [Test]
    public async Task OrganisedEvents_ReturnsViewResultWithModel_WhenEventsAreOrganisedByUser()
    {
        // Arrange
        var userId = "TestUserId";
        var eventDetailsViewModel = new List<EventDetailsViewModel>
    {
        new EventDetailsViewModel
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            OrganiserName = "Test Organiser",
            StartDate = DateTime.Now.AddDays(1).ToString("hh:mm dddd, dd MMMM yyyy"),
            EndDate = DateTime.Now.AddDays(2).ToString("hh:mm dddd, dd MMMM yyyy"),
            Price = 100,
            VenueName = "Test Venue",
            AvailableSpots = 50,
            JoinedUsers = new List<string> { "User1", "User2" }
        }
    };

        mockEventService.Setup(s => s.GetEventsOrganisedByUserAsync(userId)).ReturnsAsync(eventDetailsViewModel);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "UserRole")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.OrganisedEvents();

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<List<EventDetailsViewModel>>(viewResult.Model);
        var model = viewResult.Model as List<EventDetailsViewModel>;
        Assert.AreEqual(eventDetailsViewModel, model);
    }
    [Test]
    public async Task Delete_ReturnsNotFound_WhenEventDoesNotExist()
    {
        // Arrange
        var id = 1;

        mockEventService.Setup(s => s.GetEventAsync(id)).ReturnsAsync((EventDetailsViewModel)null);

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Delete_ReturnsRedirectToActionResult_WhenUserCanDeleteEvent()
    {
        // Arrange
        var id = 1;
        var userId = "TestUserId";

        var eventDetailsViewModel = new EventDetailsViewModel
        {
            Id = id,
            Title = "Test Event",
            Description = "Test Description",
            OrganiserName = "Test Organiser",
            StartDate = DateTime.Now.AddDays(1).ToString("hh:mm dddd, dd MMMM yyyy"),
            EndDate = DateTime.Now.AddDays(2).ToString("hh:mm dddd, dd MMMM yyyy"),
            Price = 100,
            VenueName = "Test Venue",
            AvailableSpots = 50,
            JoinedUsers = new List<string> { "User1", "User2" }
        };

        mockEventService.Setup(s => s.GetEventAsync(id)).ReturnsAsync(eventDetailsViewModel);
        mockEventService.Setup(s => s.HasAlreadyStartedAsync(id)).ReturnsAsync(false);
        mockEventService.Setup(s => s.GetOrganiserIdAsync(id)).ReturnsAsync(userId);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "Administrator")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
    }

}
