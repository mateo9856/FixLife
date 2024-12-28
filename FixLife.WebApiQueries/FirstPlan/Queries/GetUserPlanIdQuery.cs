using MediatR;

namespace FixLife.WebApiQueries.FirstPlan.Queries
{
    public class GetUserPlanIdQuery : IRequest<(short, string)>
    {
        public string UserId { get; set; }
    }
}
