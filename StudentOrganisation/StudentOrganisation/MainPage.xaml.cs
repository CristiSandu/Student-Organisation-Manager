﻿using StudentOrganisation.Services;
using StudentOrganisation.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using StudentOrganisation.Services;

namespace StudentOrganisation
{
    public partial class MainPage : ContentPage
    {
        IFirebaseAuthentication auth;
        Models.User usr;
        string _url;
        string _IdUser;
        public MainPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        public MainPage( string id)
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
            _IdUser = id;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            usr = await UserProvider.GetFirestoreUser(await SecureStorage.GetAsync("isLogged"));
            _url = await FirebaseStorageProvider.GetProfilePictureUrl(usr);
            idUser.Text = _IdUser;
            testProfile.Source = _url;

        }

        private async void singOut_Clicked(object sender, EventArgs e)
        {
            var signedOut = auth.SignOut();
            if (signedOut)
            {
                SecureStorage.Remove("isLogged");
                ((App)Application.Current).MainPage = new NavigationPage(new TestLogin());
            }
        }
        private async void getUSER_Clicked(object sender, EventArgs e)
        {
           
            //idUser.Text = usr.Name;
            try
            {
                var oauthToken = await SecureStorage.GetAsync("isLogged");

                Models.User usr = await UserProvider.GetFirestoreUser(oauthToken);


                usr.Id = oauthToken;
                BindingContext = usr;
                idUser.Text = oauthToken;

            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
            
        }

        private async void addPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result_photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Pick a photo!"
                });
                var stream = await result_photo.OpenReadAsync();

                // testProfile.Source = ImageSource.FromStream(() => stream);
              //testProfile.Source = 
                    string url = await FirebaseStorageProvider.StoreProfilePictureUrl(stream, usr);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
    }
}
