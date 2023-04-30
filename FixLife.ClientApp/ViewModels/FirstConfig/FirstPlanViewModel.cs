using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Infrastructure.FirstPlan;
using FixLife.ClientApp.Models.FirstPlan;
using FixLife.ClientApp.Views.FirstConfig.PlanViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FirstPlanViewModel : BaseViewModel
    {
        public Action<Button> ClickNext { get; set; }
        public Action<Button> ClickPrev { get; set; }
        private IFirstPlanBuilder _firstPlanBuilder;
        public WeeklyWorkImage ActiveWorkImage { get; set; }
        public FreeTime FreeTime { get; set; }
        public WeeklyWork WeeklyWork { get; set; }
        public LearnTime LearnTime { get; set; }
        public WeeklyWorkImage[] WorkImages { get; set; }
        public WeeklyWorkViewModel WeeklyWorkViewModel { get; set; }
        public FreeTimeViewModel FreeTimeViewModel { get; set; }
        public LearnTimeViewModel LearnTimeViewModel { get; set; }
        public ICommand SetImageCommand { get; private set; }
        public ICommand CreateCommand { get; private set; }
        public FirstPlanViewModel()
        {
            _firstPlanBuilder = new FirstPlanBuilder();
            _firstPlanBuilder.ClearPlan();
            WeeklyWorkViewModel = new WeeklyWorkViewModel();
            FreeTimeViewModel = new FreeTimeViewModel();
            LearnTimeViewModel = new LearnTimeViewModel();
            WorkImages = new WeeklyWorkImage[] {
                new WeeklyWorkImage {Source = "work.png", Description = "Daily Work", Name = "Work"},
                new WeeklyWorkImage {Source = "business.png", Description = "Working with your business", Name = "Company"},
            };
            ActiveWorkImage = WorkImages.First();
            CreateCommand = new Command(async (cmd) =>
            {
                await Shell.Current.GoToAsync("CreatorPage");
            });
        }

        public void SetImage(string direction, ImageButton element, Label label)
        {
            int directionCount = direction == null ? 1 : direction == "Left" ? -1 : 1;
            int currentImage = Array.IndexOf(WorkImages, ActiveWorkImage);
            try
            {
                ActiveWorkImage = WorkImages[currentImage + directionCount];
            
            } catch(IndexOutOfRangeException ex)
            {
                if(directionCount == 1)
                    ActiveWorkImage = WorkImages.First();
                else
                    ActiveWorkImage = WorkImages.Last();
            }
            element.Source = ActiveWorkImage.Source;
            label.Text = ActiveWorkImage.Description;
        }

        public void SummaryPlan()
        {
            _firstPlanBuilder.SetWeeklyWork(WeeklyWorkViewModel);
            _firstPlanBuilder.SetFreeTime(FreeTimeViewModel);
            _firstPlanBuilder.SetLearnTime(LearnTimeViewModel);
            _firstPlanBuilder.ApplyPlan();
        }

    }
}
