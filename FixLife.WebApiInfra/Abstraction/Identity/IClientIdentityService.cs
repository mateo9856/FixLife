using FixLife.WebApiDomain.User;
using FixLife.WebApiInfra.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Abstraction.Identity
{
    public interface IClientIdentityService
    {
        Guid UserId { get; }
        Task<ClientUser> GetClientUser(string userId);
        Task<ClientIdentityResponse> LoginAsync(ClientUser request);
        Task<ClientIdentityResponse> LogoutAsync();
        Task<ClientIdentityResponse> RegisterAsync(ClientUser request);

    }
}
