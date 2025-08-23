using Android.Content;
using HabitTracker.Domain.Entities;
using HabitTracker.Infrastructure.Services.Notification;
using JFomit.Functional.Monads;
namespace HabitTracker.Presentation
{
    /// <summary>
    /// Represents the main page of the application where users can create and display notifications.
    /// </summary>
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
    }

}
