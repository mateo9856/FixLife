using AutoMapper;
using FixLife.WebApiDomain.Exceptions;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
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
        private readonly IPlanService _planService;
        public EditPlanHandler(IMapper mapper, IPlanService planService) {
            _mapper = mapper;
            _planService = planService;
        }
        public async Task<EditPlanResponse> Handle(EditPlanCommand request, CancellationToken cancellationToken)
        {
            var mapPlan = _mapper.Map<Plan>(request.Request);

            if (mapPlan != null)
            {
                mapPlan.WeeklyWork.DayOfWeeks = request.Request.WeeklyWork.DayOfWeeks.Select(d => new FixLife.WebApiDomain.Common.DayOfWeek
                {
                    Day = d,
                    CreatedDate = DateTime.Now
                }).ToList();
            }

            mapPlan.UpdatedDate = DateTime.Now;

            var GetActualPlan = await _planService.GetPlanAsync(request.UserId);

            if (GetActualPlan == null)
                throw new PlanNotFoundException("Plan was not found!");

            var mapperPlan = _mapper.Map(mapPlan, GetActualPlan);

            var serviceResult = await _planService.EditPlanAsync(mapperPlan, GetActualPlan, request.UserId);

            return new EditPlanResponse { Status = serviceResult.Item1, Message = serviceResult.Item2 };
        }
    }
}
