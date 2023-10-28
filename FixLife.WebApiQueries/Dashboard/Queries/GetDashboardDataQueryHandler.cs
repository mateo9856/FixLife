using AutoMapper;
using FixLife.WebApiDomain.Exceptions;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiInfra.Abstraction.Dashboard;
using FixLife.WebApiQueries.FirstPlan.DTOHelpers;
using MediatR;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries.Dashboard.Queries
{
    public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, (short, PlanDTO)>
    {

        private readonly IDashboardService _dashboardService;
        private readonly IMapper _mapper;

        public GetDashboardDataQueryHandler(IDashboardService dashboard, IMapper mapper) { 
            _dashboardService= dashboard;
            _mapper= mapper;
        }

        public async Task<(short, PlanDTO)> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            var ResponseData = await _dashboardService.GetDashboardData(request.UserId);

            if(ResponseData.Item2 == null)
            {
                throw new PlanNotFoundException("Plan not found!");
            }

            var responseResult = _mapper.Map<PlanDTO>(ResponseData.Item2);

            return (ResponseData.Item1, responseResult);
        }
    }
}
