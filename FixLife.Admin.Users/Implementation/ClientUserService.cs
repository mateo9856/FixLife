using FixLife.Admin.Users.Abstraction;
using FixLife.Admin.Users.Models;

namespace FixLife.Admin.Users.Implementation
{
    internal class ClientUserService : IClientUserService
    {
        public Task<(short, string)> LogoutForce(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<(short, string)> ModifyUser(Guid userId, ClientUser user)
        {
            throw new NotImplementedException();
        }

        public Task<(short, string)> ResetUserPassword(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
