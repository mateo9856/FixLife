using AutoMapper;
using FixLife.WebApiDomain.Enums;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiQueries.Account;
using FixLife.WebApiQueries.Dashboard.Queries;
using FixLife.WebApiQueries.FirstPlan;
using FixLife.WebApiQueries.FirstPlan.DTOHelpers;

namespace FixLife.WebApiQueries
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ClientIdentityResponse, FixLife.WebApiInfra.Services.Identity.ClientIdentityResponse>().ReverseMap();

            CreateMap<FreeTimeDTO, FreeTime>()
                .ForMember(d => d.TimeEnd, opt => opt.MapFrom(e => TimeSpan.Parse(e.TimeEnd)))
                .ForMember(d => d.TimeStart, opt => opt.MapFrom(e => TimeSpan.Parse(e.TimeStart)))
                .ReverseMap();

            CreateMap<WeeklyWorkDTO, WeeklyWork>()
                .ForMember(d => d.DayOfWeeks, opt => opt.Ignore())
                .ForMember(d => d.TimeStart, opt => opt.MapFrom(e => TimeSpan.Parse(e.TimeStart)))
                .ForMember(d => d.TimeEnd, opt => opt.MapFrom(e => TimeSpan.Parse(e.TimeEnd)))
                .ReverseMap();

            CreateMap<LearnTimeDTO, LearnTime>()
                .ForMember(d => d.DayOfWeeks, opt => opt.Ignore())
                .ForMember(d => d.StartTime, opt => opt.MapFrom(e => TimeSpan.Parse(e.StartTime)))
                .ForMember(d => d.TimeInterval, opt => opt.MapFrom(e => TimeSpan.Parse(e.TimeInterval)))
                .ReverseMap();

            CreateMap<Plan, PlanDTO>()
                .ForMember(d => d.WeeklyWork, opt => opt.MapFrom(d => new WeeklyWorkDTO
                {
                    DayOfWeeks = d.WeeklyWork.DayOfWeeks.Select(a => a.Day).ToList(),
                    TimeEnd = d.WeeklyWork.TimeEnd.ToString(),
                    TimeStart = d.WeeklyWork.TimeStart.ToString(),
                }))
                .ForMember(d => d.LearnTime, opt => opt.MapFrom(d => new LearnTimeDTO
                {
                    DayOfWeeks = d.LearnTime.DayOfWeeks.Select(a => a.Day).ToList(),
                    TimeInterval = d.LearnTime.TimeInterval.ToString(),
                    StartTime = d.LearnTime.StartTime.ToString(),
                }))
                .ReverseMap();

            CreateMap<CreatePlanRequest, Plan>()
                .ReverseMap();
            CreateMap<EditPlanRequest, Plan>()
                .ReverseMap();
            CreateMap<PlanDTO, GetDashboardQueryResponse>()
                .ReverseMap();
        }
    }
}
