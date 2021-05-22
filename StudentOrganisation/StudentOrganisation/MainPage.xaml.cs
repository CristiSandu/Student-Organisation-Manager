using StudentOrganisation.Services;
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
            getUser();
            usr = await UserProvider.GetFirestoreUser(await SecureStorage.GetAsync("isLogged"));
            _url = await FirebaseStorageProvider.GetProfilePictureUrl(usr);
            testProfile.Source = _url;

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

            try
            {
                var oauthToken = await SecureStorage.GetAsync("isLogged");

                Models.User usr = await UserProvider.GetFirestoreUser(oauthToken);

                usr.Id = oauthToken;
                BindingContext = usr;
                NameLabel.Text = usr.Name + " " + usr.SecondName;
                PathCollectionView.ItemsSource = usr.Path;
                HighlightsCollectionView.ItemsSource = usr.Highlits;

            }
            catch (Exception ex)
            {
                
            }
            
        }

        private async void Photo_Clicked(object sender, EventArgs e)
        {
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
    }
}
