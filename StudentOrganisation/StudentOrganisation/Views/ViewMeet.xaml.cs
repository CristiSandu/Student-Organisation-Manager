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

        private async void CancelBtn_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void DeleteBtn_Clicked(object sender, EventArgs e)
        {
            MeetsModel meet = (MeetsModel)BindingContext;
            int i = 0;
            await Services.MeetsProvider.Delete(meet);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}