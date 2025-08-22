using HabitTracker.Application.Interfaces;
using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Application.Interfaces.Services;
using HabitTracker.Domain.Dto;
using JFomit.Functional;
using JFomit.Functional.Monads;
using JFomit.Functional.Extensions;

namespace HabitTracker.Application;

public class PresentationGateway(IHabitRepository habitRepository, INotificationService notificationService) : IPresentation
{
    public IHabitRepository HabitRepository { get; } = habitRepository;
    public INotificationService NotificationService { get; } = notificationService;

    public Result<Habit, string> CreateHabit(Habit habit)
    {
        throw new NotImplementedException();
    }

    public Result<Unit, string> DeleteHabit(int id)
    {
        return HabitRepository.DeleteHabit(id)
            .SelectMany(_ => NotificationService.DeleteRepetitiveNotification(id).Select2(ok => ok, error => "couldn't delete notification: " + error))
            .Select2(_ => Prelude.Unit, error => "couldn't delete habit: " + error);
    }

    public Result<ICollection<Habit>, string> GetAllHabits()
    {
        throw new NotImplementedException();
    }

    public Result<Unit, string> UpdateHabit(Habit habit)
    {
        throw new NotImplementedException();
    }
}
