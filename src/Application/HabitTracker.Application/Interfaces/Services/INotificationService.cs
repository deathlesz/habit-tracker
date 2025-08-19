using HabitTracker.Domain.Entities;
using JFomit.Functional.Monads;

namespace HabitTracker.Application.Interfaces.Services
{
    /// <summary>
    /// Notification installation service. Pattern notifications have their own unique id.
    /// All information about the notification pattern will be stored in the database in HabitEntity->HabitReminderEntity) 
    /// IHabitsReminderRepository is used to work with the database
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Starting from StartDate, a notification should be sent to the phone on each specified day of the pattern. 
        /// This loop is repeated CyclesToRun times (null => infinitely).
        /// </summary>
        public Result<HabitReminderEntity, string> SetRepetitiveNotification(string message, DateOnly startDate, int cyclePatternLength, ICollection<int> daysToNotificate, int? cyclesToRun = null);
        public Result<HabitReminderEntity, string> DeleteRepetitiveNotification(int id);
    }
}
