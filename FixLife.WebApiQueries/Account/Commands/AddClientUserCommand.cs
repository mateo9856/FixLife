using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.WebApiQueries.Account.Commands
{
    public record AddClientUserCommand(ClientIdentityRegisterRequest request) : IRequest<ClientIdentityResponse>;
}
