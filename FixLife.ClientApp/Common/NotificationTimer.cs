using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FixLife.ClientApp.Common
{
    public static class NotificationTimer
    {
        private static System.Timers.Timer _timer;

        static NotificationTimer()
        {
            _timer = new System.Timers.Timer(new TimeSpan(0, 0, 15, 0));
            _timer.Elapsed += OnTiming;
        }

        public static void EnableTiming() => _timer.Enabled = true;

        public static void DisableTiming() => _timer.Enabled = false;

        private static void OnTiming(object sender, ElapsedEventArgs e)
        {

        }
    }
}
