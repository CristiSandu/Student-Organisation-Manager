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
        ObservableCollection<MeetsModel> _meetsList;

        public MeetsPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string role = await SecureStorage.GetAsync("Role");
            if (role == "2" || role == "3")
            {
                buttonMeets.IsVisible = true;
            }
            _meetsList = new ObservableCollection<MeetsModel>(await Services.MeetsProvider.GetAll(Int32.Parse(role)));
            meetsList.ItemsSource = _meetsList;
        }

        private async void meetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MeetsModel meets = e.CurrentSelection.FirstOrDefault() as MeetsModel;

            if (e.CurrentSelection != null)
            {
                await PopupNavigation.Instance.PushAsync(new ViewMeet //TODO: to be changed to ViewMeet
                {
                    BindingContext = meets
                });
            }
        }

        private async void AddMeetsBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMeet());
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            refreshView.IsRefreshing = false;
        }
    }
}