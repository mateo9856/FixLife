using AutoMapper;
using FixLife.WebApiInfra.Abstraction.Identity;
using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ClientIdentityResponse>
    {
        private readonly IClientIdentityService _clientIdentityService;
        private readonly IMapper _mapper;

        public LogoutCommandHandler(IClientIdentityService clientIdentityService, IMapper mapper)
        {
            _clientIdentityService = clientIdentityService;
            _mapper = mapper;
        }

        public async Task<ClientIdentityResponse> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var response = await _clientIdentityService.LogoutAsync(request.UserId);
            return _mapper.Map<ClientIdentityResponse>(response);
        }
    }
} 