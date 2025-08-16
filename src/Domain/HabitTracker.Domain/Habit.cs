namespace HabitTracker.Domain;

enum GoodnessKind
{
    Positive = 0,
    Negative,
}

enum Icon
{
    Default = 0,

    Training,
    Running,
    DrinkingWater
}

enum Color
{
    Black = 0,
    Red,
    Green,
    Yellow,
    Blue,
    Magenta,
    Cyan,
    White
}

record Habit
{
    public required GoodnessKind Kind { get; init; }
    public required string Name { get; init; }
    public required Icon Icon { get; init; }
    public required Color Color { get; init; }
}

abstract record Regularity;
sealed record Daily(DailyRegularity DailyRegularity) : Regularity;
// sealed record Monthly() : Regularity;
sealed record EveryNDays(uint Count) : Regularity;

abstract record DailyRegularity;
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
