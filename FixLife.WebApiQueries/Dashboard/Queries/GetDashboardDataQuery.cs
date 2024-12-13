using FixLife.WebApiDomain.Models;
using MediatR;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardDataQuery : IRequest<(short, PlanModel)>
    {
        public string UserId { get; set; }
    
    }
}
