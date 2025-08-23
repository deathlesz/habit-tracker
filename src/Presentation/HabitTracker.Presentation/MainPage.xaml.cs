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
        
        private IServiceProvider _serviceProvider;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        /// <param name="androidNotificationService">
        /// The service responsible for scheduling repetitive notifications.
        /// </param>
        public MainPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// Handles the click event of the "Create Notification" button.
        /// Validates user input, creates a habit reminder, and stores the result.
        /// </summary>
        

        /// <summary>
        /// Handles the click event of the "Show Notification" button.
        /// If a valid habit reminder exists, displays it as an Android notification.
        /// </summary>
        

        private async void OnAddClicked(object sender, EventArgs e)
        {
            var AddPage = _serviceProvider.GetService<AddPage>();
            await Navigation.PushModalAsync(AddPage);

        }
    }
}
