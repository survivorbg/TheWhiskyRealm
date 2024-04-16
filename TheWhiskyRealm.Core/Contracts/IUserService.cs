using TheWhiskyRealm.Core.Models.AdminArea.User;

namespace TheWhiskyRealm.Core.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserViewModel>> GetAllUsersAsync(int currentPage, int pageSize);
    Task<int> GetTotalUsersAsync();
    

}
