namespace HabitTracker.Presentation
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            try
            {
                MainPage = serviceProvider.GetService<MainPage>();
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
