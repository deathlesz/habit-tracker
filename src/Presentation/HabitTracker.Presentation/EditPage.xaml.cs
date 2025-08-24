namespace HabitTracker.Presentation;

using HabitTracker.Domain.Dto;
using HabitTracker.Presentation.ViewModel;

public partial class EditPage : ContentPage
{
    public EditPage(Habit habit)
    {
        InitializeComponent();
        BindingContext = new EditPageViewModel(habit);
    }
}