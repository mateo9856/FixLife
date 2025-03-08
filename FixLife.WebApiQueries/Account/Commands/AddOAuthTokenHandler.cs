using AutoMapper;
using FixLife.WebApiDomain.Enums;
using FixLife.WebApiInfra.Abstraction.Identity;
using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    internal class AddOAuthTokenHandler : IRequestHandler<AddOAuthTokenCommand, ClientIdentityResponse>
    {
        private readonly IClientIdentityService _clientIdentityService;
        private readonly IMapper _mapper;

        public AddOAuthTokenHandler(IClientIdentityService clientIdentityService, IMapper mapper)
        {
            _clientIdentityService = clientIdentityService;
            _mapper = mapper;
        }

        public Task<ClientIdentityResponse> Handle(AddOAuthTokenCommand request, CancellationToken cancellationToken)
        {
            //TODO: Implement AddOAuthTokenHandler
            var loginUser = _clientIdentityService.AddOrLoginOAuthUserAsync(request.Email, Enum.Parse<OAuthLoginProvider>(request.OAuthProvider));

            throw new NotImplementedException();
        }
    }
}
