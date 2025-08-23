using System.Diagnostics;
using Android.App;
using HabitTracker.Application.Interfaces.Repositories;
using HabitTracker.Application.Validation;
using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Entities;
using JFomit.Functional;
using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

namespace HabitTracker.Application.Pipeline;

class UpdatingHabit(IHabitRepository habitRepository, IHabitReminderRepository habitReminderRepository)
{
    public IHabitRepository HabitRepository { get; } = habitRepository;
    public IHabitReminderRepository HabitReminderRepository { get; } = habitReminderRepository;

    public Result<Unit, string> DoUpdate(Habit habit)
    {
        Result<Habit, string> result = Ok(habit);
        return result
            .SelectMany(HabitParser.ToEntity)
            .SelectMany(DoUpdate);
    }

    private Result<Unit, string> DoUpdate(HabitEntity habit)
    {
        Debug.Assert(habit.Id >= 0); // a sanity check
        return UpdateDb(habit).SelectMany(habit, UpdateNotification);

        Result<Unit, string> UpdateDb(HabitEntity habit)
        {
            var dummy = MakeDummyHabitWithId(habit.Id);

            return HabitRepository.UpdateHabit(dummy, (HabitEntity h) =>
            {
                // Do no set Id here!
                h.Color = habit.Color;
                h.Description = habit.Description;
                h.EndDate = habit.EndDate;
                h.Goal = habit.Goal;
                //h.HabitRegularityType = habit.HabitRegularityType; // will be removed
                h.Icon = habit.Icon;
                h.Kind = habit.Kind;
                h.Name = habit.Name;
                h.PartOfTheDay = habit.PartOfTheDay;
                h.Regularity = habit.Regularity;
                h.Reminder = habit.Reminder;
                h.StartDate = habit.StartDate;
                h.State = habit.State;
            }).Select(Discard<HabitEntity>);
        }

        Result<Unit, string> UpdateNotification(Unit _, HabitEntity habit)
        {
            var reminder =
                from aHabit in HabitRepository.Habits
                select aHabit;

            
        }
    }

    // I should say, this is a very bad design
    private static HabitEntity MakeDummyHabitWithId(int id) => new()
    {
        Color = default,
        Goal = null!,
        Icon = default,
        Kind = default,
        Name = null!,
        Regularity = null!,

        Id = id,
    };
    private static HabitReminderEntity MakeDummyReminderWithId(int id) => new()
    {
        DaysToNotificate = null!,

        Id = id,
    };
}
