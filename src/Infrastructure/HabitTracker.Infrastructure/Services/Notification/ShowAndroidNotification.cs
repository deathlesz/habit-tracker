using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;
using HabitTracker.Domain.Entities;

namespace HabitTracker.Infrastructure.Services.Notification;
/// <summary>
/// Handles creating and showing Android notifications for habit reminders.
/// </summary>
public class ShowAndroidNotification
{
    /// <summary>
    /// The unique ID of the notification channel for habit notifications.
    /// </summary>
    private const string CHANNEL_ID = "habit_channel";
    /// <summary>
    /// Initializes a new instance of the <see cref="ShowAndroidNotification"/> class 
    /// and ensures the notification channel is created.
    /// </summary>
    public ShowAndroidNotification()
    {
        AddNotificationChannel();
    }
    /// <summary>
    /// Creates the notification channel for Android 8.0 (API level 26) and above.
    /// A channel is required for displaying notifications on these versions.
    /// </summary>
    private void AddNotificationChannel()
    {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channel = new NotificationChannel(CHANNEL_ID, "Habit Notifications", NotificationImportance.High);
            var manager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);
        }
    }
    /// <summary>
    /// Displays an Android notification using the information from the provided habit reminder.
    /// </summary>
    /// <param name="habitReminder">
    /// The <see cref="HabitReminderEntity"/> containing the habit message and identifier.
    /// </param>
    public void OnShowAndroidNotification(HabitReminderEntity habitReminder)
    {
        var context = Android.App.Application.Context;

        var builder = new NotificationCompat.Builder(context, CHANNEL_ID).SetContentTitle("Habit Notifications")
            .SetContentText(habitReminder.Message).SetSmallIcon(Android.Resource.Drawable.IcDialogInfo)
            .SetPriority((int)NotificationPriority.High);
        var manager = NotificationManagerCompat.From(context);
        manager.Notify(habitReminder.Id, builder.Build());
    }
}