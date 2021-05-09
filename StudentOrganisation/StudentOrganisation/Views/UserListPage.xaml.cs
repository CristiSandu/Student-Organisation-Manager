﻿using StudentOrganisation.Models;
using StudentOrganisation.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserListPage : ContentPage
    {
        public UserListPage()
        {
            InitializeComponent();
            BindingContext = new UserListViewModel();
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            ((UserListViewModel)BindingContext).FilterByName(searchBar.Text);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }

        private void TouchEffect_TouchAction(object sender, TouchTracking.TouchActionEventArgs args)
        {
            
        }

    }
}