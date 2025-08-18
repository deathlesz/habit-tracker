using Android.Media;
using JFomit.Functional.Monads;
using Microsoft.Maui.Animations;

namespace HabitTracker.Domain;

record Goal(string Name, MeasurementUnit Unit);
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
