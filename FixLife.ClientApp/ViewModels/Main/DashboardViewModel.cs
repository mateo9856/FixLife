﻿using FixLife.ClientApp.Models.AppPlan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.Main
{
    public class DashboardViewModel : BaseViewModel
    {
        public ICommand EditPlanCommand { get; private set; }
        public AppPlan ActualPlan { get; set; }
        
        public string WorkStatus { 
            get 
            {
                if (ActualPlan == null) return string.Empty;
                return InWorkState() ? "In work" : "After Work";
            } 
        }

        public bool TimesLeftVisible { get => InWorkState(); }
        public bool TimeToWorkVisible { get => !InWorkState(); }

        public DashboardViewModel()
        {
            ActualPlan = new AppPlan();//TODO: Get Data from Api
            EditPlanCommand = new Command(async e => await EditPlan());
        }

        private bool InWorkState()
        {
            if(ActualPlan == null) return false;
            var nowDate = DateTime.Parse(DateTime.Now.ToString());
            var startDate = DateTime.Parse(ActualPlan.WeeklyWork.TimeStart.ToString());
            var endDate = DateTime.Parse(ActualPlan.WeeklyWork.TimeEnd.ToString());
            return nowDate > startDate && nowDate < endDate;        
        }

        private async Task EditPlan()
        {
            
        }

    }
}
