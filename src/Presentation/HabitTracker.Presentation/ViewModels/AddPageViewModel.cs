using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation;

public partial class AddPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    // Constructor
    public AddPageViewModel()
    {
        SelectHabitTypeCommand = new Command(async () => await SelectHabitTypeAsync());
        EnterGoalMUnitCommand = new Command(async () => await EnterGoalMUnitAsync());
        SelectRegularityCommand = new Command(async () => await SelectRegularityAsync());
        SelectIconColorCommand = new Command(async () => await SelectIconColorAsync());
        SelectTimeOfDayCommand = new Command(async () => await SelectTimeOfDayAsync());
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await CancelAsync());
    }

    // Fields
    private string habitName = "";
    public string HabitName
    {
        get => habitName;
        set
        {
            if (habitName != value)
            {
                habitName = value;
                OnPropertyChanged();
                UpdateNameBorderColor();
            }
        }
    }

    private string habitDescription = "";
    public string HabitDescription
    {
        get => habitDescription;
        set
        {
            if (habitDescription != value)
            {
                habitDescription = value;
                OnPropertyChanged();
            }
        }
    }

    private Color nameBorderColor = Color.FromArgb("#7B9EE0");
    public Color NameBorderColor
    {
        get => nameBorderColor;
        set
        {
            if (nameBorderColor != value)
            {
                nameBorderColor = value;
                OnPropertyChanged();
            }
        }
    }

    private Color habitTypeButtonColor = Color.FromArgb("#7B9EE0");
    public Color HabitTypeButtonColor
    {
        get => habitTypeButtonColor;
        set
        {
            if (habitTypeButtonColor != value)
            {
                habitTypeButtonColor = value;
                OnPropertyChanged();
            }
        }
    }

    private string habitTypeText = "Select a habit type";
    public string HabitTypeText
    {
        get => habitTypeText;
        set
        {
            if (habitTypeText != value)
            {
                habitTypeText = value;
                OnPropertyChanged();
            }
        }
    }

    private bool isHabitTypeSelected;
    public bool IsHabitTypeSelected
    {
        get => isHabitTypeSelected;
        set
        {
            if (isHabitTypeSelected != value)
            {
                isHabitTypeSelected = value;
                OnPropertyChanged();
            }
        }
    }

    private Color habitGoalMUnitButtonColor = Color.FromArgb("#7B9EE0");
    public Color HabitGoalMUnitButtonColor
    {
        get => habitGoalMUnitButtonColor;
        set
        {
            if (habitGoalMUnitButtonColor != value)
            {
                habitGoalMUnitButtonColor = value;
                OnPropertyChanged();
            }
        }
    }

    private string habitGoalMUnitText = "Select a habit type";
    public string HabitGoalMUnitText
    {
        get => habitGoalMUnitText;
        set
        {
            if (habitGoalMUnitText != value)
            {
                habitGoalMUnitText = value;
                OnPropertyChanged();
            }
        }
    }

    private bool isHabitGoalMUnitSelected;
    public bool IsHabitGoalMUnitSelected
    {
        get => isHabitGoalMUnitSelected;
        set
        {
            if (isHabitGoalMUnitSelected != value)
            {
                isHabitGoalMUnitSelected = value;
                OnPropertyChanged();
            }
        }
    }

    // Commands
    public ICommand SelectHabitTypeCommand { get; }
    public ICommand EnterGoalCommand { get; }
    public ICommand EnterGoalMUnitCommand { get; }
    public ICommand SelectRegularityCommand { get; }
    public ICommand SelectIconColorCommand { get; }
    public ICommand SelectTimeOfDayCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    // Command Methods
    private async Task SelectHabitTypeAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Choose your habit type", "Cancel", null, "Positive", "Negative");

        if (string.IsNullOrEmpty(action) || action == "Cancel")
            return;

        HabitTypeText = "Habit type: " + action;
        HabitTypeButtonColor = Color.FromArgb("#9ACD32");
        IsHabitGoalMUnitSelected = true;

        NameBorderColor = Color.FromArgb("#9ACD32"); // Also change name field to green
    }

    private async Task EnterGoalMUnitAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Choose your goal", "Cancel", null, "Km", "Sec", "Count", "Step");

        if (string.IsNullOrEmpty(action) || action == "Cancel")
            return;

        HabitGoalMUnitText = "Habit type: " + action;
        HabitGoalMUnitButtonColor = Color.FromArgb("#9ACD32");
        IsHabitTypeSelected = true;

        NameBorderColor = Color.FromArgb("#9ACD32"); // Also change name field to green
    }

    private async Task SelectRegularityAsync()
    {
        await Shell.Current.DisplayAlert("Regularity", "Open regularity selector.", "OK");
    }

    private async Task SelectIconColorAsync()
    {
        await Shell.Current.DisplayAlert("Icon & Color", "Open icon/color picker.", "OK");
    }

    private async Task SelectTimeOfDayAsync()
    {
        await Shell.Current.DisplayAlert("Time of day", "Open time picker.", "OK");
    }

    private async Task SaveAsync()
    {
        await Shell.Current.DisplayAlert("Saved", "Your habit has been saved.", "OK");
        await Shell.Current.GoToAsync("..");
    }

    private async Task CancelAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    // Helpers
    private void UpdateNameBorderColor()
    {
        NameBorderColor = string.IsNullOrWhiteSpace(HabitName)
            ? Color.FromArgb("#7B9EE0")
            : Color.FromArgb("#9ACD32");
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void UpdateGoalBorderColor()
    {
        NameBorderColor = string.IsNullOrWhiteSpace(HabitName)
            ? Color.FromArgb("#7B9EE0")
            : Color.FromArgb("#9ACD32");
    }
}
