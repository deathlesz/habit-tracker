using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Entities;
using HabitTracker.Domain.Entities.Regularity;
using HabitTracker.Domain.Enums;
using HabitTracker.Presentation.ViewModel;

namespace HabitTracker.Infrastructure.Services;

public class HabitParser
{
    public Goal CreateGoalInfo(string name, string unitString)
    {
        if (!Enum.TryParse<MeasurementUnit>(unitString, true, out var unit))
        {
            throw new ArgumentException("Invalid measurement unit");
        }
        return new Goal(name, unit);
    }

    public GoodnessKind ParseKind(string value)
    {
        return value switch
        {
            "Positive" => GoodnessKind.Positive,
            "Negative" => GoodnessKind.Negative,
            _ => throw new ArgumentException($"Invalid value GoodnessKind {value}"),
        };
    }

    public Icon ParseIcon(string value)
    {
        return value switch
        {
            "Bottle" => Icon.DrinkingWater,
            "GYM" =>Icon.Training ,
            "Run" => Icon.Running,
            _ => Icon.Default
        };
    }
    public Domain.Color ParseColor(string value)
    {
        if (Enum.TryParse<Domain.Color>(value, true, out var color))
        {
            return color;
        }
        throw new ArgumentException($"Invalid value Color {value}");
    }

    public PartOfTheDay? ParsePartOfTheDay(string value)
    {
        return value switch
        {
            "Morning" =>  PartOfTheDay.Morning,
            "Afternoon" => PartOfTheDay.Afternoon,
            "Night" => PartOfTheDay.Night,
            _ => throw new ArgumentException($"Invalid value PartOfTheDay {value}"),
        };
    }

    public Regularity ParseRegularity(RegularityDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));

        if (dto.IsDaily)
        {
            DailyRegularity daily;
            if (dto.DailyEveryDay)
            {
                daily = new DaysOfTheWeek(
                [
                    DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                    DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday,
                    DayOfWeek.Sunday
                ]);
            }
            else
            {
                daily = new TimesPerWeek((uint)dto.DailyDaysPerWeek);
            }

            return new Daily(daily);
        }
        if (dto.IsMonthly)
        {
            MonthlyRegularity monthly;
            if (dto.MonthlyDays.Any(d => d))
            {
                var days = dto.MonthlyDays
                    .Select((flag, idx) => (flag, idx))
                    .Where(x => x.flag)
                    .Select(x => x.idx + 1)
                    .ToArray();
                monthly = new ConcreteDays(days);
            }
            else
            {
                monthly = new TimesPerMonth((uint)dto.MonthlyDaysPerMonth);
            }

            return new Monthly(monthly);
        }
        if (dto.IsInterval)
        {
            if (!uint.TryParse(dto.IntervalDays, out var count) || count == 0)
                throw new ArgumentException("Invalid interval days");

            return new EveryNDays(count);
        }

        throw new ArgumentException("Invalid RegularityDto: no regularity type selected");
    }
}