using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Domain.Entities;
using JFomit.Functional.Monads;
using HabitTracker.Application.Interfaces.Services;

namespace HabitTracker.Infrastructure.Services.Notification;
/// <summary>
/// Service responsible for managing habit reminders on the Android platform.
/// Provides functionality to create and delete repetitive notifications.
/// </summary>
public class AndroidNotificationService : INotificationService
{
    private IHabitReminderRepository _habitReminderRepository;
    /// <summary>
    /// Initializes a new instance of the <see cref="AndroidNotificationService"/> class.
    /// </summary>
    public AndroidNotificationService(IHabitReminderRepository habitReminderRepository)
    {
        _habitReminderRepository = habitReminderRepository;
    }
    /// <summary>
    ///Creates a new repetitive habit reminder with the specified parameters.
    /// </summary>
    /// <param name="message">The notification message to display.</param>
    /// <param name="startDate">The date when the reminder starts.</param>
    /// <param name="cyclePatternLength">The number of days in one cycle.</param>
    /// <param name="daysToNotificate">The specific days in the cycle when reminders should be sent.</param>
    /// <param name="cyclesToRun">Optional. Number of cycles to run. If null, the reminder will repeat indefinitely.</param>
    /// <returns>
    /// A <see cref="Result{T, string}"/> containing the created <see cref="HabitReminderEntity"/> 
    /// if successful, or an error message if validation or saving fails.
    /// </returns>
    public Result<HabitReminderEntity, string> SetRepetitiveNotification(
        string message, 
        DateOnly startDate, 
        int cyclePatternLength,
        ICollection<int> daysToNotificate,
        int? cyclesToRun = null
        )
    {
        if (cyclePatternLength < 1)
        {
            return Result<HabitReminderEntity, string>.Fail("cycle pattern length must be greater than zero.");
        }
        if (daysToNotificate is null || daysToNotificate.Count == 0)
        {
            return Result<HabitReminderEntity, string>.Fail("daysToNotificate must not be empty.");
        }
        if (daysToNotificate.Any(d => d < 1 || d >= cyclePatternLength))
        {
            return Result<HabitReminderEntity, string>.Fail("going beyond the boundaries of the cycle");
        }
        if (cyclesToRun != null && cyclesToRun <= 0)
        {
            return Result<HabitReminderEntity, string>.Fail("cycles toRun must be greater than zero.");
        }
        
        var remider = new HabitReminderEntity
        {
            StartDate = startDate,
            DaysToNotificate = daysToNotificate,
            CyclePatternLength = cyclePatternLength,
            CyclesToRun = cyclesToRun,
            Message = message,
        };
        
        var result = _habitReminderRepository.AddHabit(remider);

        if (result.IsSuccess)
        {
            return Result<HabitReminderEntity, string>.Ok(remider);
        }
        else
        {
            return Result<HabitReminderEntity, string>.Fail(result.Error);
        }
    }
    /// <summary>
    /// Deletes an existing repetitive notification by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the habit reminder to delete.</param>
    /// <returns>
    /// A <see cref="Result{T, string}"/> containing the deleted <see cref="HabitReminderEntity"/> 
    /// if successful, or an error message if not found or deletion fails.
    /// </returns>
    public Result<HabitReminderEntity, string> DeleteRepetitiveNotification(int id)
    {
        return _habitReminderRepository.DeleteHabit(id);
    }
}