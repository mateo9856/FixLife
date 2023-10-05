using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan.Queries
{
    public class GetUserPlanIdQuery : IRequest<(short, string)>
    {
        public string UserId { get; set; }
    }
}
