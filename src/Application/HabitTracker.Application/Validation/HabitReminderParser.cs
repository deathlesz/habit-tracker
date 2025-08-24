using System.Diagnostics;
using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Entities.Regularity;
using HabitTracker.Domain.Enums;

namespace HabitTracker.Application.Validation;

static class HabitReminderParser
{
    public static HabitScheduleEntity ConvertRegularity(Regularity regularity, Habit habit) => regularity switch
    {
        Daily(var daily) => ConvertDaily(daily, habit),
        Monthly(var monthly) => ConvertMonthly(monthly, habit),
        EveryNDays(var count) => ConvertNDays(count, habit),

        _ => throw new UnreachableException(),
    };

    private static HabitScheduleEntity ConvertNDays(uint count, Habit habit) => new()
    {
        HabitRegularityType = HabitRegularityType.Daily, // basically unset
        DatesMatched = [], // none so far
        IsAllMachedDays = false,
        IsAnyDay = true,
        StartDate = habit.StartDate,
        RepeatingCycleDays = (int)count, // will not overflow, since habit is valid
        DaysMatchedInCycle = 0, // none so far
        RepeatingDatesToMatch = null, // IsAnyDay == true
        CycleMachedDaysGoal = 1, // only 1 is required

        Id = -1 // set by db
    };

    private static HabitScheduleEntity ConvertMonthly(MonthlyRegularity monthly, Habit habit)
    {
        return monthly switch
        {
            TimesPerMonth perMonth => ConvertTimesPerMonth(perMonth, habit),
            ConcreteDays days => ConvertConcreteDays(days, habit),

            _ => throw new UnreachableException(),
        };

        static HabitScheduleEntity ConvertConcreteDays(ConcreteDays days, Habit habit)
        {
            var daysOffsets = days.UnpackDays().Select(m => m - 1).ToList();

            return new()
            {
                HabitRegularityType = HabitRegularityType.Monthly, // per month
                DatesMatched = [], // none so far
                IsAllMachedDays = true,
                IsAnyDay = false,
                StartDate = habit.StartDate,
                RepeatingCycleDays = 0, // is unset
                DaysMatchedInCycle = 0, // none so far
                RepeatingDatesToMatch = daysOffsets, // well, they are set here
                CycleMachedDaysGoal = daysOffsets.Count, // here as well

                Id = -1
            };
        }
        static HabitScheduleEntity ConvertTimesPerMonth(TimesPerMonth perMonth, Habit habit)
        {
            return new()
            {
                HabitRegularityType = HabitRegularityType.Monthly, // per month
                DatesMatched = [], // none so far
                IsAllMachedDays = false,
                IsAnyDay = true,
                StartDate = habit.StartDate,
                RepeatingCycleDays = 0, // is unset
                DaysMatchedInCycle = 0, // none so far
                RepeatingDatesToMatch = null, // IsAnyDay == true
                CycleMachedDaysGoal = (int)perMonth.Count, // will not overflow

                Id = -1
            };
        }
    }

    private static HabitScheduleEntity ConvertDaily(DailyRegularity daily, Habit habit)
    {
        return daily switch
        {
            TimesPerWeek timesPerWeek => ConvertTimesPerWeek(timesPerWeek, habit),
            DaysOfTheWeek daysOfTheWeek => ConvertDaysOfTheWeek(daysOfTheWeek, habit),

            _ => throw new UnreachableException()
        };

        static HabitScheduleEntity ConvertTimesPerWeek(TimesPerWeek timesPerWeek, Habit habit)
        {
            return new()
            {
                HabitRegularityType = HabitRegularityType.Weekly, // per week
                DatesMatched = [], // none so far
                IsAllMachedDays = false,
                IsAnyDay = true,
                StartDate = habit.StartDate,
                RepeatingCycleDays = 7, // a week
                DaysMatchedInCycle = 0, // none so far
                RepeatingDatesToMatch = null, // IsAnyDay == true
                CycleMachedDaysGoal = (int)timesPerWeek.Count, // will not overflow

                Id = -1
            };
        }
        static HabitScheduleEntity ConvertDaysOfTheWeek(DaysOfTheWeek daysOfTheWeek, Habit habit)
        {
            var days = daysOfTheWeek.UnpackDays();

            return new()
            {
                HabitRegularityType = HabitRegularityType.Weekly, // per week
                DatesMatched = [], // none so far
                IsAllMachedDays = true,
                IsAnyDay = false,
                StartDate = habit.StartDate,
                RepeatingCycleDays = 7, // a week
                DaysMatchedInCycle = 0, // none so far
                RepeatingDatesToMatch = days, // well, we have them, we set them
                CycleMachedDaysGoal = days.Count, // here as well

                Id = -1
            };
        }
    }
}
