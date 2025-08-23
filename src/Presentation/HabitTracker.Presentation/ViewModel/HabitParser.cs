using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Entities;
using HabitTracker.Domain.Entities.Regularity;
using HabitTracker.Domain.Enums;
using HabitTracker.Presentation.ViewModel;
using JFomit.Functional.Monads;

namespace HabitTracker.Infrastructure.Services;

public class HabitParser
{
    public Result<Goal, string> CreateGoalInfo(string name, string unitString)
    {
        if (!Enum.TryParse<MeasurementUnit>(unitString, true, out var unit))
        {
            return Result<Goal, string>.Fail("Invalid measurement unit");
        }
        return Result<Goal, string>.Ok(new Goal(name, unit));
    }

    public Result<GoodnessKind, string> ParseKind(string value)
    {
        return value switch
        {
            "Positive" => Result<GoodnessKind, string>.Ok(GoodnessKind.Positive),
            "Negative" => Result<GoodnessKind, string>.Ok(GoodnessKind.Negative),
            _ => Result<GoodnessKind, string>.Fail($"Invalid value GoodnessKind {value}"),
        };
    }

    public Result<Icon, string> ParseIcon(string value)
    {
        return value switch
        {
            "Bottle" => Result<Icon, string>.Ok(Icon.DrinkingWater),
            "GYM" => Result<Icon, string>.Ok(Icon.Training) ,
            "Run" => Result<Icon, string>.Ok(Icon.Running),
            _ => Result<Icon, string>.Ok(Icon.Default)
        };
    }
    public Result<Domain.Color, string> ParseColor(string value)
    {
        if (Enum.TryParse<Domain.Color>(value, true, out var color))
        {
            return Result<Domain.Color, string>.Ok(color);
        }
        return Result<Domain.Color, string>.Fail($"Invalid value Color {value}");
    }

    public Result<PartOfTheDay, string> ParsePartOfTheDay(string value)
    {
        return value switch
        {
            "Morning" => Result<PartOfTheDay, string>.Ok(PartOfTheDay.Morning),
            "Afternoon" => Result<PartOfTheDay, string>.Ok(PartOfTheDay.Afternoon),
            "Night" => Result<PartOfTheDay, string>.Ok(PartOfTheDay.Night),
            _ => Result<PartOfTheDay, string>.Fail($"Invalid value PartOfTheDay {value}"),
        };
    }

    public Result<Regularity, string> ParseRegularity(RegularityDto dto)
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

            return Result<Regularity, string>.Ok(new Daily(daily));
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

            return Result<Regularity, string>.Ok(new Monthly(monthly));
        }
        if (dto.IsInterval)
        {
            if (!uint.TryParse(dto.IntervalDays, out var count) || count == 0)
                throw new ArgumentException("Invalid interval days");

            return Result<Regularity, string>.Ok(new EveryNDays(count));
        }

        return Result<Regularity, string>.Fail("Invalid RegularityDto: no regularity type selected");
    }
}