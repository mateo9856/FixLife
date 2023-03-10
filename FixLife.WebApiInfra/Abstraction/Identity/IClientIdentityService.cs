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
        Task<ClientIdentityResponse> LoginAsync(ClientUser request);
        Task<ClientIdentityResponse> LogoutAsync();
        Task<ClientIdentityResponse> RegisterAsync(ClientUser request);

    }
}
