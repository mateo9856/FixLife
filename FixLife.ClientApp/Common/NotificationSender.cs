using Microsoft.Toolkit.Uwp.Notifications;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixLife.ClientApp.Common
{
    public class NotificationSender
    {
        public void SendWindowsToast(string header, string message)
        {
            #if WINDOWS
            var builder = new ToastContentBuilder();
            builder.AddText(header);
            builder.AddText(message);
            builder.Show();
            #endif
        }
        public void SendMobileToast(string header, string message) {
            var request = new NotificationRequest();
            request.NotificationId = 999;
            request.Title= header;
            request.Subtitle = "Life notification";
            request.Description = message;
            LocalNotificationCenter.Current.Show(request);
        }
    }
}
