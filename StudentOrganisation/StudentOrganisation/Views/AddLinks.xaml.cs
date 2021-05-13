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
    public partial class AddLinks : ContentPage
    {
        LinksModel _link = new LinksModel();
        public AddLinks()
        {
            InitializeComponent();
            BindingContext = _link;
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            _link.Date = DateTime.Now;
            await Services.LinksProvider.Create(_link);
            await Navigation.PopAsync();
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
    }
}