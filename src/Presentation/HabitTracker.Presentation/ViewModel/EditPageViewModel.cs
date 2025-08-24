using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;
using HabitTracker.Domain.Dto;
using JFomit.Functional;

namespace HabitTracker.Presentation.ViewModel;
public partial class EditPageViewModel
{
    public ColorChangingElement HabitTypeButton { get; init; }
    public ColorChangingElement HabitNameEntry { get; init; }
    public ColorChangingElement HabitGoalEntry { get; init; }
    public ColorChangingElement HabitGoalMUnitButton { get; init; }
    public ColorChangingElement HabitRegularityButton { get; init; }
    public ColorChangingElement HabitIconButton { get; init; }
    public ColorChangingElement HabitColorButton { get; init; }
    public ColorChangingElement HabitTimeOfDayButton { get; init; }
    public ColorChangingElement HabitReminderButton { get; init; }
    public ColorChangingElement HabitStartDatePicker { get; init; }
    public ColorChangingElement HabitEndDatePicker { get; init; }
    public ColorChangingElement HabitDescriptionEditor { get; init; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public EditPageViewModel(Habit habit)
    {
        HabitTypeButton = new(ElementColorStyle.Default, $"Habit type: {habit.Kind}", Prelude.Some(habit.Kind.ToString()))
        {
            Command = new Command(async () => await SelectHabitTypeAsync()),
        };
        HabitNameEntry = new(ElementColorStyle.Default, habit.Name, Prelude.Some(habit.Name));
        HabitGoalEntry = new(ElementColorStyle.Default, habit.Goal.Name, Prelude.Some(habit.Goal.Name));
        HabitGoalMUnitButton = new(ElementColorStyle.Default, $"Measurement unit: {habit.Goal.Unit}", Prelude.Some(habit.Goal.Unit.ToString()))
        {
            Command = new Command(async () => await SelectGoalMUnitAsync())
        };
        HabitRegularityButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectRegularityAsync())
        };
        HabitIconButton = new(ElementColorStyle.Default, $"Icon: {habit.Icon}", Prelude.Some(habit.Icon.ToString()))
        {
            Command = new Command(async () => await SelectIconAsync())
        };
        HabitColorButton = new(ElementColorStyle.Default, "Choose the color from the list:")
        {
            Command = new Command(async () => await SelectColorAsync())
        };
        HabitTimeOfDayButton = new(ElementColorStyle.Default, "Select time of day")
        {
            Command = new Command(async () => await SelectTimeOfDayAsync())
        };
        HabitReminderButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectReminderAsync())
        };
        HabitStartDatePicker = new(ElementColorStyle.Default);
        HabitEndDatePicker = new(ElementColorStyle.Default);
        HabitDescriptionEditor = new(ElementColorStyle.Default);
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await CancelAsync());
    }

    // methods that display action sheets and alerts
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
            "Choose your goal measurement unit", "Cancel", null, "Km", "Sec", "Count", "Step", "M", "Min", "Hour", "Ml", "Cal", "G", "Mg", "Drink");

        if (string.IsNullOrEmpty(action) || action == "Cancel")
        {
            return;
        }

        HabitGoalMUnitButton.SetValue("Measurement unit:", action);
    }

    private async Task SelectRegularityAsync() => await Shell.Current.GoToAsync($"{nameof(RegularityPage)}");
    async Task SelectIconAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
                "Choose your icon",
                "Cancel",
                null,
                "Bottle",
                "GYM",
                "Run"
            );

        if (string.IsNullOrEmpty(action) || action == "Cancel")
        {
            return;
        }

        HabitIconButton.SetValue("Icon selected:", action);
    }
    async Task SelectColorAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
                "Choose your habit color",
                "Cancel",
                null,
                "Black",
                "Red",
                "Green",
                "Yellow",
                "Blue",
                "Magenta",
                "Cyan",
                "White"
            );

        if (string.IsNullOrEmpty(action) || action == "Cancel")
        {
            return;
        }

        HabitColorButton.SetValue("Habit color:", action);
    }
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

        HabitTimeOfDayButton.SetValue("Chosen time:", action);
    }

    private async Task SelectReminderAsync() => await Shell.Current.GoToAsync($"{nameof(ReminderPage)}");

    private async Task SaveAsync()
    {
        await Shell.Current.DisplayAlert("Saved", "Your habit has been saved.", "OK"); // TODO: implement habit saving
        await Shell.Current.GoToAsync("..");
    }
    private async Task CancelAsync() => await Shell.Current.GoToAsync(".."); // return to home page


    private string? _selectedIcon;

    public ObservableCollection<string> Icons { get; } = new()
    {
        "run_icon.png",
        "gym_icon.png",
        "bottle_icon.png"
    };

    public string? SelectedIcon
    {
        get => _selectedIcon;
        set
        {
            if (_selectedIcon != value)
            {
                _selectedIcon = value;
                OnPropertyChanged();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
