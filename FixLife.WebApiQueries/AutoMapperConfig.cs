﻿using AutoMapper;
using FixLife.WebApiDomain.Models;
using FixLife.WebApiDomain.Plan;
using FixLife.WebApiQueries.Account;
using FixLife.WebApiQueries.Dashboard.Queries;
using FixLife.WebApiQueries.FirstPlan;

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

            CreateMap<CreatePlanRequest, PlanModel>()
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<EditPlanRequest, PlanModel>()
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<GetDashboardQueryResponse, PlanModel>()
                .ForMember(d => d.UserId, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
