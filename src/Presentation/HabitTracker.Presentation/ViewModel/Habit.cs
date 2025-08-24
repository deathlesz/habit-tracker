using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Graphics;

namespace HabitTracker.Presentation.ViewModel
{
    public class Habit : INotifyPropertyChanged
    {
        private string _status = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ActionButtonText));
                    OnPropertyChanged(nameof(ActionButtonColor));
                    OnPropertyChanged(nameof(ActionCommand));
                }
            }
        }

        public Color StatusColor { get; set; } = Colors.Gray;
        public double ProgressPercentage { get; set; }
        public bool ShowProgressBar { get; set; }

        // Command for action
        public ICommand SkipCommand { get; set; }
        public ICommand UndoCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public Habit()
        {
            // init command
            SkipCommand = new Command(() => ExecuteAction("Skip"));
            UndoCommand = new Command(() => ExecuteAction("Undo"));
            ResetCommand = new Command(() => ExecuteAction("Reset"));
            EditCommand = new Command(() => ExecuteAction("Edit"));
            DeleteCommand = new Command(() => ExecuteAction("Delete"));
        }

        // dinamic status for button
        public string ActionButtonText
        {
            get
            {
                return Status switch
                {
                    "Active" => "⏭️",
                    "Completed" => "↩️",
                    "Skipped" => " 🔄",
                    _ => "⏭️"
                };
            }
        }

        public Color ActionButtonColor
        {
            get
            {
                return Status switch
                {
                    "Active" => Color.FromArgb("#4CAF50"),
                    "Completed" => Color.FromArgb("#FF9800"),
                    "Skipped" => Color.FromArgb("#2196F3"),
                    _ => Color.FromArgb("#4CAF50")
                };
            }
        }

        public ICommand ActionCommand
        {
            get
            {
                return Status switch
                {
                    "Active" => SkipCommand,
                    "Completed" => UndoCommand,
                    "Skipped" => ResetCommand,
                    _ => SkipCommand
                };
            }
        }

        private void ExecuteAction(string action)
        {
            Console.WriteLine($"Выполнено действие: {action} для привычки: {Name}");

            // logic status
            switch (action)
            {
                case "Skip":
                    Status = "Skipped";
                    StatusColor = Colors.Gray;
                    break;
                case "Undo":
                    Status = "Active";
                    StatusColor = Color.FromArgb("#3B82F6");
                    break;
                case "Reset":
                    Status = "Active";
                    StatusColor = Color.FromArgb("#3B82F6");
                    break;
                case "Delete":
                    // logic delete
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}