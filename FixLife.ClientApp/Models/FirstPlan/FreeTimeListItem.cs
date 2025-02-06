using Microsoft.Maui.Platform;
using System.Globalization;

namespace FixLife.ClientApp.Models.FirstPlan
{
    public class FreeTimeListItem
    {
        public string Name { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string TimeString { get => $"{Name.ToUpper()} : {TimeStart.ToFormattedString(@"hh.mm", CultureInfo.CurrentCulture)} - {TimeEnd.ToFormattedString(@"hh.mm", CultureInfo.CurrentCulture)}"; }
    }
}
