using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

        public NewsTab()
        {
            InitializeComponent();

            newsList.ItemsSource = news;
            links.ItemsSource = _links;
        }

        private void links_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}