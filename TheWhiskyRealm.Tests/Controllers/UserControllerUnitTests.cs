using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TheWhiskyRealm.Areas.Admin.Controllers;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.User;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Controllers;

[TestFixture]
public class UserControllerUnitTests
{
    private Mock<IUserService> mockUserService;
    private Mock<RoleManager<IdentityRole>> mockRoleManager;
    private Mock<UserManager<ApplicationUser>> mockUserManager;
    private UserController controller;

    [SetUp]
public void SetUp()
{
    mockUserService = new Mock<IUserService>();

    var roles = new List<IdentityRole>
    {
        new IdentityRole { Name = "Admin" },
        new IdentityRole { Name = "WhiskyExpert" },
        new IdentityRole { Name = "User" }
    }.AsQueryable();

    var mockRoleStore = new Mock<IRoleStore<IdentityRole>>();
    mockRoleStore.As<IQueryableRoleStore<IdentityRole>>().Setup(x => x.Roles).Returns(roles);

    mockRoleManager = new Mock<RoleManager<IdentityRole>>(
        mockRoleStore.Object,
        new IRoleValidator<IdentityRole>[0],
        new Mock<ILookupNormalizer>().Object,
        new Mock<IdentityErrorDescriber>().Object,
        new Mock<ILogger<RoleManager<IdentityRole>>>().Object);

    mockUserManager = new Mock<UserManager<ApplicationUser>>(
        new Mock<IUserStore<ApplicationUser>>().Object,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null);

    controller = new UserController(mockUserService.Object, mockRoleManager.Object, mockUserManager.Object);
}



    [Test]
    public async Task Index_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var totalUsers = 20;
        var users = new List<UserViewModel>
    {
        new UserViewModel { Id = "1", Username = "Test User 1", Role = "Admin", DateOfBirth = DateTime.Today, IsLocked = false },
        new UserViewModel { Id = "2", Username = "Test User 2", Role = "User", DateOfBirth = DateTime.Today, IsLocked = true }
    };
        var expectedModel = new UserIndexViewModel { Users = users, CurrentPage = 1, TotalPages = 2, PageSize = 10 };

        mockUserService.Setup(s => s.GetTotalUsersAsync()).ReturnsAsync(totalUsers);
        mockUserService.Setup(s => s.GetAllUsersAsync(expectedModel.CurrentPage, expectedModel.PageSize)).ReturnsAsync(users);

