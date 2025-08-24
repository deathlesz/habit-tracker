using HabitTracker.Domain.Dto;
using HabitTracker.Domain.Enums;

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
            await Navigation.PushModalAsync(new EditPage(new Habit(-1)
            {
                Color = Domain.Color.Cyan,
                Goal = new Goal("Goal", MeasurementUnit.Cal),
                Icon = Icon.Training,
                Kind = GoodnessKind.Positive,
                Name = "A bs habit",
                Regularity = new Daily(new TimesPerWeek(3)),
                Description = "A very looooooong description.",
                StartDate = new DateOnly(1970, 1, 1),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                Id = 0xFEFE,
                PartOfTheDay = PartOfTheDay.Morning,
                State = State.Complete,
                Reminder = null
            }));
        }
    }
}
