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
        public UserListPage()
        {
            InitializeComponent();
            BindingContext = new UserListViewModel();
            Debug.WriteLine("After new UserListViewModel();");
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            ((UserListViewModel)BindingContext).FilterByName(searchBar.Text);
        }
        protected async override void OnAppearing()
        {
            List<User> userList = (await UserProvider.GetFirestoreAllUser());

            ((UserListViewModel)BindingContext).source = userList.Select(user => UserListItem.FromUser(user)).ToList();
            ((UserListViewModel)BindingContext).Users = new ObservableCollection<UserListItem>(((UserListViewModel)BindingContext).source);
            
            collectionView.ItemsSource = ((UserListViewModel)BindingContext).Users;
            base.OnAppearing();
        }


        private void TouchEffect_TouchAction(object sender, TouchTracking.TouchActionEventArgs args)
        {
            
        }

    }
}