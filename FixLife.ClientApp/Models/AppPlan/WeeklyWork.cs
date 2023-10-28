using FixLife.ClientApp.Common.Enums;
using FixLife.ClientApp.Models.FirstPlan;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models
{
    public class WeeklyWork
    {
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public List<DayOfTheWeekEnum> DayOfWeeks { get; set; }
        [JsonIgnore]
        public List<DayOfWeekListItem> DayOfWeeksList { get => MapToList(); }

        private List<DayOfWeekListItem> MapToList()
        {
            var list = new List<DayOfWeekListItem>();
            for (int i = 0; i < 7; i++)
            {
                var day = (DayOfTheWeekEnum)i;
                var item = new DayOfWeekListItem
                {
                    Day = day,
                };

                item.Selected = DayOfWeeks.Contains(day) ? true : false;
            }

            return list;
        }

    }
}
