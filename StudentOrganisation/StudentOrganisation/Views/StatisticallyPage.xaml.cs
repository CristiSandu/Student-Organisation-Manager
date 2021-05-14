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

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticallyPage : ContentPage
    {
        ObservableCollection<Entry> _entrys = new ObservableCollection<Entry>();
        Color _chartColor;
        public StatisticallyPage()
        {
            InitializeComponent();
        }

        public async Task CreateStatistics()
        {
            Entry entry;
            Dictionary<string, int> dict = await Services.UserProvider.CountPerRole();
            List<Entry> entry_list = new List<Entry>();
            foreach(var kvp in dict)
            {
                string color = await Services.ColorsProvider.GetColorFor(kvp.Key);
                entry = new Entry(kvp.Value)
                {
                    Color = SKColor.Parse(color),
                    ValueLabelColor=SKColor.Parse(color),
                    Label=$"{kvp.Key}",
                    ValueLabel = $"{kvp.Value}"
                };
              // if ( _entrys.Where(x => x.Label == kvp.Key) && _entrys.Where(x => x.ValueLabel == kvp.Value.ToString()))
                _entrys.Add(entry);
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await CreateStatistics();
            chartView.Chart = new DonutChart { Entries = _entrys, LabelTextSize = 40f, BackgroundColor = SKColor.Parse("#FFFFFF") };
        }
    }
}