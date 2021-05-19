using StudentOrganisation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using StudentOrganisation.Services;
using System.Collections.ObjectModel;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsTab : ContentPage
    {
        List<Models.NewsModel> news = new List<Models.NewsModel> { new Models.NewsModel
        {
            Title = "Recrutari Junior",
            Description = "In perioada urmatoare o sa incepem recrutare juniorilor",
            Date = DateTime.Now
        } , new Models.NewsModel{
            Title = "Microsoft Ignite",
            Description = "Inscrieri Microsoft Ignite au inceput",
            Date = DateTime.Now
        }, new Models.NewsModel{
            Title = "Team Building",
            Description = "Alegere data team building la urmatoare sedinta ",
            Date = DateTime.Now
        }};

        List<Models.LinksModel> _links = new List<Models.LinksModel>
        {
           new Models.LinksModel{ Title = "Resurse Liga" },
           new Models.LinksModel{ Title = "Sheet prezenta sedinta" },
           new Models.LinksModel{ Title = "Test Prezenta" }
        };
        IFirebaseAuthentication auth;
        ObservableCollection<NewsModel> _newsList; 

        public NewsTab()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthentication>();
        }
        
        protected async override void OnAppearing()
        {
            base.OnAppearing();
           string role = await SecureStorage.GetAsync("Role");
            if(role == "2" || role == "3")
            {
                buttonLink.IsVisible = true;
                buttonNews.IsVisible = true;
            }
            _newsList = new ObservableCollection<NewsModel>(await Services.NewsProvider.GetAll());
            newsList.ItemsSource = _newsList;
            links.ItemsSource = await Services.LinksProvider.GetAll();
        }
       

        private async void newsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewsModel news = e.CurrentSelection.FirstOrDefault() as NewsModel;

            if (e.CurrentSelection != null)
            {
                
                await PopupNavigation.Instance.PushAsync(new ViewNews
                {
                    BindingContext = news
                });
                
            }
        }

        private async void links_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LinksModel link = e.CurrentSelection.FirstOrDefault() as LinksModel;
            
            try
            {
                await Browser.OpenAsync(link.URL, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }
        }

        private async void AddLinksBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddLinks());
        }

        private async void AddNewsBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNews ());
        }

        private void logOut_Clicked(object sender, EventArgs e)
        {
            var signedOut = auth.SignOut();
            if (signedOut)
            {
                SecureStorage.Remove("isLogged");
                SecureStorage.Remove("Role");

                ((App)Application.Current).MainPage = new NavigationPage(new TestLogin());
            }
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
            refreshView.IsRefreshing = false;
        }


        private async void meetsPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MeetsPage());
        }


        private async void addMeet_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMeet());
        }
    }
}