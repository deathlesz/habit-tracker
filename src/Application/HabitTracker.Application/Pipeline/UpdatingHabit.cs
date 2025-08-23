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
        var oldHabit = HabitRepository.Habits.FirstOrDefault(h => h.Id == habit.Id);
        if (oldHabit is null)
        {
            return Error($"No habit with id {habit.Id}.");
        }

        return UpdateNotification(oldHabit, habit).SelectMany(habit, UpdateDb);

        Result<Unit, string> UpdateDb(Unit _, HabitEntity habit)
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

        Result<Unit, string> UpdateNotification(HabitEntity oldHabit, HabitEntity newHabit)
        {
            var newReminder = newHabit.Reminder;
            if (newReminder is null)
            {
                // delete
                return DeleteNotificationFor(oldHabit);
            }
            else if (oldHabit.Reminder is null)
            {
                // add
                return AddNotificationFor(oldHabit, newHabit);
            }
            else
            {
                // update
                var oldReminder = oldHabit.Reminder;
                return HabitReminderRepository.UpdateHabit(oldReminder, r =>
                {
                    r.CyclePatternLength = newReminder.CyclePatternLength;
                    r.CyclesToRun = newReminder.CyclesToRun;
                    r.DaysToNotificate = newReminder.DaysToNotificate.ToArray();
                    r.Message = newReminder.Message;
                    r.StartDate = newReminder.StartDate;
                    r.Time = newReminder.Time;
                }).Select(Discard<HabitReminderEntity>);
            }
        }
    }

    private Result<Unit, string> AddNotificationFor(HabitEntity oldHabit, HabitEntity newHabit)
    {
        return HabitReminderRepository.AddHabit(newHabit.Reminder!).SelectMany(newReminderId =>
            HabitRepository.UpdateHabit(oldHabit, h =>
            {
                h.Reminder = newHabit.Reminder;
                h.Reminder!.Id = newReminderId;
            })
        ).Select(Discard<HabitEntity>);
    }

    private Result<Unit, string> DeleteNotificationFor(HabitEntity habit)
        => HabitReminderRepository
            .DeleteHabit(habit.Reminder!.Id)
            .Select(Discard<HabitReminderEntity>);

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
}
