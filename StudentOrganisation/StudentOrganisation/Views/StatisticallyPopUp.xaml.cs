using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using SkiaSharp;
using Microcharts;
using Rg.Plugins.Popup.Services;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticallyPopUp 
    {
        ObservableCollection<Entry> _entry;
        public StatisticallyPopUp()
        {
            InitializeComponent();
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}