namespace HabitTracker.Domain;

enum GoodnessKind
{
    Positive = 0,
    Negative,
}

enum Icon
{
    Default = 0,

    Training,
    Running,
    DrinkingWater
}

enum Color
{
    Black = 0,
    Red,
    Green,
    Yellow,
    Blue,
    Magenta,
    Cyan,
    White
}

record Habit
{
    public required GoodnessKind Kind { get; init; }
    public required string Name { get; init; }
    public required Icon Icon { get; init; }
    public required Color Color { get; init; }

    public required Regularity Regularity { get; init; }
}
