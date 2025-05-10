using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Implementations;
using FixLife.Admin.Identity.Abstractions;
using FixLife.Admin.Identity.Exceptions;
using FixLife.Admin.Identity.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using EntityUser = FixLife.Admin.Db.Entities.AdminUser;

namespace FixLife.Admin.Identity.Implementations
{
    public class AdminIdentityService : EntityOperationsBase<EntityUser>, IAdminIdentityService
    {
        public AdminIdentityService(AdminContext adminContext) : base(adminContext)
        {
        }

        public async Task<(int, string)> LoginAdmin(AdminUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<(int, string)> LogoutAdmin(bool force = false)
        {
            throw new NotImplementedException();
        }

        private async Task<EntityUser> GetByNameAndPassword(string name, string password)
        {
            return await _dbTable.FirstOrDefaultAsync(d => d.Name == name && d.Password == password)
                ?? throw new UserNotFoundException(name);
        }

        private ClaimsPrincipal GenerateClaims(EntityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Name, user.Surname),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(claims, "AdminIdentity");
            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}
