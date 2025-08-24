using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation.ViewModel;

// ViewModel for the Edit page. It does not inherit from ObservableObject;
// instead, it raises PropertyChanged manually only for properties that need it.
public partial class EditPageViewModel
{
    // UI-facing elements that can change their visual style and display a value/label.
    // Presumably ColorChangingElement is your own control model that exposes properties
    // like Text, Value, and a Command to trigger interactions.
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

    // Page-level commands for Save/Cancel footer buttons.
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public EditPageViewModel()
    {
        // Initialize each “element” with a default visual style and optional caption.
        // For interactive items, wire up a Command that opens a picker or navigates.
        HabitTypeButton = new(ElementColorStyle.Default, "Enter your habit type")
        {
            Command = new Command(async () => await SelectHabitTypeAsync())
        };
        HabitNameEntry = new(ElementColorStyle.Default);
        HabitGoalEntry = new(ElementColorStyle.Default);

        HabitGoalMUnitButton = new(ElementColorStyle.Default, "Select measurement unit")
        {
            Command = new Command(async () => await SelectGoalMUnitAsync())
        };

        HabitRegularityButton = new(ElementColorStyle.Default)
        {
            Command = new Command(async () => await SelectRegularityAsync())
        };

        HabitIconButton = new(ElementColorStyle.Default, "Choose the icon from the list:")
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

        // Footer actions
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await CancelAsync());
    }

    // Opens an action sheet to pick habit type (Positive/Negative).
    // Updates the button’s displayed value when a valid choice is made.
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
            return; // no selection
        }

        HabitTypeButton.SetValue("Habit type:", action);
    }

    // Opens an action sheet for measurement unit selection and updates the UI element.
    private async Task SelectGoalMUnitAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Choose your goal measurement unit", "Cancel", null,
            "Km", "Sec", "Count", "Step", "M", "Min", "Hour", "Ml", "Cal", "G", "Mg", "Drink");

        if (string.IsNullOrEmpty(action) || action == "Cancel")
        {
            return;
        }

        HabitGoalMUnitButton.SetValue("Measurement unit:", action);
    }

    // Navigates to the Regularity page (modal or pushed via Shell route).
    private async Task SelectRegularityAsync() => await Shell.Current.GoToAsync($"{nameof(RegularityPage)}");

    // Opens an action sheet to choose an icon and updates the UI element.
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

    // Opens an action sheet to choose a color and updates the UI element.
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

    // Opens an action sheet to choose time-of-day preference and updates the UI element.
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

    // Navigates to a Reminder configuration page.
    private async Task SelectReminderAsync() => await Shell.Current.GoToAsync($"{nameof(ReminderPage)}");

    // Saves the habit (placeholder) and returns to the previous page.
    private async Task SaveAsync()
    {
        await Shell.Current.DisplayAlert("Saved", "Your habit has been saved.", "OK"); // TODO: implement habit saving
        await Shell.Current.GoToAsync(".."); // pop current page
    }

    // Cancels edit and returns to previous page.
    private async Task CancelAsync() => await Shell.Current.GoToAsync(".."); // return to home page

    // Backing field for the currently selected icon (from a list below).
    private string? _selectedIcon;

    // Example icon catalog (filenames in your Resources).
    public ObservableCollection<string> Icons { get; } = new()
    {
        "run_icon.png",
        "gym_icon.png",
        "bottle_icon.png"
    };

    // Selected icon property with manual change notification.
    // Only properties that the UI needs to react to must raise PropertyChanged.
    public string? SelectedIcon
    {
        get => _selectedIcon;
        set
        {
            if (_selectedIcon != value)
            {
                _selectedIcon = value;
                OnPropertyChanged(); // notifies bindings that SelectedIcon changed
            }
        }
    }

    // Minimal INotifyPropertyChanged implementation for properties that need UI updates.
    public event PropertyChangedEventHandler? PropertyChanged;

    // CallerMemberName lets you call OnPropertyChanged() without specifying the property name.
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}