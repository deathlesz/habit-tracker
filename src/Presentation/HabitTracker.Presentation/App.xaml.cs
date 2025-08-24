namespace HabitTracker.Presentation
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            try
            {
                MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                MainPage = new ContentPage
                {
                    Content = new Label { Text = $"Ошибка: {ex.Message}" }
                };
            }
        }
    }
}
