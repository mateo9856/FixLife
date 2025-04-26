using FixLife.Admin.Users.Models;

namespace FixLife.Admin.Users.Abstraction
{
    public interface IClientUserService
    {
        Task<(short, string)> LogoutForce(Guid userId);
        Task<(short, string)> ModifyUser(Guid userId, ClientUser user);
        Task<(short, string)> ResetUserPassword(Guid userId);
    }
}
