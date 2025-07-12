using MediatR;

namespace FixLife.WebApiQueries.Account.Commands;

public record LogoutForceCommand(string UserId) : IRequest<ClientIdentityResponse>;