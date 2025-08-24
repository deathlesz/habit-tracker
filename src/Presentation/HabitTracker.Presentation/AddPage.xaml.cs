using Microsoft.Maui.Controls;
using HabitTracker.Presentation.ViewModel;

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
}
