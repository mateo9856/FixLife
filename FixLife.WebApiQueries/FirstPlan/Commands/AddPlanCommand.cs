using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan.Commands
{
    public class AddPlanCommand : IRequest<CreatePlanResponse>
    {
        public CreatePlanRequest Request { get; set; }
        public string UserId { get; set; }
        public AddPlanCommand(CreatePlanRequest request, string userId)
        {
            Request = request;
            UserId = userId;
        }
    }
}
