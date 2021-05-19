using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentOrganisation.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMeet : ContentPage
    {
        MeetsModel _meets = new MeetsModel();
        public AddMeet()
        {
            InitializeComponent();
            _meets.Date = DateTime.Now;
            BindingContext = _meets;
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            await Services.MeetsProvider.Create(_meets);
            await Navigation.PopAsync();
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}