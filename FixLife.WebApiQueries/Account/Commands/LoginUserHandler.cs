using AutoMapper;
using FixLife.WebApiDomain.User;
using FixLife.WebApiInfra.Abstraction.Identity;
using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ClientIdentityResponse>
    {
        private readonly IClientIdentityService _clientIdentityService;
        private readonly IMapper _mapper;

        public LoginUserHandler(IClientIdentityService clientIdentityService, IMapper mapper)
        {
            _clientIdentityService = clientIdentityService;
            _mapper = mapper;
        }

        public async Task<ClientIdentityResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var elementRequest = request.request;

            var user = new ClientUser();

            if (elementRequest.Credentials.Contains("@")) user.Email = elementRequest.Credentials;
            else user.PhoneNumber = elementRequest.Credentials;

            user.Password = elementRequest.Password;

            var response = await _clientIdentityService.LoginAsync(user);

            return _mapper.Map<ClientIdentityResponse>(response);

        }
    }
}
