using FixLife.ClientApp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Models.FirstPlan
{
    public class DayOfWeekListItem
    {
        public bool Selected { get; set; }
        public DayOfTheWeekEnum Day { get; set; }
    }
}
