using HabitTracker.Domain.Entities.Regularity;
using HabitTracker.Domain.Enums;

namespace HabitTracker.Domain.Entities
{
    public record GoalInfo(string Name, MeasurementUnit Unit);
    public record ReminderInfo(TimeOnly Time, string Message);

    public class HabitEntity
    {
        /// <summary>
        /// Название привычки
        /// </summary>
        public required string Name { get; init; }
        /// <summary>
        /// Текстовое описание привычки
        /// </summary>
        public string Description { get; init; }
        public required GoalInfo Goal { get; init; }
        /// <summary>
        /// Информация об уведомлении
        /// </summary>
        public ReminderInfo Reminder { get; init; }
        /// <summary>
        /// Информация о расписании привычки и его выполнении
        /// </summary>
        public required HabitScheduleEntity Regularity { get; init; }
        public required GoodnessKind Kind { get; init; }
        public required Icon Icon { get; init; }
        public required Color Color { get; init; }
        public PartOfTheDay? PartOfTheDay { get; init; }
        public State? State { get; set; } =  (State?)Enums.State.Incomplete;
        public DateOnly? StartDate { get; init; }
        public DateOnly? EndDate { get; init; }
        
    }
}
