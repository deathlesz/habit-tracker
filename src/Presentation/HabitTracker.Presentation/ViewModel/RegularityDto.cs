namespace HabitTracker.Presentation.ViewModel;

public class RegularityDto
{
    public bool IsDaily { get; set; }
    public bool IsMonthly { get; set; }
    public bool IsInterval { get; set; }
    public bool DailyEveryDay { get; set; }
    public int DailyDaysPerWeek { get; set; }
    public int MonthlyDaysPerMonth { get; set; }
    public string? IntervalDays { get; set; }
}