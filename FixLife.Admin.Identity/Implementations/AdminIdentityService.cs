using FixLife.Admin.Db.Context;
using FixLife.Admin.Db.Implementations;
using FixLife.Admin.Db.Tools;
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
        private const string NoReplyMail = "noreply@fixlife.com";

        private PasswordHasher PassHasher { get; }
        private EmailSender EmailSender { get; }

        public AdminIdentityService(AdminContext adminContext) : base(adminContext)
        {
            PassHasher = new();
            EmailSender = new();
        }

        public async Task<(int, string)> LoginAdmin(AdminUser user, bool sendConfirm)
        {
            var passHash = PassHasher.Hash(user.Password);
            if(!PassHasher.Verify(user.Password, passHash))
                throw new InvalidPasswordException();
            
            var dbUser = await GetByNameAndPassword(user.Name, passHash);

            if(dbUser is null)
                throw new UserNotFoundException(user.Name);

            if(sendConfirm)
            {
                EmailSender.SendEmail(NoReplyMail, user.Email);
                return (200, $"Confirmation email sent!{Environment.NewLine}Please check your inbox to confirm your identity.");
            }
                
            var claims = GenerateClaims(dbUser);
            return (200, dbUser.Name);
        }

        public async Task<(int, string)> LogoutAdmin(bool force = false)
        {
            if (force)
                return (200, "Logout forced!");
            
            return (200, "Logout process started! Wait 1 minute to logout");
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
