using Rg.Plugins.Popup.Services;
using StudentOrganisation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
 

    public partial class ViewNews 
    {
      
        public ViewNews()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string role = await SecureStorage.GetAsync("Role");
            if (role == "2" || role == "3")
            {
                DeleteBtn.IsVisible = true;
                UpdateBtn.IsVisible = true;
            }
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            NewsModel news = (NewsModel)BindingContext;
            bool isOk = await DisplayAlert("Warning!", "Do you want to delete this item?", "Yes", " NO");
            if (isOk == true)
            {
                await Services.NewsProvider.Delete(news);
                await PopupNavigation.Instance.PopAsync();
            }
        }

        private async void UpdateBtn_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Views.AddNews
            //{
            //    BindingContext = sender as NewsModel
            //});
        }
    }
}