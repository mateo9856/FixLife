using FixLife.Admin.Identity.Abstractions;

namespace FixLife.Admin.Identity.Implementations
{
    internal class AdminIdentityService : IAdminIdentityService
    {
        public Task<(int, string)> LoginAdmin()
        {
            throw new NotImplementedException();
        }

        public Task<(int, string)> LogoutAdmin(bool force = false)
        {
            throw new NotImplementedException();
        }
    }
}
