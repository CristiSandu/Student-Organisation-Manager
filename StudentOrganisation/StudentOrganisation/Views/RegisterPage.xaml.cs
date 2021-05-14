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
using StudentOrganisation.Models;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        IFirebaseAuthentication auth;
        User user = new User();
        string _pass;
        string _confPass;
        public RegisterPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
            BindingContext = user;
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
                return;
            }


            string token = await auth.SingInWithEmailAndPassword(EmailEntry.Text, PasswordEntry.Text);
            if (token != string.Empty)
            {
                await UserProvider.CreateUserFirestore(user);
                await DisplayAlert("ok", "User created", "ok");

                await Shell.Current.GoToAsync("..");
            }

        }
    }
}