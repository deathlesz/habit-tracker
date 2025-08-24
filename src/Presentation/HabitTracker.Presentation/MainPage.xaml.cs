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
                Goal = new Goal("A bs goal", MeasurementUnit.Cal),
                Icon = Icon.Training,
                Kind = GoodnessKind.Negative,
                Name = "A bs habit",
                Regularity = new Monthly(new ConcreteDays([1, 2, 15, 30])),
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
