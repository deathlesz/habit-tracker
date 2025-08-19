using HabitTracker.Domain.Enums;
using JFomit.Functional.Monads;

namespace HabitTracker.Domain.Dto;

public record Goal(string Name, MeasurementUnit Unit);
public record Reminder(TimeOnly Time, string Message);

public record Habit(int Id)
{
    public required GoodnessKind Kind { get; init; } 
    public required string Name { get; init; }
    public required Icon Icon { get; init; }
    public required Color Color { get; init; }
    public required Goal Goal { get; init; }
    public PartOfTheDay? PartOfTheDay { get; init; }
    public State State { get; set; } = State.Incomplete;
    public Reminder? Reminder { get; init; }
    public DateOnly? StartDate { get; init; }
    public DateOnly? EndDate { get; init; }
    public string? Description { get; init; }
    
    public required Regularity Regularity { get; init; }
}
