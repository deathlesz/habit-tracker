using HabitTracker.Application.Interfaces;
using HabitTracker.Domain.Dto;
using JFomit.Functional;
using JFomit.Functional.Monads;

namespace HabitTracker.Presentation.ViewModel;

public class DummyPresentation : IPresentation
{
    public Result<Habit, string> CreateHabit(Habit habit)
    {
        throw new NotImplementedException();
    }

    public Result<Unit, string> UpdateHabit(Habit habit)
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
}