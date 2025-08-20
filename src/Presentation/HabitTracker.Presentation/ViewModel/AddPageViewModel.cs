using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation.ViewModel;

public partial class AddPageViewModel
{
    public ColorChangingElement HabitTypeButton { get; init; }
    public ColorChangingElement HabitNameEntry { get; init; }
    public ColorChangingElement HabitGoalEntry { get; init; }
    public ColorChangingElement HabitGoalMUnitButton { get; init; }
    public ColorChangingElement HabitRegularityButton { get; init; }
    public ColorChangingElement HabitIconColorButton { get; init; }
    public ColorChangingElement HabitTimeOfDayButton { get; init; }
    public ColorChangingElement HabitReminderButton { get; init; }
    public ColorChangingElement HabitStartDateButton { get; init; }
    public ColorChangingElement HabitEndDateButton { get; init; }
    public ColorChangingElement HabitDescriptionEditor { get; init; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AddPageViewModel()
    {
        HabitTypeButton = new(ElementColorStyle.Default, "Enter your habit type")
        {
            Command = new Command(async () => await SelectHabitTypeAsync())
        };
        HabitNameEntry = new(ElementColorStyle.Default);
        HabitGoalEntry = new(ElementColorStyle.Default);
        HabitGoalMUnitButton = new(ElementColorStyle.Default, "Select your habit goal measurement unit")
        {
            Command = new Command(async () => await SelectGoalMUnitAsync())
        };
        HabitRegularityButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectRegularityAsync())
        };
        HabitIconColorButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectIconColorAsync())
        };
        HabitTimeOfDayButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectTimeOfDayAsync())
        };
        HabitReminderButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectReminderAsync())
        };
        HabitStartDateButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectStartDateAsync())
        };
        HabitEndDateButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectEndDateAsync())
        };
        HabitDescriptionEditor = new(ElementColorStyle.Default);

        // SelectIconColorCommand = new Command(async () => await SelectIconColorAsync());
        // SelectTimeOfDayCommand = new Command(async () => await SelectTimeOfDayAsync());
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await CancelAsync());
    }

    async Task SelectHabitTypeAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
                "Choose your habit type",
                "Cancel",
                null,
                "Positive",
                "Negative"
            );

        if (string.IsNullOrEmpty(action) || action == "Cancel")
        {
            return;
        }

        HabitTypeButton.SetValue("Habit type:", action);
    }

    private async Task SelectGoalMUnitAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Choose your goal", "Cancel", null, "Km", "Sec", "Count", "Step");

        if (string.IsNullOrEmpty(action) || action == "Cancel")
        {
            return;
        }

        HabitGoalMUnitButton.SetValue("Measurement unit:", action);
    }

    private async Task SelectRegularityAsync() => await Shell.Current.DisplayAlert("Regularity", "Open regularity selector.", "OK");
    private async Task SelectIconColorAsync() => await Shell.Current.DisplayAlert("Icon & Color", "Open icon/color picker.", "OK");
    private async Task SelectTimeOfDayAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Choose preferred time of day",
            "Cancel",
            null,
            "Morning",
            "Afternoon",
            "Night");

        if (string.IsNullOrEmpty(action) || action == "Cancel")
        {
            return;
        }

        HabitTimeOfDayButton.SetValue("Measurement unit:", action);
    }

    private async Task SelectReminderAsync() => await Shell.Current.DisplayAlert("Reminder", "Open reminder window.", "Ok");
    private async Task SelectStartDateAsync() => await Shell.Current.DisplayAlert("Start date", "Open time picker.", "OK");
    private async Task SelectEndDateAsync() => await Shell.Current.DisplayAlert("End date", "Open time picker.", "OK");

    private async Task SaveAsync()
    {
        await Shell.Current.DisplayAlert("Saved", "Your habit has been saved.", "OK");
        await Shell.Current.GoToAsync("..");
    }
    private async Task CancelAsync() => await Shell.Current.GoToAsync("..");
}
