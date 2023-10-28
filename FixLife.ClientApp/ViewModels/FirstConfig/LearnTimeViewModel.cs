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
    public class LearnTimeViewModel : BaseViewModel
    {
        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        private TimeSpan _selectedStartTime;

        public TimeSpan SelectedStartTime
        {
            get { return _selectedStartTime; }
            set { _selectedStartTime = value; OnPropertyChanged(); }
        }

        private TimeSpan _timeInterval;

        public TimeSpan TimeInterval
        {
            get { return _timeInterval; }
            set { _timeInterval = value; OnPropertyChanged(); }
        }

        public string TimeString { get => 
                string.Format("Start: {0} - Interval: {1}", 
                    SelectedStartTime,
                    TimeInterval.ToString()); }

        public ObservableCollection<DayOfWeekListItem> DayOfWeeks { get; set; }

        public LearnTimeViewModel()
        {
            InitDayOfWeeks();
            SelectedDate = DateTime.Now;
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
