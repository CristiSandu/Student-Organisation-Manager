using StudentOrganisation.Services;
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            idUser.Text = _IdUser;
        }

        private async void singOut_Clicked(object sender, EventArgs e)
        {
            var signedOut = auth.SignOut();
            if (signedOut)
            {
                SecureStorage.Remove("isLogged");
                await Shell.Current.GoToAsync("///login");
            }
        }
        private async void getUSER_Clicked(object sender, EventArgs e)
        {
           
            //idUser.Text = usr.Name;
            try
            {
                var oauthToken = await SecureStorage.GetAsync("isLogged");
                Models.User usr = await FirestoreUser.GetFirestoreUser(oauthToken);

                usr.Id = oauthToken;
                BindingContext = usr;
                idUser.Text = oauthToken;

            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
            
        }
    }
}
