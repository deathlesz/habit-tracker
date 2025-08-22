using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GoogleGson.Annotations;
using Microsoft.Maui.Controls;

namespace HabitTracker.Presentation.ViewModel;

public class RegularityPageViewModel : INotifyPropertyChanged
{
    private bool _isDaily;
    private bool _isMonthly;
    private bool _isInterval;

    private bool _dailyEveryDay;
    private bool _monday;
    private bool _tuesday;
    private bool _wednesday;
    private bool _thursday;
    private bool _friday;
    private bool _saturday;
    private bool _sunday;

    public bool Monday
    {
        get => _monday;
        set { _monday = value; OnPropertyChanged(); }
    }

    public bool Tuesday
    {
        get => _tuesday;
        set { _tuesday = value; OnPropertyChanged(); }
    }

    public bool Wednesday
    {
        get => _wednesday;
        set { _wednesday = value; OnPropertyChanged(); }
    }

    public bool Thursday
    {
        get => _thursday;
        set { _thursday = value; OnPropertyChanged(); }
    }

    public bool Friday
    {
        get => _friday;
        set { _friday = value; OnPropertyChanged(); }
    }

    public bool Saturday
    {
        get => _saturday;
        set { _saturday = value; OnPropertyChanged(); }
    }

    public bool Sunday
    {
        get => _sunday;
        set { _sunday = value; OnPropertyChanged(); }
    }

    private int _dailyDaysPerWeek = 1;
    private bool _dailyInvalid;

    private readonly bool[] _monthlyDays = new bool[32]; // index 1..31
    private int _monthlyDaysPerMonth = 1;
    private bool _monthlyInvalid;

    private string _intervalDays = "1";
    private bool _intervalInvalid;

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool IsDaily
    {
        get => _isDaily;
        set { _isDaily = value; OnPropertyChanged(); }
    }

    public bool IsMonthly
    {
        get => _isMonthly;
        set { _isMonthly = value; OnPropertyChanged(); }
    }

    public bool IsInterval
    {
        get => _isInterval;
        set { _isInterval = value; OnPropertyChanged(); }
    }

    public bool DailyEveryDay
    {
        get => _dailyEveryDay;
        set { _dailyEveryDay = value; OnPropertyChanged(); }
    }

    public int DailyDaysPerWeek
    {
        get => _dailyDaysPerWeek;
        set { _dailyDaysPerWeek = value; OnPropertyChanged(); }
    }

    public bool DailyInvalid
    {
        get => _dailyInvalid;
        set { _dailyInvalid = value; OnPropertyChanged(); }
    }

    public bool[] MonthlyDays => _monthlyDays;

    public int MonthlyDaysPerMonth
    {
        get => _monthlyDaysPerMonth;
        set { _monthlyDaysPerMonth = value; OnPropertyChanged(); }
    }

    public bool MonthlyInvalid
    {
        get => _monthlyInvalid;
        set { _monthlyInvalid = value; OnPropertyChanged(); }
    }

    public string IntervalDays
    {
        get => _intervalDays;
        set { _intervalDays = value; OnPropertyChanged(); }
    }

    public bool IntervalInvalid
    {
        get => _intervalInvalid;
        set { _intervalInvalid = value; OnPropertyChanged(); }
    }

    public ICommand SaveRegularityCommand { get; }
    public ICommand CancelRegularityCommand { get; }

    public RegularityPageViewModel()
    {
        // default selection
        IsDaily = true;

        SaveRegularityCommand = new Command(async () => await ConfirmAsync());
        CancelRegularityCommand = new Command(async () => await CancelAsync());
    }

    private async Task ConfirmAsync()
    {
        Validate();

        if (DailyInvalid || MonthlyInvalid || IntervalInvalid)
        {
            await Shell.Current.DisplayAlert("Validation error", "Please correct the highlighted options.", "OK");
            return;
        }
        
        var dto = new RegularityDto()
        {
            IsDaily = _isDaily,
            IsMonthly = _isMonthly,
            IsInterval = _isInterval,
            DailyEveryDay = _dailyEveryDay,
            DailyDaysPerWeek = _dailyDaysPerWeek,
            MonthlyDaysPerMonth = _monthlyDaysPerMonth,
            IntervalDays = IntervalDays,
        };
        var serialized = System.Text.Json.JsonSerializer.Serialize(dto);
        await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
        {
            { "RegularityData", serialized }
        });
    }

    private async Task CancelAsync() => await Shell.Current.GoToAsync("..");

    private void Validate()
    {
        if (IsDaily)
        {
            DailyInvalid = !(DailyEveryDay ||
                             DailyDaysPerWeek > 0);
        }
        else
        {
            DailyInvalid = false;
        }

        if (IsMonthly)
        {
            MonthlyInvalid = !(Array.Exists(MonthlyDays, d => d) ||
                               MonthlyDaysPerMonth > 0);
        }
        else
        {
            MonthlyInvalid = false;
        }

        if (IsInterval)
        {
            if (!int.TryParse(IntervalDays, out var n) || n <= 0)
                IntervalInvalid = true;
            else
                IntervalInvalid = false;
        }
        else
        {
            IntervalInvalid = false;
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
