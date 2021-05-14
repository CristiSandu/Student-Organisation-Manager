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
    [QueryProperty(nameof(Title), nameof(Title))]
    [QueryProperty(nameof(Content), nameof(Content))]
    [QueryProperty(nameof(Description), nameof(Description))]
    [QueryProperty(nameof(Date), nameof(Date))]

    public partial class ViewNews : ContentPage
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        NewsModel newsMod;
        public ViewNews()
        {
            InitializeComponent();
            newsMod = new NewsModel { Title = Title, Content = Content, Description = Description };
            int i = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}