namespace HabitTracker.Presentation
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddPage());
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddPage());
        }
    }
}
