using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation.ViewModel;

public record ElementColorStyle(Color DefaultColor, Color SetColor, Color DefaultStrokeColor, Color SetStrokeColor)
{
    public static ElementColorStyle Default => new(
        Color.FromArgb("#7B9EE0"),
        Color.FromArgb("#9ACD32"),
        Color.FromArgb("#393A9A"), // TODO: maybe better to use #7A8BB3?
        Color.FromArgb("#78AA60")
    );
}

public partial class ColorChangingElement(ElementColorStyle style, string defaultValue = "") : ObservableObject
{
    public required ICommand? Command { get; init; }
    public ElementColorStyle Style { get; init; } = style;

    [ObservableProperty]
    private Color color = style.DefaultColor;
    [ObservableProperty]
    private Color strokeColor = style.DefaultStrokeColor;

    [ObservableProperty]
    private string value = defaultValue;

    partial void OnValueChanged(string value)
    {
        Color = string.IsNullOrWhiteSpace(value)
            ? Style.DefaultColor
            : Style.SetColor;

        StrokeColor = string.IsNullOrWhiteSpace(value)
                ? Style.DefaultStrokeColor
                : Style.SetStrokeColor;
    }
}

public partial class AddPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    public ColorChangingElement HabitTypeButton { get; init; }
    public ColorChangingElement HabitNameEntry { get; init; }
    public ColorChangingElement HabitGoalEntry { get; init; }
    public ColorChangingElement HabitGoalMUnitButton { get; init; }

    // Constructor
    public AddPageViewModel()
    {
        HabitTypeButton = new(ElementColorStyle.Default, "Enter your habit type")
        {
            Command = new Command(async () => await SelectHabitTypeAsync())
        };
        HabitNameEntry = new(ElementColorStyle.Default)
        {
            Command = null
        };
        HabitGoalEntry = new(ElementColorStyle.Default)
        {
            Command = null
        };
        HabitGoalMUnitButton = new(ElementColorStyle.Default, "Select your habit goal measurement unit")
        {
            Command = new Command(async () => await SelectGoalMUnitAsync())
        };
        

        SelectHabitTypeCommand = new Command(async () => await SelectHabitTypeAsync());
        EnterGoalMUnitCommand = new Command(async () => await SelectGoalMUnitAsync());
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

    private string habitGoal = "";
    public string HabitGoal
    {
        get => habitGoal;
        set
        {
            if (habitGoal != value)
            {
                habitGoal = value;
                OnPropertyChanged();
                UpdateGoalBorderColor();
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

    private Color goalBorderColor = Color.FromArgb("#7B9EE0");
    public Color GoalBorderColor
    {
        get => goalBorderColor;
        set
        {
            if (goalBorderColor != value)
            {
                goalBorderColor = value;
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

    private string habitGoalMUnitText = "Select your habit goal measurement unit";
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

        HabitTypeButton.Value = "Habit type: " + action;
    }

    private async Task SelectGoalMUnitAsync()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Choose your goal", "Cancel", null, "Km", "Sec", "Count", "Step");

        if (string.IsNullOrEmpty(action) || action == "Cancel")
            return;

        HabitGoalMUnitButton.Value = "Measurement unit: " + action;

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
        GoalBorderColor = string.IsNullOrWhiteSpace(HabitGoal)
            ? Color.FromArgb("#7B9EE0")
            : Color.FromArgb("#9ACD32");
    }
}
