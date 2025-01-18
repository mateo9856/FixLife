using MediatR;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardDataQuery : IRequest<(short, GetDashboardQueryResponse)>
    {
        public string UserId { get; set; }
    
    }
}
