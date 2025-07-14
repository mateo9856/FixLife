using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    public record ResetPasswordCommand(string UserId) : IRequest<(short status, string content)>;
} 