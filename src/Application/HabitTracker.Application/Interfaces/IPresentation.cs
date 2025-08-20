using HabitTracker.Application.Dto;
using JFomit.Functional;
using JFomit.Functional.Monads;

namespace HabitTracker.Application.Interfaces;

public interface IPresentation
{
    public Result<Habit, string> CreateHabit(Habit habit);
    public Result<Unit, string> UpdateHabit(Habit habit);
    public Result<Unit, string> DeleteHabit(int id);
    public Result<ICollection<Habit>, string> GetAllHabits();
}