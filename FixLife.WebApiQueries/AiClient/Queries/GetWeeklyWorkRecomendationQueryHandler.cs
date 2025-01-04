using FixLife.WebApiInfra.Abstraction;
using MediatR;

namespace FixLife.WebApiQueries.AiClient.Queries
{
    public class GetWeeklyWorkRecomendationQueryHandler : IRequestHandler<GetWeeklyWorkRecomendationQuery, (short, GetWeeklyWorkRecomendationResponse)>
    {
        private readonly IPlanService _planService;

        public GetWeeklyWorkRecomendationQueryHandler(IPlanService planService)
        {
            _planService = planService;
        }

        public async Task<(short, GetWeeklyWorkRecomendationResponse)> Handle(GetWeeklyWorkRecomendationQuery request, CancellationToken cancellationToken)
        {
            var result = await _planService.GetWeeklyWorkRecommendation();
            return (result.Item1, new GetWeeklyWorkRecomendationResponse
            {
                WeeklyWorks = result.Item2
            });
        }
    }
}
 