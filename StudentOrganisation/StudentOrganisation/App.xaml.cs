using StudentOrganisation.Services;
using StudentOrganisation.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
                MainPage = new TestLogin();
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
