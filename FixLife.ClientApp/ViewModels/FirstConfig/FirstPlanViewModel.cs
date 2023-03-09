﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class FirstPlanViewModel : BaseViewModel
    {
        public string ActiveWorkImage { get; set; }
        public string[] WorkImages { get; set; }
        public ICommand NextCommand { get; private set; }
        public ICommand SetImageCommand { get; private set; }
        public ICommand CreateCommand { get; private set; }
        public FirstPlanViewModel()
        {
            ActiveWorkImage = "test.png";
            WorkImages = new string[] {"test.png", "another.png" };
            NextCommand = new Command(async () =>
            {
                await Next();
            });
            CreateCommand = new Command(async (cmd) =>
            {
                await Shell.Current.GoToAsync("CreatorPage");
            });
            SetImageCommand = new Command(async (cmd) =>
            {
                await SetImage();
            });
        }

        private async Task Next()
        {
            //TODO: Prepare set to next view
        }

        private async Task SetImage(string direction = null)
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
        }

    }
}
