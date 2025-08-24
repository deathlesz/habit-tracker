using HabitTracker.Domain.Dto;
using HabitTracker.Presentation.ViewModel;
using JFomit.Functional;
using JFomit.Functional.Extensions;
using JFomit.Functional.Monads;

namespace HabitTracker.Presentation;

public partial class RegularityPage : ContentPage, IQueryAttributable
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

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Regularity", out var regularity))
        {
            ((RegularityPageViewModel)BindingContext).Regularity = Prelude.Some((Regularity)regularity);
        }
    }
}