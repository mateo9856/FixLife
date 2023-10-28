using FixLife.WebApiDomain.Plan;
using FixLife.WebApiQueries.FirstPlan.DTOHelpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardDataQuery : IRequest<(short, PlanDTO)>
    {
        public string UserId { get; set; }
    
    }
}
