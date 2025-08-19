using HabitTracker.Domain.Entities.Regularity;
using HabitTracker.Domain.Enums;

namespace HabitTracker.Domain.Entities
{
    public record GoalInfo(string Name, MeasurementUnit Unit);

    public class HabitEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// Discriminator of habits regularity type
        /// </summary>
        public HabitRegularityType HabitRegularityType { get; set; }
        /// <summary>
        /// Name of Habit
        /// </summary>
        public required string Name { get; init; }
        public string? Description { get; init; }
        public required GoalInfo Goal { get; init; }
        /// <summary>
        /// Info about notification
        /// </summary>
        public HabitReminderEntity? Reminder { get; init; }
        /// <summary>
        /// Info about schedule and its completing stage
        /// </summary>
        public required HabitScheduleEntity Regularity { get; init; }
        public required GoodnessKind Kind { get; init; }
        public required Icon Icon { get; init; }
        public required Color Color { get; init; }
        public PartOfTheDay? PartOfTheDay { get; init; }
        public State State { get; set; } = State.Incomplete;
        public DateOnly? StartDate { get; init; }
        public DateOnly? EndDate { get; init; }
    }
}
