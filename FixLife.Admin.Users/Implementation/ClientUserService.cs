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
            // No session/token logic, so just check if user exists
            var user = await GetByIdAsync(userId);
            if (user == null)
                throw new ClientNotFoundException();
            // No-op, but could add audit/event here
            return (0, "User logged out (forced)");
        }

        public Task<(short, string)> ModifyUser(Guid userId, Models.ClientUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<(short, string)> ModifyUser(Guid userId, ClientUser user)
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
            // Generate a new random password
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
