using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan.Commands
{
    public class AddPlanHandler : IRequestHandler<AddPlanCommand, CreatePlanResponse>
    {
        public Task<CreatePlanResponse> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {//TODO: Implement
            throw new NotImplementedException();
        }
    }
}
