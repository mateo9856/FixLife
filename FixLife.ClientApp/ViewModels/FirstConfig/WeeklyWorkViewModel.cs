using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Models.FirstPlan;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.ViewModels.FirstConfig
{
    public class WeeklyWorkViewModel : BaseViewModel
    {
        private TimeSpan _weeklyWorkStart;
        private TimeSpan _weeklyWorkEnd;
        public TimeSpan WeeklyWorkStart { 
            get 
            {
                return _weeklyWorkStart;
            }
            set
            {
                _weeklyWorkStart = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan WeeklyWorkEnd { 
            get
            {
                return _weeklyWorkEnd;
            }
            set 
            {
                _weeklyWorkEnd = value;
                OnPropertyChanged();
            } 
        }
        public ObservableCollection<DayOfWeekListItem> DayOfWeeks { get; set; }

        public WeeklyWorkViewModel()
        {
            WorkImages = new WeeklyWorkImage[] {
                new WeeklyWorkImage {Source = "work.png", Description = "Daily Work", Name = "Work"},
                new WeeklyWorkImage {Source = "business.png", Description = "Working with your business", Name = "Company"},
            };
            ActiveWorkImage = WorkImages.First();
            InitDayOfWeeks();
        }
        public WeeklyWorkImage ActiveWorkImage { get; set; }
        public WeeklyWorkImage[] WorkImages { get; set; }

        public void ApplyWorkTime(string type)
        {

        }

        public void SetImage(string direction, ImageButton element, Label label)
        {
            int directionCount = direction == null ? 1 : direction == "Left" ? -1 : 1;
            int currentImage = Array.IndexOf(WorkImages, ActiveWorkImage);
            try
            {
                ActiveWorkImage = WorkImages[currentImage + directionCount];

            }
            catch (IndexOutOfRangeException ex)
            {
                if (directionCount == 1)
                    ActiveWorkImage = WorkImages.First();
                else
                    ActiveWorkImage = WorkImages.Last();
            }
            element.Source = ActiveWorkImage.Source;
            label.Text = ActiveWorkImage.Description;
        }

        private void InitDayOfWeeks()
        {
            DayOfWeeks = new ObservableCollection<DayOfWeekListItem>()
            {
                new DayOfWeekListItem { Day = DayOfTheWeekEnum.Monday, Selected = false },
                new DayOfWeekListItem { Day = DayOfTheWeekEnum.Thursday, Selected = false },
                new DayOfWeekListItem { Day = DayOfTheWeekEnum.Wednesday, Selected = false },
                new DayOfWeekListItem { Day = DayOfTheWeekEnum.Thursday, Selected = false },
                new DayOfWeekListItem { Day = DayOfTheWeekEnum.Friday, Selected = false },
                new DayOfWeekListItem { Day = DayOfTheWeekEnum.Saturday, Selected = false },
                new DayOfWeekListItem { Day = DayOfTheWeekEnum.Sunday, Selected = false }
            };
        }
    }
}
