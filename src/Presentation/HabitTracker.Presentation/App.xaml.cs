namespace HabitTracker.Presentation
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            MainPage = serviceProvider.GetRequiredService<MainPage>();
        }
    }
}
