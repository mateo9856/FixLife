using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    public record AddClientUserCommand(ClientIdentityRegisterRequest request) : IRequest<ClientIdentityResponse>;
}
