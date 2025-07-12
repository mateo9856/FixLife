using AutoMapper;
using FixLife.WebApiInfra.Abstraction.Identity;
using MediatR;

namespace FixLife.WebApiQueries.Account.Commands;

public class LogoutForceCommandHandler : IRequestHandler<LogoutForceCommand, ClientIdentityResponse>
{
    private readonly IClientIdentityService _clientIdentityService;
    private readonly IMapper _mapper;

    public LogoutForceCommandHandler(IClientIdentityService clientIdentityService, IMapper mapper)
    {
        _clientIdentityService = clientIdentityService;
        _mapper = mapper;
    }

    public async Task<ClientIdentityResponse> Handle(LogoutForceCommand request, CancellationToken cancellationToken)
    {
        var response = await _clientIdentityService.LogoutForceAsync(request.UserId);
        return _mapper.Map<ClientIdentityResponse>(response);
    }
}