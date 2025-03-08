using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    public record AddOAuthTokenCommand(string Token, string Email, string OAuthProvider) : IRequest<ClientIdentityResponse>;
}
