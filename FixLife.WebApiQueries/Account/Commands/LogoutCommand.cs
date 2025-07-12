using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    public record LogoutCommand(string UserId) : IRequest<ClientIdentityResponse>;
} 