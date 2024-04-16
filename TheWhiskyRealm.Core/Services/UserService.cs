using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheWhiskyRealm.Core.Contracts;
using TheWhiskyRealm.Core.Models.AdminArea.Region;
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
}
