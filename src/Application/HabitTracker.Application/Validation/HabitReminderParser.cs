using System.Diagnostics;
using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Entities.Regularity;
using HabitTracker.Domain.Enums;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

namespace HabitTracker.Application.Validation;

static class HabitReminderParser
{
    public static HabitScheduleEntity ToEntity(this Regularity regularity, Habit habit) => ConvertRegularity(regularity, habit);
    public static Regularity ToDto(this HabitScheduleEntity habitSchedule)
    {
        switch (habitSchedule.HabitRegularityType)
        {
            case HabitRegularityType.Weekly:
                if (habitSchedule.IsAnyDay)
                {
                    // times per week
                    return new Daily(new TimesPerWeek((uint)habitSchedule.CycleMachedDaysGoal));
                }
                else
                {
                    // week days
                    var days = habitSchedule.RepeatingDatesToMatch!.Select(ConvertWeekDayToEnum).ToArray();
                    return new Daily(new DaysOfTheWeek(days));
                }
            case HabitRegularityType.Monthly:
                if (habitSchedule.IsAnyDay)
                {
                    // times per month
                    // cast is valid provided that habitSchedule is valid
                    return new Monthly(new TimesPerMonth((uint)habitSchedule.CycleMachedDaysGoal));
                }
                else
                {
                    // concrete days
                    var days = habitSchedule.RepeatingDatesToMatch!.Select(d => d + 1).ToArray(); // from offset to numbers
                    return new Monthly(new ConcreteDays(days));
                }
            default: // N days
                return new EveryNDays((uint)habitSchedule.RepeatingCycleDays); // is valid provided that habitSchedule is valid
        }

        static DayOfWeek ConvertWeekDayToEnum(int day) => day switch
        {
            0 => DayOfWeek.Monday,
            1 => DayOfWeek.Tuesday,
            2 => DayOfWeek.Wednesday,
            3 => DayOfWeek.Thursday,
            4 => DayOfWeek.Friday,
            5 => DayOfWeek.Saturday,
            6 => DayOfWeek.Sunday,

            _ => throw new UnreachableException(),
        };
    }


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
        DaysMachedInCycle = 0, // none so far
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
                DaysMachedInCycle = 0, // none so far
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
                DaysMachedInCycle = 0, // none so far
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
                DaysMachedInCycle = 0, // none so far
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
                DaysMachedInCycle = 0, // none so far
                RepeatingDatesToMatch = days, // well, we have them, we set them
                CycleMachedDaysGoal = days.Count, // here as well

                Id = -1
            };
        }
    }
}