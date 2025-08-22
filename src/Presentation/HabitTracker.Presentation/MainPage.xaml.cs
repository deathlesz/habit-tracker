﻿using Microsoft.Maui.Controls;

namespace HabitTracker.Presentation
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}