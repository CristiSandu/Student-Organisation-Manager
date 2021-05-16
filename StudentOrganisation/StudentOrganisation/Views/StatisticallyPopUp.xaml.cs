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

        public StatisticallyPopUp(ObservableCollection<Entry> entry)
        {
            InitializeComponent( );

            _entry = entry;
            Chart cht = new DonutChart { Entries = _entry, LabelTextSize = 40f, BackgroundColor = SKColor.Parse("#FFFFFF") };
            chartView.Chart = cht;
        }

        
    }
}