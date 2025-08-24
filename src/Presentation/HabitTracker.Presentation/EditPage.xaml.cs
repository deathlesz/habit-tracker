namespace HabitTracker.Presentation;

using System.Collections.Generic;
using HabitTracker.Domain.Dto;
using HabitTracker.Presentation.ViewModel;
using JFomit.Functional;

public partial class EditPage : ContentPage, IQueryAttributable
{
    public EditPage(Habit habit)
    {
        InitializeComponent();
        BindingContext = new EditPageViewModel(habit);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("NewRegularity", out var regularity))
        {
            var edit = (EditPageViewModel)BindingContext;
            var reg = (Regularity)regularity;
            edit.HabitRegularityButton.SetValueByText("Regularity", Prelude.Some(reg));
        }
    }
}