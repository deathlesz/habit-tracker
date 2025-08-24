using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Enums;
using JFomit.Functional;
using System.Diagnostics;
using JFomit.Functional.Extensions;
using static JFomit.Functional.Prelude;
using JFomit.Functional.Monads;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HabitTracker.Presentation.ViewModel;

public partial class EditPageViewModel : ObservableObject
{
    public ColorChangingElement HabitTypeButton { get; init; }
    public ColorChangingElement HabitNameEntry { get; init; }
    public ColorChangingElement HabitGoalEntry { get; init; }
    public ColorChangingElement HabitGoalMUnitButton { get; init; }
    public ColorChangingElement<Regularity> HabitRegularityButton { get; init; }
    public ColorChangingElement HabitIconButton { get; init; }
    public ColorChangingElement HabitColorButton { get; init; }
    public ColorChangingElement HabitTimeOfDayButton { get; init; }
    public ColorChangingElement HabitReminderButton { get; init; }
    public ColorChangingElement HabitStartDatePicker { get; init; }
    public ColorChangingElement HabitEndDatePicker { get; init; }
    public ColorChangingElement HabitDescriptionEditor { get; init; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    private Habit originalHabit;
    // private IPresentation presentation;

    public EditPageViewModel(Habit habit)
    {
        originalHabit = habit;
        // this.presentation = presentation;

        HabitTypeButton = new(ElementColorStyle.Default, $"Habit type: {habit.Kind}", Some(habit.Kind.ToString()))
        {
            Command = new Command(async () => await SelectHabitTypeAsync()),
        };
        HabitNameEntry = new(ElementColorStyle.Default, habit.Name, Some(habit.Name));
        HabitGoalEntry = new(ElementColorStyle.Default, habit.Goal.Name, Some(habit.Goal.Name));
        HabitGoalMUnitButton = new(ElementColorStyle.Default, $"Measurement unit: {habit.Goal.Unit}", Some(habit.Goal.Unit.ToString()))
        {
            Command = new Command(async () => await SelectGoalMUnitAsync())
        };
        HabitRegularityButton = new(ElementColorStyle.Default, "Regularity", Some(habit.Regularity))
        {
            Command = new Command(async () => await SelectRegularityAsync(habit.Regularity))
        };
        HabitIconButton = new(ElementColorStyle.Default, $"Icon: {habit.Icon}", Some(habit.Icon.ToString()))
        {
            Command = new Command(async () => await SelectIconAsync())
        };
        // from here
        HabitColorButton = new(ElementColorStyle.Default, $"Color: {habit.Color}", Some(habit.Color.ToString()))
        {
            Command = new Command(async () => await SelectColorAsync())
        };
        HabitTimeOfDayButton = new(ElementColorStyle.Default, $"Time of day: {habit.PartOfTheDay}", habit.PartOfTheDay.ToOption().Select(partOfTheDay => partOfTheDay.ToString()))
        {
            Command = new Command(async () => await SelectTimeOfDayAsync())
        };
        HabitReminderButton = new(ElementColorStyle.Default, habit.Reminder.ToOption().Match(_ => "Edit reminder", () => "Add reminder"))
        {
            Command = new Command(async () => await SelectReminderAsync())
        };

        var startDate = habit.StartDate.ToOption().Select(date => date.ToString());
        HabitStartDatePicker = new(ElementColorStyle.Default, $"{startDate.Match(date => date, () => "None")}", startDate);

        var endDate = habit.EndDate.ToOption().Select(date => date.ToString());
        HabitEndDatePicker = new(ElementColorStyle.Default, $"{endDate.Match(date => date, () => "None")}", endDate);

        HabitDescriptionEditor = new(ElementColorStyle.Default, $"{habit.Description ?? ""}", Some(habit.Description ?? ""));
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

    private async Task SelectRegularityAsync(Regularity regularity) => await Shell.Current.GoToAsync(nameof(RegularityPage), new ShellNavigationQueryParameters
    {
        { "OldRegularity", regularity }
    });

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
        var habit = new Habit(originalHabit.Id)
        {
            Color = Enum.Parse<Domain.Color>(HabitColorButton.StoredValue.Unwrap()),
            Goal = new Goal(HabitGoalEntry.StoredValue.Unwrap(), HabitGoalMUnitButton.StoredValue.Unwrap() switch
            {
                "Km" => MeasurementUnit.Km,
                "Sec" => MeasurementUnit.Sec,
                "Count" => MeasurementUnit.Count,
                "Step" => MeasurementUnit.Steps,
                "M" => MeasurementUnit.M,
                "Min" => MeasurementUnit.Min,
                "Hour" => MeasurementUnit.Hr,
                "Ml" => MeasurementUnit.Ml,
                "Cal" => MeasurementUnit.Cal,
                "G" => MeasurementUnit.G,
                "Mg" => MeasurementUnit.Mg,
                "Drink" => MeasurementUnit.Drink,
                _ => throw new UnreachableException(),
            }),
            Icon = HabitIconButton.StoredValue.Unwrap() switch
            {
                "Bottle" => Icon.DrinkingWater,
                "GYM" => Icon.Training,
                "Run" => Icon.Running,
                _ => Icon.Default,
            },
            Kind = Enum.Parse<GoodnessKind>(HabitTypeButton.StoredValue.Unwrap()),
            Name = HabitNameEntry.StoredValue.Unwrap(),
            Regularity = HabitRegularityButton.StoredValue.Unwrap(),
            Description = HabitDescriptionEditor.StoredValue.Unwrap(),
            StartDate = HabitStartDatePicker.StoredValue.Select(date => DateOnly.Parse(date)).ToNullable(),
            EndDate = HabitEndDatePicker.StoredValue.Select(date => DateOnly.Parse(date)).ToNullable(),
            PartOfTheDay = Enum.Parse<PartOfTheDay>(HabitTimeOfDayButton.StoredValue.Unwrap()),
            State = originalHabit.State,
            Reminder = originalHabit.Reminder // TODO: change when reminder page is merged
        };

        // TODO: uncomment when repositories are merged in
        // if (presentation.UpdateHabit(habit).IsSuccess)
        // {
        //     await Shell.Current.DisplayAlert("Saved", "Your habit has been saved.", "OK");
        // }
        // else
        // {
        //     await Shell.Current.DisplayAlert("Error", "Encountered an error during saving.", "OK");
        // }

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
}
