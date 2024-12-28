using AutoMapper;
using FixLife.WebApiDomain.Models;
using FixLife.WebApiInfra.Abstraction;
using FluentValidation;
using MediatR;

namespace FixLife.WebApiQueries.FirstPlan.Commands
{
    public class AddPlanHandler : IRequestHandler<AddPlanCommand, CreatePlanResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPlanService _planService;
        public AddPlanHandler(IMapper mapper, IPlanService planService)
        {
            _mapper = mapper;
            _planService = planService;
        }

        public async Task<CreatePlanResponse> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
            var mapPlan = _mapper.Map<PlanModel>(request.Request);
            
            if(mapPlan == null)
            {
                throw new ValidationException("Validation after map is null");
            }

            mapPlan.WeeklyWork.DayOfWeeks = request.Request.WeeklyWork.DayOfWeeks;

            mapPlan.LearnTime.DayOfWeeks = request.Request.LearnTime.DayOfWeeks;

            var serviceResult = await _planService.CreatePlanAsync(mapPlan, true, request.UserId);

            return new CreatePlanResponse { Status = serviceResult.Item1, Message = serviceResult.Item2 };
        }
    }
}
