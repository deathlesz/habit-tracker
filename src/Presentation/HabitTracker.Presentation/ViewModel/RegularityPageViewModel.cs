using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using HabitTracker.Domain.Dto;
using JFomit.Functional;
using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;
using Microsoft.Maui.Controls;

namespace HabitTracker.Presentation.ViewModel;

public partial class RegularityPageViewModel : ObservableObject
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

    // public event PropertyChangedEventHandler? PropertyChanged;

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

        await Shell.Current.GoToAsync("..", new ShellNavigationQueryParameters()
        {
            { "NewRegularity", CreateRegularity() }
        });
    }

    private async Task CancelAsync() => await Shell.Current.GoToAsync("..");

    private Regularity CreateRegularity()
    {
        if (IsDaily)
        {
            if (DailyEveryDay)
            {
                return new Daily(new DaysOfTheWeek(
                [
                    DayOfWeek.Monday,
                    DayOfWeek.Tuesday,
                    DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,
                    DayOfWeek.Friday,
                    DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                ]));
            }

            if (Monday || Tuesday || Wednesday || Thursday || Friday || Saturday || Sunday)
            {
                var lst = new List<DayOfWeek>();
                AppendIfDaySet(lst, Monday, DayOfWeek.Monday);
                AppendIfDaySet(lst, Tuesday, DayOfWeek.Tuesday);
                AppendIfDaySet(lst, Wednesday, DayOfWeek.Wednesday);
                AppendIfDaySet(lst, Thursday, DayOfWeek.Thursday);
                AppendIfDaySet(lst, Friday, DayOfWeek.Friday);
                AppendIfDaySet(lst, Saturday, DayOfWeek.Saturday);
                AppendIfDaySet(lst, Sunday, DayOfWeek.Sunday);

                return new Daily(new DaysOfTheWeek(lst.ToArray()));
            }

            return new Daily(new TimesPerWeek((uint)DailyDaysPerWeek));
        }
        if (IsMonthly)
        {
            if (MonthlyDays.Any(d => d))
            {
                var days = MonthlyDays
                    .Select((d, i) => (d, i))
                    .Where(item => item.d)
                    .Select(item => item.i + 1)
                    .ToArray();
                return new Monthly(new ConcreteDays(days));
            }
            else
            {
                return new Monthly(new TimesPerMonth((uint)MonthlyDaysPerMonth));
            }
        }
        if (IsInterval)
        {
            return new EveryNDays(uint.Parse(IntervalDays));
        }

        throw new UnreachableException();

        static void AppendIfDaySet(List<DayOfWeek> lst, bool flag, DayOfWeek dayOfWeek)
        {
            if (flag)
            {
                lst.Add(dayOfWeek);
            }
        }
    }

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

    // private void OnPropertyChanged([CallerMemberName] string? name = null)
    //     => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    [ObservableProperty]
    private Option<Regularity> regularity;

    partial void OnRegularityChanged(Option<Regularity> value)
    {
        if (!value.TryUnwrap(out var regularity))
        {
            return;
        }

        switch (regularity)
        {
            case Daily(var daily):
                {
                    IsDaily = true;
                    IsMonthly = false;
                    IsInterval = false;

                    if (daily is TimesPerWeek(var times))
                    {
                        DailyDaysPerWeek = (int)times;
                    }
                    else
                    {
                        var days = ((DaysOfTheWeek)daily);
                        Monday = days.IsDaySet(DayOfWeek.Monday);
                        Tuesday = days.IsDaySet(DayOfWeek.Tuesday);
                        Wednesday = days.IsDaySet(DayOfWeek.Wednesday);
                        Thursday = days.IsDaySet(DayOfWeek.Thursday);
                        Friday = days.IsDaySet(DayOfWeek.Friday);
                        Saturday = days.IsDaySet(DayOfWeek.Saturday);
                        Sunday = days.IsDaySet(DayOfWeek.Sunday);
                    }
                    break;
                }

            case Monthly(var monthly):
                {
                    IsDaily = false;
                    IsMonthly = true;
                    IsInterval = false;

                    if (monthly is TimesPerMonth(var times))
                    {
                        MonthlyDaysPerMonth = (int)times;
                    }
                    else
                    {
                        var days = monthly as ConcreteDays;
                        var d = days!.UnpackDays();
                        for (int i = 0; i < MonthlyDays.Length; i++)
                        {
                            MonthlyDays[i] = d.Contains(i + 1) ? true : false;
                        }
                        OnPropertyChanged(nameof(MonthlyDays));
                    }
                    break;
                }

            case EveryNDays(var count):
                IsDaily = false;
                IsMonthly = false;
                IsInterval = true;

                IntervalDays = count.ToString();
                break;

            default:
                throw new UnreachableException();
        }
    }

    // public void ApplyQueryAttributes(IDictionary<string, object> query)
    // {
    //     if (query.TryGetValue("regularity", out var regularity))
    //     {
    //         Regularity = Prelude.Some((Regularity)regularity);
    //     }
    // }
}
