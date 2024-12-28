using AutoMapper;
using FixLife.WebApiDomain.Models;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using MediatR;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, (short, GetDashboardQueryResponse)>
    {
        private readonly IDashboardService _dashboardService;
        private readonly IMapper _mapper;

        public GetDashboardDataQueryHandler(IDashboardService dashboard, IMapper mapper) { 
            _dashboardService = dashboard;
            _mapper= mapper;
        }

        public async Task<(short, GetDashboardQueryResponse)> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            var ResponseData = await _dashboardService.GetDashboardData(request.UserId);

            if (ResponseData.Item2 is null)
                return (ResponseData.Item1, null);

            var mappedResponse = _mapper.Map<GetDashboardQueryResponse>(ResponseData.Item2);

            return (ResponseData.Item1, mappedResponse);
        }
    }
}
