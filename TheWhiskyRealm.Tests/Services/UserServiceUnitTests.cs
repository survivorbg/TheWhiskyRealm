using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.User;
using TheWhiskyRealm.Core.Services;
using TheWhiskyRealm.Infrastructure.Data;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Tests.Services;

[TestFixture]
public class UserServiceUnitTests
{
    private TheWhiskyRealmDbContext dbContext;
    private IRepository repository;
    private UserManager<ApplicationUser> userManager;
    private IUserService service;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<TheWhiskyRealmDbContext>()
            .UseInMemoryDatabase(databaseName: "TheWhiskyRealmInMemoryDb" + Guid.NewGuid().ToString())
            .Options;

        dbContext = new TheWhiskyRealmDbContext(options);
        repository = new Repository(dbContext);
        var store = new UserStore<ApplicationUser>(dbContext);
        userManager = new UserManager<ApplicationUser>(store, null, null, null, null, null, null, null, null);
        service = new UserService(repository, userManager);

        var user = new ApplicationUser
        {
            Id = "TestUserId",
            UserName = "TestUser@test.com",
            Email = "TestUser@test.com",
            DateOfBirth = DateTime.Now.AddYears(-20)
        };
        await dbContext.Users.AddAsync(user);

        await dbContext.SaveChangesAsync();
    }

    [TearDown]
    public async Task Teardown()
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.DisposeAsync();
    }

    [Test]
    public async Task GetAllUsersAsync_ShouldReturnCorrectUsers()
    {
        // Arrange
        var currentPage = 1;
        var pageSize = 10;

        // Act
        var result = await service.GetAllUsersAsync(currentPage, pageSize);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<UserViewModel>>(result);
        Assert.AreEqual(1, result.Count());

        var userViewModel = result.First();
        Assert.AreEqual("TestUserId", userViewModel.Id);
        Assert.AreEqual("TestUser@test.com", userViewModel.Username);
        Assert.AreEqual("No role", userViewModel.Role);
        Assert.AreEqual(DateTime.Now.AddYears(-20).Date, userViewModel.DateOfBirth.Date);
        Assert.IsFalse(userViewModel.IsLocked);
    }

    [Test]
    public async Task GetTotalUsersAsync_ShouldReturnCorrectCount()
    {
        // Act
        var result = await service.GetTotalUsersAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result);
    }

    [Test]
    public async Task GetUserInfoAsync_ShouldReturnCorrectInfo()
    {
        // Arrange
        var userId = "TestUserId";

        // Act
        var result = await service.GetUserInfoAsync(userId);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("TestUser@test.com", result.Email);
        Assert.AreEqual("No role", result.Role);
        Assert.AreEqual(0, result.TotalReviews);
        Assert.AreEqual(0, result.TotalRatings);
        Assert.AreEqual(0, result.TotalComments);
        Assert.AreEqual(0, result.JoinedEvents);
        Assert.AreEqual(0, result.FavouriteWhiskies);
        Assert.AreEqual(0, result.AddedWhiskies);
        Assert.AreEqual(0, result.OrganisedEvents);
        Assert.AreEqual(0, result.WrittenArticles);
    }


}