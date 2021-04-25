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
        public RegisterPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        private async void BirthDayDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                await DisplayAlert("Error!", "Password != ConfirmPassword", "OK");
                return;
            }

            string token = await auth.SingInWithEmailAndPassword(EmailEntry.Text, PasswordEntry.Text);
            if (token != string.Empty)
            {
                Application.Current.MainPage = new NavigationPage(new TestLogin());
            }

        }
    }
}