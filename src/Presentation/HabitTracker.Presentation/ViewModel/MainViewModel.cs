using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation
{
    public class MainViewModel
    {
        public ObservableCollection<Habit> Habits { get; set; }

        public MainViewModel()
        {
            Habits = new ObservableCollection<Habit>();
            LoadHabits();
        }

        private void LoadHabits()
        {
            Habits.Add(new Habit
            {
                Name = "Утренняя зарядка",
                Progress = "2500/3000",
                Status = "In Progress",
                StatusColor = Color.FromArgb("#3B82F6"),
                ProgressPercentage = 2500.0 / 3000.0,
                ShowProgressBar = true
            });

            Habits.Add(new Habit
            {
                Name = "Чтение книги",
                Progress = "",
                Status = "Completed",
                StatusColor = Color.FromArgb("#10B981"),
                ProgressPercentage = 3000.0 / 3000.0,
                ShowProgressBar = true
            });

            Habits.Add(new Habit
            {
                Name = "Чтение книги",
                Progress = "",
                Status = "Completed",
                StatusColor = Color.FromArgb("#10B981"),
                ShowProgressBar = true
            });

            Habits.Add(new Habit
            {
                Name = "Чтение книги",
                Progress = "Complete",
                Status = "Completed",
                StatusColor = Color.FromArgb("#10B981"),
                ShowProgressBar = false
            });

            Habits.Add(new Habit
            {
                Name = "Чтение книги",
                Progress = "Complete",
                Status = "Completed",
                StatusColor = Color.FromArgb("#10B981"),
                ShowProgressBar = false
            });

            Habits.Add(new Habit
            {
                Name = "Медитация",
                Progress = "Skipped",
                Status = "Skipped",
                StatusColor = Color.FromArgb("#6B7280"),
                ShowProgressBar = false
            });

            Habits.Add(new Habit
            {
                Name = "Running",
                Progress = "Complete",
                Status = "Completed",
                StatusColor = Color.FromArgb("#10B981"),
                ShowProgressBar = false
            });
        }
    }

    public class Habit
    {
        public string Name { get; set; } = string.Empty;
        public string Progress { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public Color StatusColor { get; set; } = Colors.Gray;
        public double ProgressPercentage { get; set; }
        public bool ShowProgressBar { get; set; }
    }
}