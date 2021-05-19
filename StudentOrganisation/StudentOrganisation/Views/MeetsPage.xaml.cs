using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentOrganisation.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using StudentOrganisation.Services;
using System.Collections.ObjectModel;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeetsPage : ContentPage
    {
        List<Models.MeetsModel> meets = new List<Models.MeetsModel> { new Models.MeetsModel
        {
            Title = "Meet 1",
            Description = "Description for meet 1",
            Date = DateTime.Now
        } , new Models.MeetsModel{
            Title = "Meet 2",
            Description = "Description for meet 2",
            Date = DateTime.Now
        }, new Models.MeetsModel{
            Title = "Meet 3",
            Description = "Description for meet 3",
            Date = DateTime.Now
        }};

        IFirebaseAuthentication auth;
        ObservableCollection<MeetsModel> _meetsList;

        public MeetsPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string role = await SecureStorage.GetAsync("Role");
            if (role == "2" || role == "3")
            {
                buttonMeets.IsVisible = true;
            }
            _meetsList = new ObservableCollection<MeetsModel>(await Services.MeetsProvider.GetAll(Int32.Parse(role));
            meetsList.ItemsSource = _meetsList;
        }
    }
}