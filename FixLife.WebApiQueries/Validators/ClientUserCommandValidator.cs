using FixLife.WebApiQueries.Account.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Validators
{
    public sealed class ClientUserCommandValidator : AbstractValidator<AddClientUserCommand>
    {
        public ClientUserCommandValidator()
        {
            RuleFor(x => x.request.Email).EmailAddress();
            RuleFor(x => x.request.Password).MinimumLength(8);
            RuleFor(x => x.request.PhoneNumber).MinimumLength(9).MaximumLength(15);
        }
    }
}
