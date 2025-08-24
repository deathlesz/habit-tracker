﻿using Microsoft.Maui.Controls;
using HabitTracker.Presentation.ViewModel;

namespace HabitTracker.Presentation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel.MainViewModel();
        }
    }
}