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

            CreateMap<FreeTime, FreeTimeDTO>()
                .ForMember(d => d.TimeEnd, opt => opt.MapFrom(e => e.TimeEnd.ToString()))
                .ForMember(d => d.TimeStart, opt => opt.MapFrom(e => e.TimeStart.ToString()))
                .ReverseMap();

            CreateMap<WeeklyWork, WeeklyWorkDTO>()
                .ForMember(d => d.TimeStart, opt => opt.MapFrom(e => e.TimeStart.ToString()))
                .ForMember(d => d.TimeEnd, opt => opt.MapFrom(e => e.TimeEnd.ToString()))
                .ReverseMap();

            CreateMap<LearnTime, LearnTimeDTO>()
                .ForMember(d => d.StartTime, opt => opt.MapFrom(e => e.StartTime.ToString()))
                .ForMember(d => d.TimeInterval, opt => opt.MapFrom(e => e.TimeInterval.ToString()))
                .ReverseMap();

            CreateMap<CreatePlanRequest, Plan>()
                .ReverseMap();
        }
    }
}
