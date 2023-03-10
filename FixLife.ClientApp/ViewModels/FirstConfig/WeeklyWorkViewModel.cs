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
            InitDayOfWeeks();
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
