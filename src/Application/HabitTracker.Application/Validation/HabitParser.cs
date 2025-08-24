using System.Diagnostics;
using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Entities;
using HabitTracker.Domain.Entities.Regularity;
using HabitTracker.Domain.Enums;
using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

namespace HabitTracker.Application.Validation;

static class HabitParser
{
    public static Result<HabitEntity, string> ToEntity(this Habit habit)
    {
        Result<Habit, string> result = Ok(habit);
        return result
            .Where(habit => !string.IsNullOrWhiteSpace(habit.Name), orElse: "Invalid or empty name.")
            .SelectMany(CheckGoal)
            .SelectMany(CheckDates)
            .SelectMany(CheckReminder)
            .SelectMany(CheckDescription)
            .SelectMany(CheckRegularity)
            .Select(ConvertToEntity);
    }

    static Result<T, E> Where<T, E>(this Result<T, E> result, Func<T, bool> predicate, E orElse)
    {
        if (result.TryUnwrap2(out var success, out var originalError))
        {
            return predicate(success) ? Ok(success) : Error(orElse);
        }
        else
        {
            return Error(originalError);
        }
    }

    static Result<Habit, string> CheckGoal(Habit habit)
        => string.IsNullOrWhiteSpace(habit.Goal.Name) ? Error("Invalid or empty goal.") : Ok(habit);
    static Result<Habit, string> CheckDates(Habit habit)
    {
        if (habit.StartDate is DateOnly startDate && habit.EndDate is DateOnly endDate)
        {
            return startDate < endDate ? Ok(habit) : Error("Start date is later than or equal to End date.");
        }

        return Ok(habit);
    }
    private static Result<Habit, string> CheckReminder(Habit habit)
    {
        if (habit.Reminder is Reminder reminder)
        {
            return string.IsNullOrWhiteSpace(reminder.Message) ? Error("Invalid or empty reminder message.") : Ok(habit);
        }

        return Ok(habit);
    }
    private static Result<Habit, string> CheckDescription(Habit habit)
    {
        if (habit.Description is string description)
        {
            return string.IsNullOrWhiteSpace(description) ? Error("Invalid or empty description.") : Ok(habit);
        }

        return Ok(habit);
    }
    private static Result<Habit, string> CheckRegularity(Habit habit) => habit.Regularity switch
    {
        Daily(var dailyRegularity) => CheckDaily(habit, dailyRegularity),
        Monthly(var monthlyRegularity) => CheckMonthly(habit, monthlyRegularity),
        EveryNDays(var days) => days != 0 ? Ok(habit) : Error("Can't repeat every 0 days."),

        _ => throw new UnreachableException()
    };

    private static Result<Habit, string> CheckMonthly(Habit habit, MonthlyRegularity monthlyRegularity) => monthlyRegularity switch
    {
        ConcreteDays(var days) => days != 0
            ? Error("No month day specified.")
            : Ok(habit),
        TimesPerMonth(var count) => count > 0 && count <= 31
            ? Error("Can only choose a number of days that can appear in a month.")
            : Ok(habit),

        _ => throw new UnreachableException(),
    };

    private static Result<Habit, string> CheckDaily(Habit habit, DailyRegularity dailyRegularity) => dailyRegularity switch
    {
        DaysOfTheWeek daysOfTheWeek => daysOfTheWeek.WeekDays == 0 ? Error("No week days specified.") : Ok(habit),
        TimesPerWeek(var times) => times > 0 && times <= 7
            ? Ok(habit)
            : Error("Can only repeat between 1 and 7 times per week."),

        _ => throw new UnreachableException()
    };

    private static HabitEntity ConvertToEntity(Habit habit)
    {
        var regularity = HabitReminderParser.ConvertRegularity(habit.Regularity, habit);

        var entity = new HabitEntity()
        {
            Name = habit.Name,
            Color = habit.Color,
            Icon = habit.Icon,
            Goal = new GoalInfo(habit.Goal.Name, habit.Goal.Unit),
            Kind = habit.Kind,
            Description = habit.Description,
            StartDate = habit.StartDate,
            EndDate = habit.EndDate,
            // HabitRegularityType = ConvertRegularity(habit.Regularity) // will not be used
            Regularity = regularity,
            Reminder = ConvertReminder(habit.Reminder, habit, regularity),
            PartOfTheDay = habit.PartOfTheDay,
            State = habit.State,
            Id = -1,
        };

        return entity;
    }

    private static HabitReminderEntity? ConvertReminder(Reminder? reminderOption, Habit habit, HabitScheduleEntity habitSchedule)
    {
        if (reminderOption is Reminder reminder && habitSchedule.IsAllMachedDays)
        {
            // TODO: smb, figure out how to set notifications for habits w/o strict schedules and go fix this mess
            return new()
            {
                Time = reminder.Time,
                StartDate = habitSchedule.StartDate ?? DateOnly.FromDateTime(DateTime.Now), // idk what to do if unset
                Id = -1, // will be set by db
                Message = reminder.Message,
                DaysToNotificate = habitSchedule.RepeatingDatesToMatch!,
                CyclePatternLength = habitSchedule.RepeatingCycleDays,
                CyclesToRun = null, // idk, infinite for now
            };
        }

        return null;
    }
}
