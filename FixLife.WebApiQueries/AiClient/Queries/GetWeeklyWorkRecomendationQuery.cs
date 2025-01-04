using MediatR;

namespace FixLife.WebApiQueries.AiClient.Queries
{
    public class GetWeeklyWorkRecomendationQuery : IRequest<(short, GetWeeklyWorkRecomendationResponse)>
    {
    }
}
