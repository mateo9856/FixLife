using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using FixLife.WebApiQueries.Dashboard.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan.Queries
{
    public class GetUserPlanIdQueryHandler : IRequestHandler<GetUserPlanIdQuery, (short, string)>
    {

        private readonly IPlanService _planService;

        public GetUserPlanIdQueryHandler(IPlanService planService)
        {
            _planService = planService;
        }

        public Task<(short, string)> Handle(GetUserPlanIdQuery request, CancellationToken cancellationToken)
            => _planService.GetPlanIdAsync(request.UserId);
    }
}
