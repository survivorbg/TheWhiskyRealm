using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.User;
using TheWhiskyRealm.Infrastructure.Data.Common;
using TheWhiskyRealm.Infrastructure.Data.Models;

namespace TheWhiskyRealm.Core.Services;

public class UserService : IUserService
{
    private readonly IRepository repo;
    private readonly UserManager<ApplicationUser> userManager;

    public UserService(IRepository repo, UserManager<ApplicationUser> userManager)
    {
        this.repo = repo;
        this.userManager = userManager;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync(int currentPage, int pageSize)
    {
        var users = await repo
            .AllReadOnly<ApplicationUser>()
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var userViewModels = new List<UserViewModel>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            userViewModels.Add(new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Role = roles.FirstOrDefault() != null ? roles.First() : "No role",
                DateOfBirth = user.DateOfBirth,
                IsLocked = await userManager.IsLockedOutAsync(user),
            });
        }

        return userViewModels;
    }


    public async Task<int> GetTotalUsersAsync()
    {
        return await repo
            .AllReadOnly<ApplicationUser>()
            .CountAsync();
    }

    public async Task<UserInfoViewModel?> GetUserInfoAsync(string id)
    {
        var user = await repo.GetByIdAsync<ApplicationUser>(id);
        if (user != null)
        {
            var model = await repo.AllReadOnly<ApplicationUser>()
                .Where(u => u.Id == id)
                .Select(u => new UserInfoViewModel
                {
                    Email = u.Email,
                    TotalComments = u.Comments.Count(),
                    TotalRatings = u.Ratings.Count(),
                    TotalReviews = u.Reviews.Count(),
                    FavouriteWhiskies = u.UsersWhiskies.Count(),
                    JoinedEvents = u.UsersEvents.Count()
                })
                .FirstOrDefaultAsync();
            var roles = await userManager.GetRolesAsync(user);
            model!.Role = roles.SingleOrDefault() ?? "No role";
            model.AddedWhiskies = await repo.AllReadOnly<Whisky>().Where(w => w.PublishedBy == id).CountAsync();
            model.WrittenArticles = await repo.AllReadOnly<Article>().Where(w => w.PublisherUserId == id).CountAsync();
            model.OrganisedEvents = await repo.AllReadOnly<Event>().Where(w => w.OrganiserId == id).CountAsync();
            return model;
        }

        return null;
    }
}
