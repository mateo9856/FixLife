using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.FirstPlan.Commands
{
    public class EditPlanHandler : IRequestHandler<EditPlanCommand, EditPlanResponse>
    {
        private readonly IMapper _mapper;
        public EditPlanHandler(IMapper mapper) {
            _mapper = mapper;
        }
        public Task<EditPlanResponse> Handle(EditPlanCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
