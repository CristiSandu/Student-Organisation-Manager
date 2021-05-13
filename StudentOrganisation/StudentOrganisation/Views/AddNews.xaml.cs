using StudentOrganisation.Models;
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
    public partial class AddNews : ContentPage
    {
        NewsModel _news = new NewsModel();
        public AddNews()
        {
            InitializeComponent();
            BindingContext = _news;
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            await Services.NewsProvider.Create(_news);
            await Navigation.PopAsync();


        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}