using MediatR;

namespace FixLife.WebApiQueries.AiClient.Queries
{
    public class GetFreeTimeRecomendationQuery : IRequest<(short, GetFreeTimeRecomendationResponse)>
    {
        public int Count { get; set; }
    }
}
