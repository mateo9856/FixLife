﻿using AutoMapper;
using FixLife.ClientApp.Models;
using FixLife.ClientApp.ViewModels.FirstConfig;

namespace FixLife.ClientApp.Common
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FreeTimeViewModel, FreeTime>()
                    .ForMember(opt => opt.Text, map => map.MapFrom(d => d.HobbyText))
                    .ForMember(opt => opt.TimeStart, map => map.MapFrom(d => d.FreeTimeStart))
                    .ForMember(opt => opt.TimeEnd, map => map.MapFrom(d => d.FreeTimeEnd))
                    .ReverseMap();

                cfg.CreateMap<LearnTimeViewModel, LearnTime>()
                    .ForMember(opt => opt.TimeInterval, map => map.MapFrom(d => d.TimeInterval))
                    .ForMember(opt => opt.StartTime, map => map.MapFrom(d => d.SelectedStartTime))
                    .ForMember(opt => opt.DayOfWeeks, map => map.MapFrom(d => d.DayOfWeeks.Where(d => d.Selected).Select(d => d.Day)))
                    .ReverseMap();

                cfg.CreateMap<WeeklyWorkViewModel, WeeklyWork>()
                    .ForMember(opt => opt.TimeEnd, map => map.MapFrom(d => d.WeeklyWorkEnd))
                    .ForMember(opt => opt.TimeStart, map => map.MapFrom(d => d.WeeklyWorkStart))
                    .ForMember(opt => opt.DayOfWeeks, map => map.MapFrom(d => d.DayOfWeeks.Where(d => d.Selected).Select(d => d.Day)))
                    .ReverseMap();
            });
    }
}
