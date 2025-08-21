using HabitTracker.Application.Dto;
using HabitTracker.Application.Interfaces;
using JFomit.Functional;
using JFomit.Functional.Monads;
using JFomit.Functional.Extensions;
using static JFomit.Functional.Prelude;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using HabitTracker.Domain.Entities;
using HabitTracker.Domain.Entities.Regularity;

namespace HabitTracker.Application;

public class PresentationGateway : IPresentation
{
    public Result<Habit, string> CreateHabit(Habit habit)
    {
        Result<Habit, string> result = Ok(habit);

        return result
            // .Where(static habit => Enum.IsDefined(habit.Kind), "Invalid kind.")
            .Where(static habit => !string.IsNullOrWhiteSpace(habit.Name), "Empty name.")
            // .Where(static habit => Enum.IsDefined(habit.Icon), "Invalid icon.")
            // .Where(static habit => Enum.IsDefined(habit.Color), "Invalid color.")
            .Where(static habit => !string.IsNullOrWhiteSpace(habit.Goal.Name), "Invalid goal name.")
            // .Where(static habit => Enum.IsDefined(habit.Goal.Unit), "Invalid measurement unit.")
            // .Where(static habit => habit.PartOfTheDay.TryUnwrap(out var part) && Enum.IsDefined(part), "Invalid part of the day.")
            .Where(static habit => habit.Description.TryUnwrap(out var d) && !string.IsNullOrWhiteSpace(d), "Invalid description.")
            .Where(static habit => habit.Regularity switch
            {
                Daily(var dailyRegularity) => dailyRegularity switch
                {
                    DaysOfTheWeek(var days) => days != 0,
                    TimesPerWeek(var count) => count > 0 && count <= 7,
                    _ => throw new UnreachableException()
                },
                Monthly(var monthlyRegularity) => monthlyRegularity switch
                {
                    TimesPerMonth(var times) => times > 0,
                    ConcreteDays(var concrete) => concrete != 0,
                    _ => throw new UnreachableException()
                },
                EveryNDays(var days) => days > 0,
                _ => throw new UnreachableException()
            },
            "Invalid regularity.")
            .Select(habit =>
                new HabitEntity()
                {
                    Kind = habit.Kind,
                    Color = habit.Color,
                    Goal = new GoalInfo(habit.Goal.Name, habit.Goal.Unit)0,
                    Description = habit.Description.ToNullable(),
                    Icon = habit.Icon,
                    Name = habit.Name,
                    EndDate = habit.EndDate.ToNullable(),
                    StartDate = habit.StartDate.ToNullable(),
                    Regularity = CreateFromRegularity(habit.Regularity)
                }
            );
    }

    private static HabitScheduleEntity CreateFromRegularity(Regularity regularity)
    {
        return regularity switch
        {
            Daily(var dailyRegularity) => ConvertDaily(dailyRegularity),
            Monthly(var monthlyRegularity) => ConvertMonthly(monthlyRegularity),

            _ => throw new UnreachableException()
        };

        static HabitScheduleEntity ConvertDaily(DailyRegularity dailyRegularity)
        {
            return dailyRegularity switch
            {
                DaysOfTheWeek daysOfTheWeek => new HabitScheduleEntity()
                {
                    RepeatingCycleDays = 7,
                    IsAnyDay = false,
                    IsAllMachedDays = true,
                    DatesMatched = [],
                    RepeatingDatesToMatch = daysOfTheWeek.Extract(),
                    StartDate = GetStartOfTheWeekDate(DateTime.Now),
                },
                TimesPerWeek timesPerWeek => new HabitScheduleEntity()
                {
                    RepeatingCycleDays = 7,
                    IsAnyDay = true,
                    IsAllMachedDays = false,
                    DatesMatched = [],
                    RepeatingDatesToMatch = null,
                    StartDate = GetStartOfTheWeekDate(DateTime.Now),
                    CycleMachedDaysGoal = (int)timesPerWeek.Count,
                },

                _ => throw new UnreachableException()
            };
        }

        static HabitScheduleEntity ConvertMonthly(MonthlyRegularity monthlyRegularity)
        {
            return monthlyRegularity switch
            {
                TimesPerMonth timesPerMonth => new HabitScheduleEntity()
                {
                    // what to put here?????
                    RepeatingCycleDays = null,
                    IsAnyDay = true,
                    IsAllMachedDays = false,
                    
                },

                _ => throw new UnreachableException()
            };
        }
    }

    private static DateOnly GetStartOfTheWeekDate(DateTime anchor)
    {
        // Skipping to the start of the week. Is probably bad
        // because of Unix time
        while (anchor.DayOfWeek != DayOfWeek.Monday)
        {
            anchor.Subtract(TimeSpan.FromDays(1));
        }

        return DateOnly.FromDateTime(anchor);
    }

    public Result<Unit, string> DeleteHabit(int id)
    {
        throw new NotImplementedException();
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

file static class ResultExtensions
{
    public static Result<T, E> Where<T, E>(this Result<T, E> result, Func<T, bool> filter, E error)
    {
        if (result.TryUnwrap2(out var ok, out var e))
        {
            return filter(ok) ? Ok(ok) : Error(error);
        }
        else
        {
            return Error(e);
        }
    }
}
