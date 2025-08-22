using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Entities;
using HabitTracker.Domain.Entities.Regularity;
using HabitTracker.Domain.Enums;

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
    public HabitScheduleEntity Regularity { get; init; }

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
}