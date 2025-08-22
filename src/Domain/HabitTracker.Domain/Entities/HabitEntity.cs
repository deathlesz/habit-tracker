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
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required GoalInfo Goal { get; set; }
        /// <summary>
        /// Info about notification
        /// </summary>
        public HabitReminderEntity? Reminder { get; set; }
        /// <summary>
        /// Info about schedule and its completing stage
        /// </summary>
        public required HabitScheduleEntity Regularity { get; set; }
        public required GoodnessKind Kind { get; set; }
        public required Icon Icon { get; set; }
        public required Color Color { get; set; }
        public PartOfTheDay? PartOfTheDay { get; set; }
        public State State { get; set; } = State.Incomplete;
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