        // Act
        var result = await controller.Index(expectedModel.CurrentPage, expectedModel.PageSize);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<UserIndexViewModel>(viewResult.Model);
        var model = viewResult.Model as UserIndexViewModel;
        Assert.AreEqual(expectedModel.CurrentPage, model.CurrentPage);
        Assert.AreEqual(expectedModel.TotalPages, model.TotalPages);
        Assert.AreEqual(expectedModel.PageSize, model.PageSize);
        CollectionAssert.AreEquivalent(expectedModel.Users, model.Users);
    }
    [Test]
    public async Task Create_WithValidModel_ReturnsRedirectToActionResult()
    {
        // Arrange
        var userFormModel = new UserFormModel
        {
            Email = "test@example.com",
            DateOfBirth = DateTime.Today.AddYears(-20),
            Password = "Test123!",
            Role = "User"
        };
        var user = new ApplicationUser
        {
            UserName = userFormModel.Email,
            Email = userFormModel.Email,
            DateOfBirth = userFormModel.DateOfBirth
        };
        var identityResult = IdentityResult.Success;

        mockUserManager.Setup(um => um.CreateAsync(It.Is<ApplicationUser>(u => u.Email == userFormModel.Email), userFormModel.Password)).ReturnsAsync(identityResult);
        mockUserManager.Setup(um => um.AddToRoleAsync(It.Is<ApplicationUser>(u => u.Email == userFormModel.Email), userFormModel.Role)).ReturnsAsync(identityResult);

        // Act
        var result = await controller.Create(userFormModel);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var redirectToActionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(UserController.Index), redirectToActionResult.ActionName);
    }
    [Test]
    public async Task Delete_WithValidId_ReturnsCorrectViewResultWithModel()
    {
        // Arrange
        var id = "1";
        var user = new ApplicationUser
        {
            Id = id,
            Email = "test@example.com",
            DateOfBirth = DateTime.Today.AddYears(-20)
        };
        var expectedModel = new UserFormModel
        {
            Id = id,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth
        };

        mockUserManager.Setup(um => um.FindByIdAsync(id)).ReturnsAsync(user);

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<UserFormModel>(viewResult.Model);
        var model = viewResult.Model as UserFormModel;
        Assert.AreEqual(expectedModel.Id, model.Id);
        Assert.AreEqual(expectedModel.Email, model.Email);
        Assert.AreEqual(expectedModel.DateOfBirth, model.DateOfBirth);
    }
    [Test]
    public async Task Delete_WithInvalidId_ReturnsNotFoundResult()
    {
        // Arrange
        var id = "1";

        mockUserManager.Setup(um => um.FindByIdAsync(id)).ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await controller.Delete(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Delete_WhenUserExists_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new UserFormModel { Id = "1", Email = "test@test.com", DateOfBirth = DateTime.Today, Password = "Test1234!", ConfirmPassword = "Test1234!", Role = "Admin" };
        var user = new ApplicationUser { Id = model.Id, Email = model.Email, UserName = model.Email, DateOfBirth = model.DateOfBirth };

        mockUserManager.Setup(x => x.FindByIdAsync(model.Id)).ReturnsAsync(user);
        mockUserManager.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await controller.Delete(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(UserController.Index), actionResult.ActionName);
    }

    [Test]
    public async Task Delete_WhenUserDoesNotExist_ReturnsViewResult()
    {
        // Arrange
        var model = new UserFormModel { Id = "1", Email = "test@test.com", DateOfBirth = DateTime.Today, Password = "Test1234!", ConfirmPassword = "Test1234!", Role = "Admin" };

        mockUserManager.Setup(x => x.FindByIdAsync(model.Id)).ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await controller.Delete(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsNull(viewResult.Model);
    }

    [Test]
    public async Task Delete_WhenUserExistsButDeletionFails_ReturnsViewResultWithModel()
    {
        // Arrange
        var model = new UserFormModel { Id = "1", Email = "test@test.com", DateOfBirth = DateTime.Today, Password = "Test1234!", ConfirmPassword = "Test1234!", Role = "Admin" };
        var user = new ApplicationUser { Id = model.Id, Email = model.Email, UserName = model.Email, DateOfBirth = model.DateOfBirth };
        var identityError = new IdentityError { Description = "Deletion failed." };

        mockUserManager.Setup(x => x.FindByIdAsync(model.Id)).ReturnsAsync(user);
        mockUserManager.Setup(x => x.DeleteAsync(user)).ReturnsAsync(IdentityResult.Failed(identityError));

        // Act
        var result = await controller.Delete(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(user, viewResult.Model);
        Assert.IsTrue(controller.ModelState.ErrorCount > 0);
    }
    [Test]
    public async Task EditRole_WhenIdIsNull_ReturnsNotFoundResult()
    {
        // Act
        var result = await controller.EditRole((string)null);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task EditRole_WhenUserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var id = "1";

        mockUserManager.Setup(x => x.FindByIdAsync(id)).ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await controller.EditRole(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task EditRole_WhenUserExists_ReturnsViewResultWithModel()
    {
        // Arrange
        var id = "1";
        var user = new ApplicationUser { Id = id, Email = "test@test.com", UserName = "test@test.com", DateOfBirth = DateTime.Today };
        var roles = new List<string> { "Admin", "User", "WhiskyExpert" };

        mockUserManager.Setup(x => x.FindByIdAsync(id)).ReturnsAsync(user);
        mockRoleManager.Setup(x => x.Roles).Returns(roles.Select(r => new IdentityRole { Name = r }).AsQueryable());

        // Act
        var result = await controller.EditRole(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.IsInstanceOf<UserEditViewModel>(viewResult.Model);
        var model = viewResult.Model as UserEditViewModel;
        Assert.AreEqual(user.Id, model.Id);
        Assert.AreEqual(user.Email, model.Email);
        Assert.AreEqual(user.DateOfBirth, model.DateOfBirth);
        CollectionAssert.AreEquivalent(roles, model.Roles);
    }

    [Test]
    public async Task EditRole_WhenModelStateIsInvalid_ReturnsViewResultWithModel()
    {
        // Arrange
        var model = new UserEditViewModel { Id = "1", Email = "test@test.com", DateOfBirth = DateTime.Today, Role = "Admin" };
        controller.ModelState.AddModelError("Error", "Some error");

        // Act
        var result = await controller.EditRole(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

    [Test]
    public async Task EditRole_WhenUserDoesNotExist_ReturnsViewResultWithModel()
    {
        // Arrange
        var model = new UserEditViewModel { Id = "1", Email = "test@test.com", DateOfBirth = DateTime.Today, Role = "Admin" };

        mockUserManager.Setup(x => x.FindByIdAsync(model.Id)).ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await controller.EditRole(model);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

    [Test]
    public async Task EditRole_WhenUserExistsAndRoleIsNotChanged_ReturnsRedirectToActionResult()
    {
        // Arrange
        var model = new UserEditViewModel { Id = "1", Email = "test@test.com", DateOfBirth = DateTime.Today, Role = "Admin" };
        var user = new ApplicationUser { Id = model.Id, Email = model.Email, UserName = model.Email, DateOfBirth = model.DateOfBirth };

        mockUserManager.Setup(x => x.FindByIdAsync(model.Id)).ReturnsAsync(user);
        mockUserManager.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new List<string> { model.Role });

        // Act
        var result = await controller.EditRole(model);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(UserController.Index), actionResult.ActionName);
    }
    [Test]
    public async Task Lock_WhenIdIsNull_ReturnsNotFoundResult()
    {
        // Act
        var result = await controller.Lock(null);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Lock_WhenUserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var id = "1";

        mockUserManager.Setup(x => x.FindByIdAsync(id)).ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await controller.Lock(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Lock_WhenUserExists_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "1";
        var user = new ApplicationUser { Id = id, Email = "test@test.com", UserName = "test@test.com", DateOfBirth = DateTime.Today };

        mockUserManager.Setup(x => x.FindByIdAsync(id)).ReturnsAsync(user);
        mockUserManager.Setup(x => x.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await controller.Lock(id);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(UserController.Index), actionResult.ActionName);
    }
    [Test]
    public async Task Unlock_WhenIdIsNull_ReturnsNotFoundResult()
    {
        // Act
        var result = await controller.Unlock(null);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Unlock_WhenUserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var id = "1";

        mockUserManager.Setup(x => x.FindByIdAsync(id)).ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await controller.Unlock(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Unlock_WhenUserExists_ReturnsRedirectToActionResult()
    {
        // Arrange
        var id = "1";
        var user = new ApplicationUser { Id = id, Email = "test@test.com", UserName = "test@test.com", DateOfBirth = DateTime.Today };

        mockUserManager.Setup(x => x.FindByIdAsync(id)).ReturnsAsync(user);
        mockUserManager.Setup(x => x.SetLockoutEndDateAsync(user, null)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await controller.Unlock(id);

        // Assert
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        var actionResult = result as RedirectToActionResult;
        Assert.AreEqual(nameof(UserController.Index), actionResult.ActionName);
    }
    [Test]
    public async Task Info_WhenIdIsNull_ReturnsNotFoundResult()
    {
        // Act
        var result = await controller.Info(null);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Info_WhenUserDoesNotExist_ReturnsNotFoundResult()
    {
        // Arrange
        var id = "1";

        mockUserService.Setup(x => x.GetUserInfoAsync(id)).ReturnsAsync((UserInfoViewModel)null);

        // Act
        var result = await controller.Info(id);

        // Assert
        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async Task Info_WhenUserExists_ReturnsViewResultWithModel()
    {
        // Arrange
        var id = "1";
        var model = new UserInfoViewModel
        {
            Email = "test@test.com",
            Role = "WhiskyExpert",
            TotalReviews = 10,
            TotalRatings = 5,
            TotalComments = 15,
            JoinedEvents = 3,
            FavouriteWhiskies = 7,
            AddedWhiskies = 2,
            OrganisedEvents = 1,
            WrittenArticles = 4
        };

        mockUserService.Setup(x => x.GetUserInfoAsync(id)).ReturnsAsync(model);

        // Act
        var result = await controller.Info(id);

        // Assert
        Assert.IsInstanceOf<ViewResult>(result);
        var viewResult = result as ViewResult;
        Assert.AreEqual(model, viewResult.Model);
    }

}

