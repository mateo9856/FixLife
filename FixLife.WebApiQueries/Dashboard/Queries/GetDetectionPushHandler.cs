using FixLife.WebApiInfra.Abstraction.Dashboard;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDetectionPushHandler : IRequestHandler<GetDetectionPushQuery, GetDetectionPushResponse>
    {
        private readonly IDashboardService _dashboardService;
        public GetDetectionPushHandler(IDashboardService dashboardService) { 
            _dashboardService= dashboardService;
        }

        public Task<GetDetectionPushResponse> Handle(GetDetectionPushQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
