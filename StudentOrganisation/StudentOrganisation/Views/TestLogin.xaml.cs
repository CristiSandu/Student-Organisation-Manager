﻿using StudentOrganisation.Services;
using StudentOrganisation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestLogin : ContentPage
    {
        LoginViewModel viewModel;
        IFirebaseAuthentication auth;

        public TestLogin()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
            BindingContext = viewModel = new LoginViewModel();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string token = await auth.LoginWithEmailAndPassword(viewModel.Username, viewModel.Password);
            if (token != string.Empty)
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
                ShowError();
            }
        }

        private async void ShowError()
        {
            await DisplayAlert("Authentication Failed", "Email or password are incorrect. Try again!", "OK");
        }
    }
}