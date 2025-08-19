using System.ComponentModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation;

public partial class AddPageViewModel : ObservableObject
{
    // Fields
    [ObservableProperty]
    private string habitName = "";
    [ObservableProperty]
    private string habitDescription = "";
    
    // Button backgrounds and text field states
    [ObservableProperty]
    private Color nameBorderColor = Color.FromArgb("#7B9EE0"); // Default border color
    [ObservableProperty]
    private Color habitTypeButtonColor = Color.FromArgb("#7B9EE0"); // Default button color
    
    [ObservableProperty]
    private string habitTypeText = "Select a habit type";
    [ObservableProperty]
    private bool isHabitTypeSelected;

    // Commands wired from XAML
    // [RelayCommand]
    private async Task SelectHabitTypeAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Choose your habit type", "Cancel", null, "Positive", "Negative");

        if (string.IsNullOrEmpty(action) || action == "Cancel")
            return;

        HabitTypeText = "Habit type: " + action;
        HabitTypeButtonColor = Color.FromArgb("#9ACD32"); // Change button background to green
        IsHabitTypeSelected = true;


        // Example: change the name field border background to indicate a filled state
        NameBorderColor = Color.FromArgb("#9ACD32"); // Green when selected
    }

    // [RelayCommand]
    private async Task EnterGoalAsync()
    {
        // Open a popup/page to enter goal
        await Shell.Current.DisplayAlert("Goal", "Open goal editor here.", "OK");
    }

    // [RelayCommand]
    private async Task SelectRegularityAsync()
    {
        await Shell.Current.DisplayAlert("Regularity", "Open regularity selector.", "OK");
    }

    // [RelayCommand]
    private async Task SelectIconColorAsync()
    {
        await Shell.Current.DisplayAlert("Icon & Color", "Open icon/color picker.", "OK");
    }

    // [RelayCommand]
    private async Task SelectTimeOfDayAsync()
    {
        await Shell.Current.DisplayAlert("Time of day", "Open time picker.", "OK");
    }

    // [RelayCommand]
    private async Task SaveAsync()
    {
        // TODO: validate and persist
        await Shell.Current.DisplayAlert("Saved", "Your habit has been saved.", "OK");
        await Shell.Current.GoToAsync("..");
    }

    // [RelayCommand]
    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    // Automatically notifies UI and runs OnHabitNameChanged when HabitName is set


    partial void OnHabitNameChanged(string value)
    {
        NameBorderColor = string.IsNullOrWhiteSpace(value)
            ? Color.FromArgb("#7B9EE0") // default blue
            : Color.FromArgb("#9ACD32"); // green when filled
    }
}
