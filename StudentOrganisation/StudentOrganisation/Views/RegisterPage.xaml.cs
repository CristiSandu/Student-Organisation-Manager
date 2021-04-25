using StudentOrganisation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StudentOrganisation.ViewModels;
using StudentOrganisation.Services;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        IFirebaseAuthentication auth;
        string _pass;
        string _confPass;
        public RegisterPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        private async void BirthDayDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
          
        }

        private async void AddUser_Clicked(object sender, EventArgs e)
        {
            _pass = PasswordEntry.Text;
            _confPass = ConfirmPasswordEntry.Text;
            if (_pass != _confPass)
            {
                await DisplayAlert("Error!", "Password != ConfirmPassword", "OK");

            }
            else
            {

                string token = await auth.SingInWithEmailAndPassword(EmailEntry.Text, PasswordEntry.Text);
                if (token != string.Empty)
                {
                    Application.Current.MainPage = new NavigationPage(new TestLogin());
                }
            }
        }
    }
}