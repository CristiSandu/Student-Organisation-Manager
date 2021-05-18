using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using SkiaSharp;
using Entry = Microcharts.ChartEntry;
using System.Collections.ObjectModel;
using Rg.Plugins.Popup.Services;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticallyPage : ContentPage
    {
        ObservableCollection<Entry> _entrys = new ObservableCollection<Entry>();
        Button _latsButton = null;
        public StatisticallyPage()
        {
            InitializeComponent();
        }

        public async Task CreateStatistics()
        {
            Entry entry;
            Dictionary<string, int> dict = await Services.UserProvider.CountPerPath();
            int nrOfUsers =0;
            List<Entry> entry_list = new List<Entry>();
            foreach (var kvp in dict)
            {
                string color = await Services.ColorsProvider.GetColorFor(kvp.Key);
                nrOfUsers += kvp.Value;
                entry = new Entry(kvp.Value)
                {
                    Color = SKColor.Parse(color),
                    ValueLabelColor = SKColor.Parse(color),
                    Label = $"{kvp.Key}",
                    ValueLabel = $"{kvp.Value}"
                };
                if (!_entrys.Any(x => x.Label == kvp.Key))
                    _entrys.Add(entry);
            }

            NrUser.Text = nrOfUsers.ToString();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CreateStatistics();
           // chartView.Chart = new DonutChart { Entries = _entrys, LabelTextSize = 40f, BackgroundColor = SKColor.Parse("#FFFFFF") };
        }

        private async void Filter1_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Style == (Style)Application.Current.Resources["ButtonCheckedStyle"])
                btn.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
            else
                btn.Style = (Style)Application.Current.Resources["ButtonCheckedStyle"];

            await PopupNavigation.Instance.PushAsync(new StatisticallyPopUp(_entrys));
                


            if (_latsButton != null)
            {
                if (_latsButton.Style == (Style)Application.Current.Resources["ButtonCheckedStyle"])
                    _latsButton.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
                else
                    _latsButton.Style = (Style)Application.Current.Resources["ButtonCheckedStyle"];
                _latsButton = btn;
            }else
            {
                _latsButton = btn;
            }
        }
    }
}