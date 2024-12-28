using AutoMapper;
using FixLife.WebApiDomain.User;
using FixLife.WebApiInfra.Abstraction.Identity;
using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    public class AddClientUserHandler : IRequestHandler<AddClientUserCommand, ClientIdentityResponse>
    {
        private readonly IClientIdentityService _clientIdentityService;
        private readonly IMapper _mapper;

        public AddClientUserHandler(IClientIdentityService clientIdentityService, IMapper mapper)
        {
            _mapper = mapper;
            _clientIdentityService = clientIdentityService;
        }

        public async Task<ClientIdentityResponse> Handle(AddClientUserCommand request, CancellationToken cancellationToken)
        {
            var elementRequest = request.request;

            var user = new ClientUser
            {
                Email = elementRequest.Email,
                Password = elementRequest.Password,
                PhoneNumber = elementRequest.PhoneNumber
            };

            var register = await _clientIdentityService.RegisterAsync(user);
            
            return _mapper.Map<ClientIdentityResponse>(register);
        }
    }
}
