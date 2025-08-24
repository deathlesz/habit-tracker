namespace HabitTracker.Presentation.ViewModel;

public class ReminderResult
{
    public string Message { get; set; }
    
    public DateOnly StartDate { get; set; }
    
    public int CyclePatternLength { get; set; }
    
    public ICollection<int> DaysToNotify { get; set; }
    
    public int? CyclesToRun { get; set; }
}