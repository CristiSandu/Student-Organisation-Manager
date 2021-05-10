using StudentOrganisation.Models;
using StudentOrganisation.ViewModels;
using System;
using System.Collections.Generic;
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
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            ((UserListViewModel)BindingContext).FilterByName(searchBar.Text);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            List<User> userList = (await FirestoreUser.GetFirestoreAllUser());

            ((UserListViewModel)BindingContext).source = userList.Select(user => UserListItem.FromUser(user)).ToList();
            ((UserListViewModel)BindingContext).Users = new ObservableCollection<UserListItem>(((UserListViewModel)BindingContext).source);

            collectionView.ItemsSource = ((UserListViewModel)BindingContext).Users;
        }

        private void TouchEffect_TouchAction(object sender, TouchTracking.TouchActionEventArgs args)
        {
            
        }

    }
}