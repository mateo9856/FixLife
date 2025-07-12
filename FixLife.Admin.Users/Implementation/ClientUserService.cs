using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Entities;
using FixLife.Admin.Db.Implementations;
using FixLife.Admin.Db.Tools;
using FixLife.Admin.Users.Abstraction;
using FixLife.Admin.Users.Exceptions;

namespace FixLife.Admin.Users.Implementation
{
    public class ClientUserService : EntityOperationsBase<ClientUser>, IClientUserService
    {
        public ClientUserService(AdminContext adminContext) : base(adminContext)
        {
        }

        public async Task<(short, string)> LogoutForce(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
                throw new ClientNotFoundException();
            // TODO: Communication to client api and send request
            return (0, "User logged out (forced)");
        }

        public async Task<(short, string)> ModifyUser(Guid userId, Models.ClientUser user)
        {
            var existing = await GetByIdAsync(userId);
            if (existing == null)
                throw new ClientNotFoundException();
            existing.Name = user.Name;
            existing.Surname = user.Surname;
            existing.Email = user.Email;
            existing.PhoneNumber = user.PhoneNumber;
            Update(existing);
            await SaveChangesAsync();
            return (0, "User updated");
        }
        
        public async Task<(short, string)> ResetUserPassword(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
                throw new ClientNotFoundException();
            
            return (200, "Redirect to page!");
        }

        public async Task<(short, string)> ConfirmUserPassword(Guid userId, string password)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
                throw new ClientNotFoundException();
            var hasher = new PasswordHasher();
            user.Password = hasher.Hash(password);
            if (hasher.Verify(password, user.Password))
                return (200, "Password confirmed");
            else
                return (400, "Invalid password");
        }
    }
}
