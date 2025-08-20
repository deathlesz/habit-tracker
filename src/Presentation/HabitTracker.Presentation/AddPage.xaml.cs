using Microsoft.Maui.Controls;

namespace HabitTracker.Presentation;

[QueryProperty(nameof(ItemId), "itemId")]
public partial class AddPage : ContentPage
{
    public string ItemId { get; set; } // must be public set


    public AddPage()
	{
		InitializeComponent();
        BindingContext = new AddPageViewModel();
    }

    // OnClicked methods
    // TODO: pack the entered data in struct (or what suits here best) and save in into DB
    private async void OnCancelClicked(object sender, EventArgs e) // pop-up window & return to home page
    {
        if (BindingContext is AddPageViewModel vm)
            await vm.CancelCommand.ExecuteAsync(null);
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // TODO: 
    }

    //private async void OnTypeClicked(object sender, EventArgs e)
    //{
    //    if (BindingContext is AddPageViewModel vm)
    //        await vm.SelectHabitTypeCommand.ExecuteAsync(null);
    //}

    private void OnHabitNameChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is AddPageViewModel vm)
            vm.OnHabitNameChanged();
    }

    private async void OnGoalClicked(object sender, EventArgs e)
    {
        // TODO: 
    }
    private async void OnStartDateClicked(object sender, EventArgs e)
    {
        // TODO: 
    }
    private async void OnEndDateClicked(object sender, EventArgs e)
    {
        // TODO: 
    }
    private async void OnReminderClicked(object sender, EventArgs e)
    {
        // TODO: 
    }
}