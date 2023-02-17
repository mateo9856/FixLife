using FixLife.WebApiQueries.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiInfra.Abstraction.Identity
{
    public interface IClientIdentityService
    {
        Task<ClientIdentityResponse> LoginAsync(ClientIdentityRequest request);
        Task<ClientIdentityResponse> LogoutAsync();
        Task<ClientIdentityResponse> RegisterAsync(ClientIdentityRegisterRequest request);

    }
}
