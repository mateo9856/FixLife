using FixLife.WebApiDomain.Enums;
using FixLife.WebApiQueries.Account.Commands;
using FluentValidation;

namespace FixLife.WebApiQueries.Validators
{
    public class OAuthTokenValidator : AbstractValidator<AddOAuthTokenCommand>
    {
        public OAuthTokenValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();
            
            RuleFor(x => x.OAuthProvider)
                .NotNull()
                .NotEmpty()
                .IsEnumName(typeof(OAuthLoginProvider));
        }
    }
}
