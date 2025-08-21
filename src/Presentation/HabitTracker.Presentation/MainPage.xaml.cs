using Android.Content;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Services.Notification;
using JFomit.Functional.Monads;
namespace HabitTracker.Presentation
{
    public partial class MainPage : ContentPage
    {
        private AndroidNotificationService _androidNotificationService;
        private Result<HabitReminderEntity, string> result;
        public MainPage(AndroidNotificationService androidNotificationService)
        {
            InitializeComponent();
            _androidNotificationService = androidNotificationService;
        }

        private void OnCreateNotificationServiceClicked(object sender, EventArgs e)
        {
            string message = MessageEntry.Text;
            DateOnly startDate = DateOnly.FromDateTime(DatePicker.Date);
            if (!int.TryParse(CyclePatternLength.Text, out int cyclePatternLength))
            {
                ResultLabel.TextColor = Colors.Red;
                ResultLabel.Text = "Invalid cycle pattern length";
                return;
            }

            var days = DaysToNotify.Text;
            ICollection<int> daysToNotifycate;
            try
            {
                daysToNotifycate = days.Split(',').Select(d => int.Parse(d.Trim())).ToList();
            }
            catch
            {
                ResultLabel.TextColor = Colors.Red;
                ResultLabel.Text = "Invalid days format! Use: 1,3,5";
                return;
            }

            int? cyclesToRun = null;
            if (!string.IsNullOrWhiteSpace(CyclesToRun.Text))
            {
                if (int.TryParse(CyclesToRun.Text, out int parsedCyclesToRun))
                {
                    cyclesToRun = parsedCyclesToRun;
                }
                else
                {
                    ResultLabel.Text = "Invalid cycle pattern length";
                    ResultLabel.TextColor = Colors.Red;
                    return;
                }
            }

            result = _androidNotificationService.SetRepetitiveNotification(
                message,
                startDate,
                cyclePatternLength,
                daysToNotifycate,
                cyclesToRun
            );
            if (result.IsSuccess)
            {
                ResultLabel.Text = "Success";
                ResultLabel.TextColor = Colors.Green;
            }
            else
            {
                ResultLabel.Text = result.Error;
                ResultLabel.TextColor = Colors.Red;
            }
        }

        private void OnShowNotificationButtonClicked(object sender, EventArgs e)
        {

            if (result.TryUnwrap2(out var habitReminder, out var error))
            {
                var notification = new ShowAndroidNotification();
                notification.OnShowAndroidNotification(habitReminder);
                ResultShowNotification.Text = $"Success: {habitReminder}";
                ResultShowNotification.TextColor = Colors.Green;
            }
            else
            {
                ResultShowNotification.Text = error;
                ResultShowNotification.TextColor = Colors.Red;
            }
        }
    }

}
