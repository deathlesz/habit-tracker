using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Domain.Entities;
using JFomit.Functional.Monads;
using HabitTracker.Application.Interfaces.Services;

namespace HabitTracker.Infrastructure.Services.Notification;

public class AndroidNotificationService : INotificationService
{
    private IHabitReminderRepository _habitReminderRepository;
    public AndroidNotificationService(IHabitReminderRepository habitReminderRepository)
    {
        _habitReminderRepository = habitReminderRepository;
    }
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

    public Result<HabitReminderEntity, string> DeleteRepetitiveNotification(int id)
    {
        return _habitReminderRepository.DeleteHabit(id);
    }
}