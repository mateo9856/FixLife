using AutoMapper;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var mapPlan = _mapper.Map<Plan>(request.Request);
            
            if(mapPlan != null)
            {
                mapPlan.WeeklyWork.DayOfWeeks = request.Request.WeeklyWork.DayOfWeeks.Select(d => new FixLife.WebApiDomain.Common.DayOfWeek
                {
                    Day = d,
                    CreatedDate= DateTime.Now
                }).ToList();
            }

            mapPlan.CreatedDate = DateTime.Now;
            var serviceResult = await _planService.CreatePlanAsync(mapPlan, true, request.UserId);

            return new CreatePlanResponse { Status = serviceResult.Item1, Message = serviceResult.Item2 };
        }
    }
}
