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

        public async Task<ClientIdentityResponse> Handle(AddOAuthTokenCommand request, CancellationToken cancellationToken)
        {
            var parsedProvider = Enum.Parse<OAuthLoginProvider>(request.OAuthProvider);
            var loginUser = await _clientIdentityService.AddOrLoginOAuthUserAsync(request.Email, parsedProvider);

            return _mapper.Map<ClientIdentityResponse>(loginUser);
        }
    }
}
