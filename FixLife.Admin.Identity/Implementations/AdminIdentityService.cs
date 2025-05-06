using FixLife.Admin.Identity.Abstractions;
using FixLife.Admin.Identity.Models;

namespace FixLife.Admin.Identity.Implementations
{
    internal class AdminIdentityService : IAdminIdentityService
    {
        public Task<(int, string)> LoginAdmin(AdminUser user)
        {
            throw new NotImplementedException();
        }

        public Task<(int, string)> LogoutAdmin(bool force = false)
        {
            throw new NotImplementedException();
        }
    }
}
