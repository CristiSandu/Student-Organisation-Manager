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
        List<User> userList;
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
          
            if (model == null)
            {
                userList = (await UserProvider.GetFirestoreAllUser());
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

        private async void collectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count == 0)
                return;
            UserListItem user = e.CurrentSelection.FirstOrDefault() as UserListItem;
            if (e.CurrentSelection != null)
            {
                await Navigation.PushAsync(new MainPage(user.Id));
            }
             ((CollectionView)sender).SelectedItem = null;
        }

        private async void presentPerson_Clicked(object sender, EventArgs e)
        {
            ToolbarItem tbi = sender as ToolbarItem;
            if (tbi.Text == "Presents") {
                userList = (await UserProvider.GetAllPresent());
            }
            else if (tbi.Text == "Paths")
            {
                try
                {
                    string path = await DisplayActionSheet("Select Path:", "Cancel", null, "Mobile", "AI", "IoT", "Limbaje", "Gaming", "Azure");
                    userList = (await UserProvider.GetFirestoreUserFromPath(path));
                }catch(Exception ex)
                {

                }
            } else if (tbi.Text == "Roles")
            {
                try
                {
                    int path_int = 0;
                    string role = await DisplayActionSheet("Select Role:", "Cancel", null, "Junior", "Member", "Mentor", "Admin");
                    if (role == "Junior")
                    {
                        path_int = 0;
                    } else  if (role == "Member")
                    {
                        path_int = 1;
                    }
                    else if (role == "Mentor")
                    {
                        path_int = 2;
                    }
                    else if (role == "Admin")
                    {
                        path_int = 3;
                    }
                    userList = (await UserProvider.GetFirestoreUserFromRole(path_int));
                }
                catch (Exception ex)
                {

                }
            }
            else if (tbi.Text == "HighLites")
            {
                try
                {
                    string hl = await DisplayActionSheet("Select Highelites:", "Cancel", null, "MVP", "Alpha", "Beta", "Gold");
                    
                    userList = (await UserProvider.GetFirestoreUserFromHighlit(hl));
                }
                catch (Exception ex)
                {

                }
            } else
            {
                userList = (await UserProvider.GetFirestoreAllUser());
            }

            model = ((UserListViewModel)BindingContext);
            await model.InitColors();
            model.source = userList.Select(user => UserListItem.FromUser(user)).ToList();
            model.Users = new ObservableCollection<UserListItem>(model.source);
            foreach (var user in model.Users)
            {
                user.PageModel = model;
            }

            OnAppearing();


        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            refreshView.IsRefreshing = true;
            OnAppearing();
            refreshView.IsRefreshing = false;
        }
    }
}