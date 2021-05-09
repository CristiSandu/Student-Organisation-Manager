using StudentOrganisation.Services;
using StudentOrganisation.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
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

        private void singOut_Clicked(object sender, EventArgs e)
        {
            var signedOut = auth.SignOut();
            if (signedOut)
            {
                Application.Current.MainPage = new TestLogin();
            }
        }

        private async void getUSER_Clicked(object sender, EventArgs e)
        {
            Models.User usr = await FirestoreUser.GetFirestoreUser(_IdUser);
            //idUser.Text = usr.Name;
            BindingContext = usr;
        }
    }
}
