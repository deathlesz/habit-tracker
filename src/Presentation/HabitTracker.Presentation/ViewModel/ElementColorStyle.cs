namespace HabitTracker.Presentation.ViewModel;

public record ElementColorStyle(Color DefaultColor, Color SetColor, Color DefaultStrokeColor, Color SetStrokeColor)
{
    public static ElementColorStyle Default => new(
        Color.FromArgb("#7B9EE0"),
        Color.FromArgb("#9ACD32"),
        Color.FromArgb("#393A9A"),
        Color.FromArgb("#78AA60")
    );
}
