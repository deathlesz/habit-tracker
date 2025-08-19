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
        public Result<HabitReminderEntity, string> SetRepetitiveNotification(string message, DateOnly StartDate, int CyclePatternLength, ICollection<int> DaysToNotificate, int? CyclesToRun = null);
        public Result<HabitReminderEntity, string> DeleteRepetitiveNotification(int id);
    }
}
