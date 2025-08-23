using HabitTracker.Application.Interfaces;
using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Application.Interfaces.Services;
using HabitTracker.Application.Pipeline;
using HabitTracker.Domain.Dto;
using JFomit.Functional;
using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;

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
        throw new NotImplementedException();
    }

    public Result<ICollection<Habit>, string> GetAllHabits()
    {
        throw new NotImplementedException();
    }

    public Result<Unit, string> UpdateHabit(Habit habit) => UpdatingHabit.DoUpdate(habit);
}
