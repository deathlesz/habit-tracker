namespace HabitTracker.Presentation;
using HabitTracker.Presentation.ViewModel;

public partial class EditPage : ContentPage
{
	public EditPage()
	{
		InitializeComponent();
        BindingContext = new EditPageViewModel();
    }
}