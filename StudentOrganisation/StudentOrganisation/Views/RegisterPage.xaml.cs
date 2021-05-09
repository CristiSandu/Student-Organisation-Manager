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
                return;
            }


            string token = await auth.SingInWithEmailAndPassword(EmailEntry.Text, PasswordEntry.Text);
            if (token != string.Empty)
            {
                User user = new User();
                user.Name = FirstNameEntry.Text;
                user.SecondName = LasttNameEntry.Text;
                user.Email = EmailEntry.Text;
                user.Id = token;
                await FirestoreUser.CreateUserFirestore(user);
                await DisplayAlert("ok", "User created", "ok");
                Application.Current.MainPage = new NavigationPage(new TestLogin());
            }

        }
    }
}