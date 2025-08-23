using System.Collections.ObjectModel;
using HabitTracker.Presentation.ViewModel;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation;
        public class MainViewModel
        {
            public ObservableCollection<Habit> Habits { get; set; }
            public ColorChangingElement AddPageButton { get; init; }
            public MainViewModel()
            {
                AddPageButton = new(ElementColorStyle.Default)
                {
                    Command = new Command(async () => await SelectAddPageAsync())
                };
                Habits = new ObservableCollection<Habit>();
                LoadHabits();
            }

            private void LoadHabits()
            {
                Habits.Add(new Habit
                {
                    Name = "Утренняя зарядка",
                    Progress = "2500/3000 MI",
                    StatusColor = Color.FromArgb("#3B82F6"),
                    ProgressPercentage = 2500.0 / 3000.0,
                    ShowProgressBar = true
                });

                Habits.Add(new Habit
                {
                    Name = "Чтение книги",
                    Progress = "Completed",
                    StatusColor = Color.FromArgb("#10B981"),
                    ProgressPercentage = 3000.0 / 3000.0,
                    ShowProgressBar = true
                });

                Habits.Add(new Habit
                {
                    Name = "Чтение книги",
                    Progress = "Completed",
                    StatusColor = Color.FromArgb("#10B981"),
                    ProgressPercentage = 3000.0 / 3000.0,
                    ShowProgressBar = true
                });

                Habits.Add(new Habit
                {
                    Name = "Чтение книги",
                    Progress = "Completed",
                    StatusColor = Color.FromArgb("#10B981"),
                    ProgressPercentage = 3000.0 / 3000.0,
                    ShowProgressBar = true
                });

                Habits.Add(new Habit
                {
                    Name = "Чтение книги",
                    Progress = "Completed",
                    StatusColor = Color.FromArgb("#10B981"),
                    ProgressPercentage = 3000.0 / 3000.0,
                    ShowProgressBar = true
                });

                Habits.Add(new Habit
                {
                    Name = "Медитация",
                    Progress = "Skipped",
                    StatusColor = Color.FromArgb("#6B7280"),
                    ShowProgressBar = false
                });

                Habits.Add(new Habit
                {
                    Name = "Чтение книги",
                    Progress = "Completed",
                    StatusColor = Color.FromArgb("#10B981"),
                    ProgressPercentage = 3000.0 / 3000.0,
                    ShowProgressBar = true
                });
            }
            private async Task SelectAddPageAsync() => await Shell.Current.GoToAsync($"{nameof(AddPage)}");
        }

        public class Habit
        {
            public string Name { get; set; } = string.Empty;
            public string Progress { get; set; } = string.Empty;
            public Color StatusColor { get; set; } = Colors.Gray;
            public double ProgressPercentage { get; set; }
            public bool ShowProgressBar { get; set; }
        }