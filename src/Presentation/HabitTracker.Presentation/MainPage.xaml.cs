using HabitTracker.Application.Interfaces.Repositories;

namespace HabitTracker.Presentation
{
	public partial class MainPage : ContentPage
	{
		int count = 0;
		IHabitRepository habitRepository;
		IHabitReminderRepository reminderRepository;

		public MainPage(IHabitReminderRepository rRep, IHabitRepository hRep)
		{
			InitializeComponent();
			reminderRepository = rRep;
			habitRepository = hRep;
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";
			habitRepository.GetAll();

			SemanticScreenReader.Announce(CounterBtn.Text);
		}
	}

}
