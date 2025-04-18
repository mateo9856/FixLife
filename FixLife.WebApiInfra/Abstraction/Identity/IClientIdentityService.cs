using FixLife.WebApiDomain.Enums;
using FixLife.WebApiDomain.User;
using FixLife.WebApiInfra.Services.Identity;

namespace FixLife.WebApiInfra.Abstraction.Identity
{
    public interface IClientIdentityService
    {
        string UserId { get; }
        Task<ClientUser> GetClientUser(string userId);
        Task<ClientIdentityResponse> LoginAsync(ClientUser request);
        Task<ClientIdentityResponse> LogoutAsync();
        Task<ClientIdentityResponse> RegisterAsync(ClientUser request);
        Task<ClientIdentityResponse> AddOrLoginOAuthUserAsync(string email, OAuthLoginProvider oauthAccount);

    }
}
