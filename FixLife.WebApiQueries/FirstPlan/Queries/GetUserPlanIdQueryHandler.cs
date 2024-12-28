using FixLife.WebApiInfra.Abstraction;
using MediatR;

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
