﻿using StudentOrganisation.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using StudentOrganisation.Views;

namespace StudentOrganisation
{
    public partial class MainPage : ContentPage
    {
        IFirebaseAuthentication auth;
        Models.User usr;
        string _IdUser;
        bool IsCurrentUser = false;
        public MainPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        public MainPage(string id)
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
            _IdUser = id;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            getUser();
        }

        private async void singOut_Clicked(object sender, EventArgs e)
        {
            var signedOut = auth.SignOut();
            if (signedOut)
            {
                SecureStorage.Remove("isLogged");
                ((App)Application.Current).MainPage = new NavigationPage(new Views.TestLogin());
            }
        }
        private async void getUser()
        {
            string oauthToken;
            string currentUserToken;

            try
            {
                currentUserToken = await SecureStorage.GetAsync("isLogged");
                oauthToken = _IdUser == null ? currentUserToken : _IdUser;
                usr = await UserProvider.GetFirestoreUser(oauthToken);

                IsCurrentUser = oauthToken == currentUserToken;
                IsPresentSwitch.IsVisible = IsCurrentUser;
                chaneStars.IsEnabled = !IsCurrentUser;

                usr.Id = oauthToken;
                BindingContext = usr;
                NameLabel.Text = usr.Name + " " + usr.SecondName;
                testProfile.Source = await FirebaseStorageProvider.GetProfilePictureUrl(usr);
                PathCollectionView.ItemsSource = usr.Path;
                HighlightsCollectionView.ItemsSource = usr.Highlits;

                IsPresentSwitch.IsToggled = usr.IsPresent;
                IsPresentLabel.Text = usr.IsPresent ? "Present" : "Not Present";
                IsPresentLabelBackground.BackgroundColor = usr.IsPresent ? Color.FromHex("#7FBA00") : Color.FromHex("#F25022");


            }
            catch (Exception ex)
            {

            }

        }

        private async void Photo_Clicked(object sender, EventArgs e)
        {
            if (!IsCurrentUser)
                return;

            try
            {
                var result_photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Pick a photo!"
                });
                var stream = await result_photo.OpenReadAsync();

                string url = await FirebaseStorageProvider.StoreProfilePictureUrl(stream, usr);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Switch sw = sender as Switch;
            usr.IsPresent = sw.IsToggled;

            IsPresentLabel.Text = usr.IsPresent ? "Present" : "Not Present";
            IsPresentLabelBackground.BackgroundColor = usr.IsPresent ? Color.FromHex("#7FBA00") : Color.FromHex("#F25022");

            await UserProvider.UpdateFirestoreUser(usr);
        }

        private async void chaneStars_Clicked(object sender, EventArgs e)
        {
            try
            {
                string stars = await DisplayPromptAsync("Change Stars", "How manny you want to put ?", initialValue: "1", maxLength: 2, keyboard: Keyboard.Numeric);
                int nrStars = int.Parse(stars) + usr.Stars;

                await UserProvider.AddStarForUser(usr, int.Parse(stars));
                starsNumber.Text = nrStars.ToString();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
