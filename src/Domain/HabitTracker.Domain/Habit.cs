using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using MauiColor = Microsoft.Maui.Graphics.Color;

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
sealed record Daily : Regularity;
sealed record Monthly : Regularity;
sealed record EveryNDays(int Count) : Regularity;

abstract record DailyRegularity;
sealed record DaysOfTheWeek(byte WeekDays)
{
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
