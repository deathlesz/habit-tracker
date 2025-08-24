using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;
using static JFomit.Functional.Prelude;

namespace HabitTracker.Presentation.ViewModel;

public partial class ColorChangingElement(ElementColorStyle style, string defaultValue = "", Option<string> storedValue = default) : ObservableObject
{
    public ICommand? Command { get; init; } = null;
    public ElementColorStyle Style { get; init; } = style;

    [ObservableProperty]
    private Color color = storedValue.Match(some: (_) => style.SetColor, none: () => style.DefaultColor);
    [ObservableProperty]
    private Color strokeColor = storedValue.Match(some: (_) => style.SetStrokeColor, none: () => style.DefaultStrokeColor);

    [ObservableProperty]
    private string value = defaultValue;
    public Option<string> StoredValue { get; private set; } = storedValue;

    partial void OnValueChanged(string value)
    {
        Color = string.IsNullOrWhiteSpace(value)
            ? Style.DefaultColor
            : Style.SetColor;

        StrokeColor = string.IsNullOrWhiteSpace(value)
            ? Style.DefaultStrokeColor
            : Style.SetStrokeColor;
    }

    public void SetValue(string prefix, string actual)
    {
        Value = $"{prefix} {actual}";
        StoredValue = string.IsNullOrWhiteSpace(actual) ? None : Some(actual);
    }
}
