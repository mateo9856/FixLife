using AutoMapper;
using FixLife.ClientApp.Common;
using FixLife.ClientApp.Models;
using FixLife.ClientApp.ViewModels.FirstConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Infrastructure.FirstPlan
{
    public class FirstPlanBuilder : IFirstPlanBuilder
    {
        private readonly Mapper _mapper;

        public AppPlan Plan { get; set; }

        public FirstPlanBuilder()
        {
            _mapper = new Mapper(AutoMapperConfig.mapConfig);
            Plan = new AppPlan();
            Plan.FreeTime = new List<FreeTime>();
        }

        public void ApplyPlan() =>
            FirstPlanSession.Instance().SummaryPlan = Plan;

        public void ClearPlan()
        {
            Plan = new AppPlan();
            Plan.FreeTime = new List<FreeTime>();
        }

        public void SetFreeTime(FreeTimeViewModel model)
        {
            var mapModel = _mapper.Map<FreeTime>(model);
            if(!Plan.FreeTime.Any(d => d.Text == mapModel.Text))
                Plan.FreeTime.Add(mapModel);
        }

        public void SetLearnTime(LearnTimeViewModel model)
        {
            var mapModel = _mapper.Map<LearnTime>(model);
            Plan.LearnTime = mapModel;
        }

        public void SetWeeklyWork(WeeklyWorkViewModel model)
        {
            var mapModel = _mapper.Map<WeeklyWork>(model);
            Plan.WeeklyWork = mapModel;
        }

        public void AssignTypeEdit(string type)
        {
            FirstPlanSession.Instance().IsEdit = type.Equals("edit") ? true : false;
        }
    }
}
