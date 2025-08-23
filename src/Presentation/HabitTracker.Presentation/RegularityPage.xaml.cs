using HabitTracker.Presentation.ViewModel;

namespace HabitTracker.Presentation;

public partial class RegularityPage : ContentPage
{
    public RegularityPage()
    {
        InitializeComponent();
        BindingContext = new RegularityPageViewModel();
    }
    private async void OnCancelClicked(object sender, EventArgs e) //return to home page
    {

    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {

    }

}