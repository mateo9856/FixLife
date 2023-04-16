using AutoMapper;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiQueries.Account;
using FixLife.WebApiQueries.FirstPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.WebApiQueries
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ClientIdentityResponse, FixLife.WebApiInfra.Services.Identity.ClientIdentityResponse>().ReverseMap();
            CreateMap<CreatePlanRequest, Plan>().ReverseMap();
        }
    }
}
