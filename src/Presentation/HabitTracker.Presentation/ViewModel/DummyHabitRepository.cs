using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Entities;
using JFomit.Functional.Monads;

namespace HabitTracker.Presentation.ViewModel;

public class DummyHabitRepository : IHabitRepository
{
    private List<HabitEntity> _habits = new();
    public IQueryable<HabitEntity> Habits => _habits.AsQueryable();
    
    public ICollection<HabitEntity> GetAll() => _habits;

    public Result<int, string> AddHabit(HabitEntity habitEntity)
    {
        habitEntity.Id = _habits.Count + 1;
        _habits.Add(habitEntity);
        return Result<int, string>.Ok(habitEntity.Id);
    }

    public Result<HabitEntity, string> DeleteHabit(int id)
    {
        var habit = _habits.Find(habit => habit.Id == id);
        if (habit is null)
        {
            return Result<HabitEntity, string>.Fail("Habit not found");
        }
        return Result<HabitEntity, string>.Ok(habit);
    }

    public Result<HabitEntity, string> UpdateHabit(HabitEntity habitEntity, Action<HabitEntity> action)
    {
        var habit = _habits.Find(habit => habit.Id == habitEntity.Id);
        if (habit is null)
        {
            return Result<HabitEntity, string>.Fail("Habit not found");
        }
        action(habit);
        return Result<HabitEntity, string>.Ok(habit);
    }
}