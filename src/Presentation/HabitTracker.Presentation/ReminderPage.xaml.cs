using HabitTracker.Application.Interfaces.Services;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Services.Notification;
using HabitTracker.Presentation.ViewModel;
using JFomit.Functional.Monads;

namespace HabitTracker.Presentation;

public partial class ReminderPage : ContentPage
{
    private int _cyclePatternLength;

    private ICollection<int> _daysToNotify;

    private int? _cyclesToRun = null;
    public ReminderPage()
    {
        InitializeComponent();
    }
    
    private async void OnCreateNotificationClicked(object sender, EventArgs e)
    {
        var result = CheckingAndRecordingData();
    
        if (result.TryUnwrap2(out var reminderResult, out var error))
        {
            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                ["ReminderResult"] = reminderResult
            });
        }
        else
        {
            ResultLabel.TextColor = Colors.Red;
            ResultLabel.Text = error;
        }
    }

    private Result<ReminderResult, string> CheckingAndRecordingData()
    {
        if (!int.TryParse(CyclePatternLength.Text, out _cyclePatternLength))
        {
            return Result<ReminderResult, string>.Fail("Invalid cycle pattern length");
        }

        var days = DaysToNotify.Text;
        try
        {
            _daysToNotify = days.Split(',').Select(d => int.Parse(d.Trim())).ToList();
        }
        catch
        {
            return Result<ReminderResult, string>.Fail("Invalid days format! Use: 1,3,5");
        }
        
        if (!string.IsNullOrWhiteSpace(CyclesToRun.Text))
        {
            if (int.TryParse(CyclesToRun.Text, out int parsedCyclesToRun))
            {
                _cyclesToRun = parsedCyclesToRun;
            }
            else
            {
                return Result<ReminderResult, string>.Fail("Invalid cycles to run value");
            }
        }
        
        var reminderResult = new ReminderResult()
        {
            Message = MessageEntry.Text,
            CyclePatternLength = _cyclePatternLength,
            CyclesToRun = _cyclesToRun,
            DaysToNotify = _daysToNotify,
            StartDate = DateOnly.FromDateTime(DatePicker.Date),
        };

        return Result<ReminderResult, string>.Ok(reminderResult);
    }

    /// <summary>
    /// Handles the click event of the "Show Notification" button.
    /// If a valid habit reminder exists, displays it as an Android notification.
    /// </summary>
    // private void OnShowNotificationButtonClicked(object sender, EventArgs e)
    // {
    //
    //     if (result.TryUnwrap2(out var habitReminder, out var error))
    //     {
    //         var notification = new ShowAndroidNotification();
    //         notification.OnShowAndroidNotification(habitReminder);
    //         ResultShowNotification.Text = $"Success: {habitReminder}";
    //         ResultShowNotification.TextColor = Colors.Green;
    //     }
    //     else
    //     {
    //         ResultShowNotification.Text = error;
    //         ResultShowNotification.TextColor = Colors.Red;
    //     }
    // }
}