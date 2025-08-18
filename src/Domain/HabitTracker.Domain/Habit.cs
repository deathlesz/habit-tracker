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

struct Goal
{
    public string Name {get; init;}
    public MeasurementUnit Unit {get; init;}
    
    public Goal(string name, MeasurementUnit unit)
    {
        Name = name;
        Unit = unit;
    }
} 

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

struct Reminder
{
    public TimeOnly Time {get; init;}
    public string Message { get; init; }

    public Reminder(TimeOnly time, string message)
    {
        Time = time;
        Message = message;
    }
}
record Habit
{
    public required GoodnessKind Kind { get; init; } 
    public required string Name { get; init; }
    public required Icon Icon { get; init; }
    public required Color Color { get; init; }
    public required Goal Goal { get; init; }
    public PartOfTheDay PartOfTheDay { get; init; }
    public State State { get; set; } = State.Incomplete;
    public Reminder Reminder { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public string Description { get; init; }
    
    public required Regularity Regularity { get; init; }
}
