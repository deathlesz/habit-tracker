using Android.Media;
using JFomit.Functional.Monads;
using Microsoft.Maui.Animations;

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

enum MeasurementUnit
{
    Count = 0,
    Steps,
    M,
    Km,
    Sec,
    Min,
    Hr,
    Ml,
    Cal,
    G,
    Mg,
    Drink
}

record Goal(string Name, MeasurementUnit Unit);

//Optional

enum PartOfTheDay
{
    Morning = 0,
    Afternoon,
    Night
}

enum State
{
    Incomplete = 0,
    Complete,
}

record Reminder(TimeOnly Time, string Message);

record Habit
{
    public required GoodnessKind Kind { get; init; } 
    public required string Name { get; init; }
    public required Icon Icon { get; init; }
    public required Color Color { get; init; }
    public required Goal Goal { get; init; }
    public Option<PartOfTheDay> PartOfTheDay { get; init; }
    public State State { get; set; } = State.Incomplete;
    public Option<Reminder> Reminder { get; init; }
    public Option<DateOnly> StartDate { get; init; }
    public Option<DateOnly> EndDate { get; init; }
    public Option<string> Description { get; init; }
    
    public required Regularity Regularity { get; init; }
}
