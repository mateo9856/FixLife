using AutoMapper;
using FixLife.WebApiDomain.User;
using FixLife.WebApiInfra.Abstraction.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var response = await _clientIdentityService.LoginAsync(user);

            return _mapper.Map<ClientIdentityResponse>(response);

        }
    }
}
