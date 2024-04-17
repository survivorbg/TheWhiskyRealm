using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using TheWhiskyRealm.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.Award;
using TheWhiskyRealm.Infrastructure.Data.Enums;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class AwardControllerUnitTests
{
    private Mock<IAwardService> mockAwardService;
    private Mock<IWhiskyService> mockWhiskyService;
    private AwardController controller;

    [SetUp]
    public void SetUp()
    {
        mockAwardService = new Mock<IAwardService>();
        mockWhiskyService = new Mock<IWhiskyService>();
        controller = new AwardController(mockAwardService.Object, mockWhiskyService.Object);
    }

    [Test]
    public async Task Edit_ReturnsCorrectViewResultWithModel_WhenModelIsNotNull()
    {
        // Arrange
        var id = 1;
        var awardViewModel = new AwardViewModel
        {
            Id = id,
            Title = "Test Award",
            AwardsCeremony = "Test Ceremony",
            MedalType = MedalType.Gold.GetDisplayName(),
            Year = 2023,
            WhiskyId = 1
        };

        mockAwardService.Setup(s => s.GetAwardByIdAsync(id)).ReturnsAsync(awardViewModel);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(awardViewModel.WhiskyId)).ReturnsAsync("TestUserId");

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
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
        Assert.IsInstanceOf<AwardViewModel>(viewResult.Model);
        var model = viewResult.Model as AwardViewModel;
        Assert.AreEqual(awardViewModel, model);
    }


    [Test]
    public async Task Edit_ReturnsNotFoundResult_WhenModelIsNull()
    {
        // Arrange
        var id = 1;
        mockAwardService.Setup(s => s.GetAwardByIdAsync(id)).ReturnsAsync((AwardViewModel)null);

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var id = 1;
        var awardViewModel = new AwardViewModel
        {
            Id = id,
            Title = "Test Award",
            AwardsCeremony = "Test Ceremony",
            MedalType = MedalType.Gold.GetDisplayName(),
            Year = 2023,
            WhiskyId = 1
        };

        mockAwardService.Setup(s => s.GetAwardByIdAsync(id)).ReturnsAsync(awardViewModel);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(awardViewModel.WhiskyId)).ReturnsAsync("DifferentUserId");

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "TestUserId"),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // Act
        var result = await controller.Edit(id);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }

    [Test]
    public async Task Edit_ReturnsUnauthorized_WhenUserIsWhiskyExpertButDidNotPublishWhisky()
    {
        // Arrange
        var userId = "test-user-id";
        var publisherId = "another-user-id";
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardViewModel() { WhiskyId = whiskyId };

        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(publisherId);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }

    [Test]
    public async Task EditPost_ReturnsUnauthorizedResult_WhenUserIsNotAuthorized()
    {
        // Arrange
        var awardViewModel = new AwardViewModel
        {
            Id = 1,
            Title = "Test Award",
            AwardsCeremony = "Test Ceremony",
            MedalType = MedalType.Gold.GetDisplayName(),
            Year = 2023,
            WhiskyId = 1
        };

        // Mock User
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, "DifferentUserId"),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(awardViewModel.WhiskyId)).ReturnsAsync("TestUserId");

        // Act
        var result = await controller.Edit(awardViewModel);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }
    [Test]
    public async Task Edit_ReturnsNotFound_WhenAwardDoesNotExist()
    {
        // Arrange
        var userId = "test-user-id";
        var publisherId = userId;
        var whiskyId = 1;
        var awardId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardViewModel() { Id = awardId, WhiskyId = whiskyId };

        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(publisherId);
        mockAwardService.Setup(s => s.AwardExistAsync(awardId)).ReturnsAsync(false);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Edit_ReturnsRedirectToActionResult_WhenModelIsValid()
    {
        // Arrange
        var userId = "test-user-id";
        var publisherId = userId;
        var whiskyId = 1;
        var awardId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardViewModel()
        {
            Id = awardId,
            WhiskyId = whiskyId,
            Title = "Test Title",
            AwardsCeremony = "Test AwardsCeremony",
            MedalType = "Gold",
            Year = 2024,
            MedalTypeOptions = new List<string> { "Gold", "Silver", "Bronze" }
        };

        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(publisherId);
        mockAwardService.Setup(s => s.AwardExistAsync(awardId)).ReturnsAsync(true);

        // Act
        var result = await controller.Edit(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual("Whisky", redirectToActionResult.ControllerName);
        Assert.AreEqual(whiskyId, redirectToActionResult.RouteValues["id"]);
    }
    [Test]
    public async Task Delete_ReturnsNotFound_WhenAwardDoesNotExist()
    {
        // Arrange
        var userId = "test-user-id";
        var awardId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockAwardService.Setup(s => s.GetAwardByIdAsync(awardId)).ReturnsAsync((AwardViewModel)null);


        // Act
        var result = await controller.Delete(awardId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Delete_ReturnsViewResult_WhenModelIsValid()
    {
        // Arrange
        var userId = "test-user-id";
        var awardId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var award = new AwardViewModel() { Id = awardId, WhiskyId = 1 };
        mockAwardService.Setup(s => s.GetAwardByIdAsync(awardId)).ReturnsAsync(award);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(award.WhiskyId)).ReturnsAsync(userId);

        // Act
        var result = await controller.Delete(awardId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }
    [Test]
    public async Task DeletePost_ReturnsNotFound_WhenAwardDoesNotExist()
    {
        // Arrange
        var userId = "test-user-id";
        var awardId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardViewModel() { Id = awardId };
        mockAwardService.Setup(s => s.GetAwardByIdAsync(awardId)).ReturnsAsync((AwardViewModel)null);

        // Act
        var result = await controller.Delete(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Delete_ReturnsUnauthorized_WhenUserIsWhiskyExpertButDidNotPublishWhisky()
    {
        // Arrange
        var userId = "test-user-id";
        var publisherId = "another-user-id";
        var awardId = 1;
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardViewModel() { Id = awardId, WhiskyId = whiskyId };
        mockAwardService.Setup(s => s.GetAwardByIdAsync(awardId)).ReturnsAsync(model);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(publisherId);

        // Act
        var result = await controller.Delete(model);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }
    [Test]
    public async Task Delete_ReturnsRedirectToActionResult_WhenModelIsValid()
    {
        // Arrange
        var userId = "test-user-id";
        var awardId = 1;
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardViewModel() { Id = awardId, WhiskyId = whiskyId };
        mockAwardService.Setup(s => s.GetAwardByIdAsync(awardId)).ReturnsAsync(model);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(userId);

        // Act
        var result = await controller.Delete(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual("Whisky", redirectToActionResult.ControllerName);
        Assert.AreEqual(whiskyId, redirectToActionResult.RouteValues["id"]);
    }
    [Test]
    public async Task Add_ReturnsNotFound_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var userId = "test-user-id";
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(false);

        // Act
        var result = await controller.Add(whiskyId);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task Add_ReturnsUnauthorized_WhenUserIsWhiskyExpertButDidNotPublishWhisky()
    {
        // Arrange
        var userId = "test-user-id";
        var publisherId = "another-user-id";
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(publisherId);

        // Act
        var result = await controller.Add(whiskyId);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }
    [Test]
    public async Task Add_ReturnsViewResult_WhenModelIsValid()
    {
        // Arrange
        var userId = "test-user-id";
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(userId);

        // Act
        var result = await controller.Add(whiskyId);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
    }
    [Test]
    public async Task AddPost_ReturnsNotFound_WhenWhiskyDoesNotExist()
    {
        // Arrange
        var userId = "test-user-id";
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardAddModel() { WhiskyId = whiskyId };
        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(false);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }
    [Test]
    public async Task AddPost_ReturnsUnauthorized_WhenUserIsWhiskyExpertButDidNotPublishWhisky()
    {
        // Arrange
        var userId = "test-user-id";
        var publisherId = "another-user-id";
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardAddModel() { WhiskyId = whiskyId };
        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(publisherId);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<UnauthorizedResult>(result);
    }
    [Test]
    public async Task Add_ReturnsRedirectToActionResult_WhenModelIsValid()
    {
        // Arrange
        var userId = "test-user-id";
        var whiskyId = 1;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Role, "WhiskyExpert")
        }));

        controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        var model = new AwardAddModel()
        {
            Title = "Test Title",
            AwardsCeremony = "Test AwardsCeremony",
            MedalType = "Gold",
            Year = 2024,
            WhiskyId = whiskyId,
            MedalTypeOptions = new List<string> { "Gold", "Silver", "Bronze" }
        };

        mockWhiskyService.Setup(s => s.WhiskyExistAsync(whiskyId)).ReturnsAsync(true);
        mockWhiskyService.Setup(s => s.GetWhiskyPublisherAsync(whiskyId)).ReturnsAsync(userId);

        // Act
        var result = await controller.Add(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual("Details", redirectToActionResult.ActionName);
        Assert.AreEqual("Whisky", redirectToActionResult.ControllerName);
        Assert.AreEqual(whiskyId, redirectToActionResult.RouteValues["id"]);
    }

}
