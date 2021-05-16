using Rg.Plugins.Popup.Services;
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
 

    public partial class ViewNews 
    {
      
        public ViewNews()
        {
            InitializeComponent();
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}