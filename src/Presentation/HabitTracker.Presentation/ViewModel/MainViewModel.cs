using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Habit> Habits { get; set; }
        public ColorChangingElement AddPageButton { get; init; }
        public ColorChangingElement StatPageButton { get; init; }

        public MainViewModel()
        {
            AddPageButton = new ColorChangingElement(ElementColorStyle.Default)
            {
                Command = new Command(async () => await SelectAddPageAsync())
            };
            StatPageButton = new ColorChangingElement(ElementColorStyle.Default)
            {
                Command = new Command(async () => await SelectStatPageAsync())
            };

            Habits = new ObservableCollection<Habit>();
            LoadHabits();
        }

        private void LoadHabits()
        {
            Habits.Add(new Habit
            {
                Name = "Tranning",
                Status = "2500.0 / 3000.0",
                StatusColor = Color.FromArgb("#3B82F6"),
                ProgressPercentage = 2500.0 / 3000.0,
                ShowProgressBar = true
            });

            Habits.Add(new Habit
            {
                Name = "Reading book",
                Status = "Completed",
                StatusColor = Color.FromArgb("#10B981"),
                ProgressPercentage = 3000.0 / 3000.0,
                ShowProgressBar = true
            });

            Habits.Add(new Habit
            {
                Name = "Learning english",
                Status = "Completed",
                StatusColor = Color.FromArgb("#10B981"),
                ProgressPercentage = 3000.0 / 3000.0,
                ShowProgressBar = true
            });

            Habits.Add(new Habit
            {
                Name = "Running",
                Status = "Skipped",
                StatusColor = Color.FromArgb("#6B7280"),
                ShowProgressBar = false
            });

            Habits.Add(new Habit
            {
                Name = "Drink water",
                Status = "Active",
                StatusColor = Color.FromArgb("#3B82F6"),
                ProgressPercentage = 1500.0 / 2000.0,
                ShowProgressBar = true
            });
        }

        private async Task SelectAddPageAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(AddPage)}");
        }

        private async Task SelectStatPageAsync()
        {
            await Shell.Current.GoToAsync($"{nameof(StatPage)}");
        }
    }
}