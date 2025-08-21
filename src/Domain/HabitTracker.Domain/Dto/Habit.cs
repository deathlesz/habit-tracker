using HabitTracker.Domain.Enums;
using JFomit.Functional.Monads;
using Color = HabitTracker.Domain.Color;
using State = HabitTracker.Domain.Enums.State;

namespace HabitTracker.Domain.Dto;

/// <summary>
/// The goal to achieve.
/// </summary>
public record Goal(string Name, MeasurementUnit Unit);
/// <summary>
/// A reminder.
/// </summary>
public record Reminder(TimeOnly Time, string Message);

/// <summary>
/// A habit.
/// </summary>
/// <param name="Id">This habit's unique identifier.</param>
public record Habit(int Id)
{
    /// <summary>
    /// Kind, either <see cref="GoodnessKind.Positive"/> or <see cref="GoodnessKind.Negative"/>.
    /// </summary>
    public required GoodnessKind Kind { get; init; }
    /// <summary>
    /// The habit's name.
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// The habit's icon.
    /// </summary>
    public required Icon Icon { get; init; }
    /// <summary>
    /// The habit's color.
    /// </summary>
    public required Color Color { get; init; }
    /// <summary>
    /// The goal to achieve for this habit.
    /// </summary>
    public required Goal Goal { get; init; }
    /// <summary>
    /// The part of the day the habit should be finished by.
    /// </summary>
    public PartOfTheDay? PartOfTheDay { get; init; }
    /// <summary>
    /// The habit's state, either <see cref="State.Complete"/> or <see cref="State.Incomplete"/>.
    /// </summary>
    public State State { get; set; } = State.Incomplete;
    /// <summary>
    /// The <see cref="Dto.Reminder"/> associated with the habit, if any.
    /// </summary>
    public Reminder? Reminder { get; init; }
    /// <summary>
    /// The date by which the habit tracking is started.
    /// </summary>
    public DateOnly? StartDate { get; init; }
    /// <summary>
    /// The date by which the habit tracking is to end.
    /// </summary>
    public DateOnly? EndDate { get; init; }
    /// <summary>
    /// Optional description.
    /// </summary>
    public string? Description { get; init; }
    /// <summary>
    /// Regularity (how often) of the habit.
    /// </summary>
    public required Regularity Regularity { get; init; }
}
