using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Implementations;
using FixLife.Admin.Users.Abstraction;
using FixLife.Admin.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace FixLife.Admin.Users.Implementation
{
    public class ClientUserService : DbContextRepository, IClientUserService
    {
        private DbSet<ClientUser> _clientUsers;

        public ClientUserService(AdminContext adminContext) : base(adminContext)
        {
            _clientUsers = GetDbSet<ClientUser>();
        }

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
