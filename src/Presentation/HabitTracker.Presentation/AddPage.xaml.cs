using Microsoft.Maui.Controls;
using HabitTracker.Presentation.ViewModel;

namespace HabitTracker.Presentation;

[QueryProperty(nameof(ItemId), "itemId")]
[QueryProperty(nameof(RegularityDataJson), "RegularityData")]
public partial class AddPage : ContentPage
{
    public string RegularityDataJson 
    {
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                var dto = System.Text.Json.JsonSerializer.Deserialize<RegularityDto>(value);
                (BindingContext as AddPageViewModel)?.SetRegularity(dto);
            }
        }
    } 
    public string ItemId { get; set; } // must be public set

    public AddPage()
    {
        InitializeComponent();
        BindingContext = new AddPageViewModel(new DummyPresentation());
    }

    // OnClicked methods
    // I dunno how or why they work, but when deleted they cause compiler errors
    private async void OnCancelClicked(object sender, EventArgs e) // pop-up window & return to home page
    {

    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {

    }

    private async void OnTypeClicked(object sender, EventArgs e)
    {

    }

    private void OnHabitNameChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void OnHabitGoalChanged(object sender, TextChangedEventArgs e)
    {

    }

    private async void OnGoalClicked(object sender, EventArgs e)
    {

    }
    private async void OnStartDateClicked(object sender, EventArgs e)
    {

    }
    private async void OnEndDateClicked(object sender, EventArgs e)
    {

    }
    private async void OnReminderClicked(object sender, EventArgs e)
    {

    }
}
