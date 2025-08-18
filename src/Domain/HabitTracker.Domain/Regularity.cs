namespace HabitTracker.Domain;

abstract record Regularity;
abstract record DailyRegularity;
abstract record MonthlyRegularity;
sealed record Daily(DailyRegularity DailyRegularity) : Regularity;
sealed record Monthly(MonthlyRegularity MonthlyRegularity) : Regularity;
sealed record EveryNDays(uint Count) : Regularity;

sealed record DaysOfTheWeek(byte WeekDays) : DailyRegularity
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
}
sealed record TimesPerWeek(uint Count) : DailyRegularity;

sealed record ConcreteDays(uint MonthDays) : MonthlyRegularity
{
    public ConcreteDays(params ReadOnlySpan<int> days):
        this(PackDayOfWeekSpan(days))
    {}

    private static uint PackDayOfWeekSpan(ReadOnlySpan<int> days)
    {
        uint result = 0;
        for (int i = 0; i < days.Length; i++)
        {
            result |= 1u << (days[i] - 1);
        } 
        return result;
    }

    public bool isDaySet(int day)
    {
        return (MonthDays & (1u << (day - 1))) != 0;
    }
};
sealed record TimesPerMonth(uint Count) : MonthlyRegularity;
