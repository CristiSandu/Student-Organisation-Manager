using StudentOrganisation.Models;
using StudentOrganisation.Services;
using StudentOrganisation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserListPage : ContentPage
    {
        UserListViewModel model;
        public UserListPage()
        {
            InitializeComponent();
            BindingContext = new UserListViewModel();
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            ((UserListViewModel)BindingContext).FilterByName(searchBar.Text);
        }
        protected async override void OnAppearing()
        {
            List<User> userList = (await UserProvider.GetFirestoreAllUser());
            if (model == null)
            {
                model = ((UserListViewModel)BindingContext);

                await model.InitColors();
                model.source = userList.Select(user => UserListItem.FromUser(user)).ToList();
                model.Users = new ObservableCollection<UserListItem>(model.source);
                foreach (var user in model.Users)
                {
                    user.PageModel = model;
                }
            }
            collectionView.ItemsSource = model.Users;
            base.OnAppearing();
        }
        private void TouchEffect_TouchAction(object sender, TouchTracking.TouchActionEventArgs args)
        {
            
        }

        private async void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserListItem user = e.CurrentSelection.FirstOrDefault() as UserListItem;
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new MainPage(user.Id));
            }
        }
    }
}