using FixLife.Admin.Identity.Models;

namespace FixLife.Admin.Identity.Abstractions
{
    internal interface IAdminIdentityService
    {
        Task<(int, string)> LoginAdmin(AdminUser user, bool sendConfirm);
        Task<(int, string)> LogoutAdmin(bool force = false);
    }
}
