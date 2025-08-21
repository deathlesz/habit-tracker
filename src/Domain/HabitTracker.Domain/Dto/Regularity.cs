namespace HabitTracker.Domain.Dto;

/// <summary>
/// A kind of regularity. Can be one of <see cref="DailyRegularity"/>, <see cref="MonthlyRegularity"/> or <see cref="EveryNDays"/>.
/// </summary>
/// <remarks>
/// Use pattern matching to extract the concrete regularity record:
/// </remarks>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching"/>
public abstract record Regularity;
/// <summary>
/// A kind of daily regularity.
/// </summary>
public abstract record DailyRegularity;
/// <summary>
/// A kind of monthly regularity.
/// </summary>
public abstract record MonthlyRegularity;
/// <summary>
/// A variant of <see cref="Regularity"/>, the daily one.
/// </summary>
public sealed record Daily(DailyRegularity DailyRegularity) : Regularity;
/// <summary>
/// A variant of <see cref="Regularity"/>, the monthly one.
/// </summary>
public sealed record Monthly(MonthlyRegularity MonthlyRegularity) : Regularity;
/// <summary>
/// A variant of <see cref="Regularity"/>, the <i>Every N days</i> one.
/// </summary>
public sealed record EveryNDays(uint Count) : Regularity;

/// <summary>
/// A variant of <see cref="DailyRegularity"/>, the <i>Days per week</i> one.
/// </summary>
public sealed record DaysOfTheWeek(byte WeekDays) : DailyRegularity
{
    public DaysOfTheWeek(params ReadOnlySpan<DayOfWeek> days)
        : this(PackDayOfWeekSpan(days))
    { }

    private static byte PackDayOfWeekSpan(ReadOnlySpan<DayOfWeek> days)
    {
        byte result = 0;

        for (int i = 0; i < days.Length; i++)
        {
            result |= days[i] switch
            {
                DayOfWeek.Monday => 1,
                DayOfWeek.Tuesday => 2,
                DayOfWeek.Wednesday => 4,
                DayOfWeek.Thursday => 8,
                DayOfWeek.Friday => 16,
                DayOfWeek.Saturday => 32,
                DayOfWeek.Sunday => 64,

                _ => 0
            };
        }

        return result;
    }

    public bool IsDaySet(DayOfWeek day) => day switch
    {
        DayOfWeek.Monday => (WeekDays & 1) != 0,
        DayOfWeek.Tuesday => (WeekDays & 2) != 0,
        DayOfWeek.Wednesday => (WeekDays & 4) != 0,
        DayOfWeek.Thursday => (WeekDays & 8) != 0,
        DayOfWeek.Friday => (WeekDays & 16) != 0,
        DayOfWeek.Saturday => (WeekDays & 32) != 0,
        DayOfWeek.Sunday => (WeekDays & 64) != 0,

        _ => false,
    };

    public List<int> Extract()
    {
        var lst = new List<int>();
        lst
            .AppendIfSet(this, DayOfWeek.Monday)
            .AppendIfSet(this, DayOfWeek.Thursday)
            .AppendIfSet(this, DayOfWeek.Wednesday)
            .AppendIfSet(this, DayOfWeek.Thursday)
            .AppendIfSet(this, DayOfWeek.Friday)
            .AppendIfSet(this, DayOfWeek.Saturday)
            .AppendIfSet(this, DayOfWeek.Sunday);
            
        return lst;
    }

}

file static class ListExtensions
{
    internal static List<int> AppendIfSet(this List<int> lst, DaysOfTheWeek obj, DayOfWeek dayOfWeek)
    {
        if (obj.IsDaySet(dayOfWeek))
        {
            lst.Add(dayOfWeek switch
            {
                DayOfWeek.Monday => 0,
                DayOfWeek.Tuesday => 1,
                DayOfWeek.Wednesday => 2,
                DayOfWeek.Thursday => 3,
                DayOfWeek.Friday => 4,
                DayOfWeek.Saturday => 5,
                DayOfWeek.Sunday => 6,

                _ => 0
            });
        }

        return lst;
    }
}


/// <summary>
/// A variant of <see cref="DailyRegularity"/>, a times per week one.
/// </summary>
public sealed record TimesPerWeek(uint Count) : DailyRegularity;

/// <summary>
/// A variant of <see cref="MonthlyRegularity"/>.
/// </summary>
public sealed record ConcreteDays(uint MonthDays) : MonthlyRegularity
{
    public ConcreteDays(params ReadOnlySpan<int> days) :
        this(PackDayOfWeekSpan(days))
    { }

    private static uint PackDayOfWeekSpan(ReadOnlySpan<int> days)
    {
        uint result = 0;
        for (int i = 0; i < days.Length; i++)
        {
            result |= 1u << (days[i] - 1);
        }
        return result;
    }

    public bool IsDaySet(int day)
    {
        return (MonthDays & (1u << (day - 1))) != 0;
    }
}

/// <summary>
/// A variant of <see cref="MonthlyRegularity"/>, a times per month one.
/// </summary>
public sealed record TimesPerMonth(uint Count) : MonthlyRegularity;
