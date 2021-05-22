  using StudentOrganisation.Services;
using StudentOrganisation.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
namespace StudentOrganisation
{
    public partial class App : Application
    {
        IFirebaseAuthentication auth;
        public static int _role = 0;

        public App()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
            if (auth.IsSignIn())
            {
                MainPage = new MainPage();
            }
            else
            {
                MainPage = new NavigationPage(new TestLogin());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
