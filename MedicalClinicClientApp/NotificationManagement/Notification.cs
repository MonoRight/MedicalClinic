using Notifications.Wpf;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalClinicClientApp.NotificationManagement
{
    public static class Notification
    {
        public static readonly NotificationManager notificationManager = new NotificationManager();

        public static void Show(string title, string message, NotificationType type, int hours, int minutes, int seconds)
        {
            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type = type
            },
                expirationTime: new TimeSpan(hours, minutes, seconds));
        }
    }
}
