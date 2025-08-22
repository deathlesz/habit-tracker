namespace HabitTracker.Presentation
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
            Routing.RegisterRoute(nameof(EditPage), typeof(EditPage));
            Routing.RegisterRoute(nameof(RegularityPage), typeof(RegularityPage));
            Routing.RegisterRoute(nameof(ReminderPage), typeof(ReminderPage));
        }
    }
}
