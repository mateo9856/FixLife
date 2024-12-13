using FixLife.WebApiDomain.Models;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using MediatR;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, (short, PlanModel)>
    {
        private readonly IDashboardService _dashboardService;

        public GetDashboardDataQueryHandler(IDashboardService dashboard) { 
            _dashboardService= dashboard;
        }

        public async Task<(short, PlanModel)> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            var ResponseData = await _dashboardService.GetDashboardData(request.UserId);
            
            return ResponseData;
        }
    }
}
