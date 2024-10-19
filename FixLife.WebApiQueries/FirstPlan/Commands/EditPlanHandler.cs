using AutoMapper;
using FixLife.WebApiDomain.Exceptions;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using FluentValidation;
using MediatR;

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

            if (mapPlan == null)
            {
                throw new ValidationException("Request after map is null");
            }

            mapPlan.WeeklyWork.DayOfWeeks = request.Request.WeeklyWork.DayOfWeeks;

            mapPlan.LearnTime.DayOfWeeks = request.Request.LearnTime.DayOfWeeks;

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
