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
        User user = new User { Path = new List<string>(), Highlits = new List<string>(),Stars=0,Role=0,IsPresent=false};
        string _pass;
        string _confPass;
        List<string> paths = new List<string>
        {
            "Mobile",
            "Limbaje",
            "AI",
            "IoT",
            "Azure",
            "Gaming"
        };

        List<string> hl = new List<string>
        {
            "MVP",
            "Alpha",
            "Beta",
            "Gold",
            "None"
        };

        public RegisterPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
            BindingContext = user;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            PathPicker.ItemsSource = paths;
            HighlightsPicker.ItemsSource = hl;
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
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
                user.Id = token;
                await UserProvider.CreateUserFirestore(user);
                await DisplayAlert("Ok", "User created", "OK");

                await Navigation.PopAsync();
            }
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void HighlightsPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker pick = sender as Picker;
            string selectedItem = (string)pick.SelectedItem ;
            if (selectedItem != "None")
                user.Highlits.Add(selectedItem);
        }

        private void PathPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker pick = sender as Picker;
            string selectedItem = (string)pick.SelectedItem;
            user.Path.Add(selectedItem);
        }
    }
}