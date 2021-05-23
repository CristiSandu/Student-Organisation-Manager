using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using StudentOrganisation.Models;
using System.Windows.Input;
using Xamarin.Essentials;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMeet
    {
        public ICommand Command => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public ViewMeet()
        {
            InitializeComponent();
            TapCommand.Command = Command;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string role = await SecureStorage.GetAsync("Role");
            if (role == "2" || role == "3")
            {
                DeleteBtn.IsVisible = true;
            }
        }

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            MeetsModel meet = (MeetsModel)BindingContext;
            bool isOk = await DisplayAlert("Warning!", "Do you want to delete this item?", "Yes", " NO");
            if (isOk == true)
            {
                await Services.MeetsProvider.Delete(meet);
                await PopupNavigation.Instance.PopAsync();
            }
        }
    }
}