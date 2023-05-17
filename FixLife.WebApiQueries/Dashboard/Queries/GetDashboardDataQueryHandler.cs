using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, (short, Plan)>
    {

        private readonly IDashboardService _dashboardService;

        public GetDashboardDataQueryHandler(IDashboardService dashboard) { 
            _dashboardService= dashboard;
        }

        public async Task<(short, Plan)> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            var ResponseData = await _dashboardService.GetDashboardData(request.UserId);
            return ResponseData;
        }
    }
}
