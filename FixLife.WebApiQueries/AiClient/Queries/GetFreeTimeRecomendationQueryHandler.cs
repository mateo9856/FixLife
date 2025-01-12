using FixLife.WebApiInfra.Abstraction;
using MediatR;

namespace FixLife.WebApiQueries.AiClient.Queries
{
    public class GetFreeTimeRecomendationQueryHandler : IRequestHandler<GetFreeTimeRecomendationQuery, (short, GetFreeTimeRecomendationResponse)>
    {
        private readonly IPlanService _planService;

        public GetFreeTimeRecomendationQueryHandler(IPlanService planService)
        {
            _planService = planService;
        }

        public async Task<(short, GetFreeTimeRecomendationResponse)> Handle(GetFreeTimeRecomendationQuery request, CancellationToken cancellationToken)
        {
            var result = await _planService.GetFreeTimeRecommendation(request.Count);
            return (result.Item1, new GetFreeTimeRecomendationResponse
            {
                FreeTimes = result.Item2
            });
        }
    }
}
 