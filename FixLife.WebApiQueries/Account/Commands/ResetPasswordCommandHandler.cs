using AutoMapper;
using FixLife.WebApiInfra.Abstraction.Identity;
using MediatR;

namespace FixLife.WebApiQueries.Account.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, (short status, string content)>
    {
        private readonly IClientIdentityService _clientIdentityService;
        private readonly IMapper _mapper;

        public ResetPasswordCommandHandler(IClientIdentityService clientIdentityService, IMapper mapper)
        {
            _clientIdentityService = clientIdentityService;
            _mapper = mapper;
        }

        public async Task<(short status, string content)> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _clientIdentityService.ResetPasswordAsync(request.UserId);
        }
    }
} 