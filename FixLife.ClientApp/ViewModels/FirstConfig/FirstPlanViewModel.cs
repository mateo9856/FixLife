﻿using FixLife.ClientApp.Common.Enums;
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
        public string ActiveWorkImage { get; set; }
        public FreeTime FreeTime { get; set; }
        public WeeklyWork WeeklyWork { get; set; }
        public LearnTime LearnTime { get; set; }
        public string[] WorkImages { get; set; }
        public WeeklyWorkViewModel WeeklyWorkViewModel { get; set; }
        public ICommand SetImageCommand { get; private set; }
        public ICommand CreateCommand { get; private set; }
        public FirstPlanViewModel()
        {
            WeeklyWorkViewModel = new WeeklyWorkViewModel();
            ActiveWorkImage = "test.png";
            WorkImages = new string[] {"test.png", "another.png" };
            CreateCommand = new Command(async (cmd) =>
            {
                await Shell.Current.GoToAsync("CreatorPage");
            });
        }

        public void SetImage(string direction, Image element)
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
            element.Source = ActiveWorkImage;
        }

    }
}
