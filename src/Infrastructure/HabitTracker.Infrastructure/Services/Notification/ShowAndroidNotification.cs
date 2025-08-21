using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Infrastructure.Services.Notification;

public class ShowAndroidNotification
{
    private const string CHANNEL_ID = "habit_channel";
    
    public ShowAndroidNotification()
    {
        AddNotificationChannel();
    }

    private void AddNotificationChannel()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(CHANNEL_ID, "Habit Notifications", NotificationImportance.Default);
            var manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);
        }
    }
    public void OnShowAndroidNotification(HabitReminderEntity habitReminder)
    {
        var context = Android.App.Application.Context;

        var builder = new NotificationCompat.Builder(context, CHANNEL_ID).SetContentTitle("Habit Notifications")
            .SetContentText(habitReminder.Message).SetSmallIcon(Android.Resource.Drawable.IcDialogInfo)
            .SetPriority((int)NotificationPriority.Default);
        var manager = NotificationManagerCompat.From(context);
        manager.Notify(habitReminder.Id, builder.Build());
    }
}