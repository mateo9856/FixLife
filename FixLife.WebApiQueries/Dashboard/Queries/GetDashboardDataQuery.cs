using FixLife.WebApiDomain.Plan;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardDataQuery : IRequest<(short, Plan)>
    {
        public string UserId { get; set; }
    
    }
}
