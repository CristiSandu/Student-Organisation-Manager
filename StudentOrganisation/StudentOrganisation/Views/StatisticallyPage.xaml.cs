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
using StudentOrganisation.Models;

namespace StudentOrganisation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticallyPage : ContentPage
    {
        ObservableCollection<Entry> _entrys = new ObservableCollection<Entry>();
        Models.MainStatisticsModel mainStatistics = new Models.MainStatisticsModel();
        Dictionary<string, StatisticsModel> stats = new Dictionary<string, StatisticsModel>()
        {
            {"PATH", new StatisticsModel() },
            {"ROLE", new StatisticsModel() },
            {"HIGHL", new StatisticsModel() },
            {"STARS", new StatisticsModel() },
            {"YEAR", new StatisticsModel() },

        };

        Dictionary<int, int> yearStat = new Dictionary<int, int>();
        StatisticsModel _stat;
        Button _latsButton = null;
        public StatisticallyPage()
        {
            InitializeComponent();

        }

        public async Task CreateStatistics()
        {
            Entry entry;
            Dictionary<string, int> dict = await Services.UserProvider.CountPerPath();
            List<Entry> entry_list = new List<Entry>();
            foreach (var kvp in dict)
            {
                string color = await Services.ColorsProvider.GetColorFor(kvp.Key);
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
        }

        public async Task CreateDefaultStat()
        {
            //    List<string> str = new List<string> { "MVP", "Alpha", "Beta", "Gold" };
            //    List<string> str1 = new List<string> { "Mobile", "Limbaje", "AI", "IoT", "Azure", "Gaming" };

            //    int text = await Services.UserProvider.CountAll();
            //    NrUser.Text = text.ToString();
            //    Dictionary<string, int> dict = await Services.UserProvider.CountPerRole();
            //    NrUser2.Text = dict["Junior"].ToString();
            //    Dictionary<string, int> dict2 = await Services.UserProvider.CountPerHighlits();
            //    List<String> myKeys = dict2.Keys.ToList();
            //    var list = myKeys.Except(str).ToList();
            //    NrUser3.Text = dict2[list[0]].ToString();
            //    DescriptionNrUser3.Text = $"Max Distinction {list[0].ToUpper()}";

            //    Dictionary<string, int> dict3 = await Services.UserProvider.CountPerHighlits();
            //    List<String> myKeys3 = dict2.Keys.ToList();
            //    var list3 = myKeys3.Except(str1).ToList();
            //    NrUser4.Text = dict2[list3[0]].ToString();
            //    DescriptionNrUser4.Text = $"Max Memeber in {list3[0].ToUpper()}";

            stats["PATH"] = await Models.StatisticsModel.GetStatisticByName("PATH");
            stats["ROLE"] = await Models.StatisticsModel.GetStatisticByName("ROLE");
            stats["HIGHL"] = await Models.StatisticsModel.GetStatisticByName("HIGHL");
            stats["STARS"] = await Models.StatisticsModel.GetStatisticByName("STARS");
            stats["YEAR"] = await Models.StatisticsModel.GetStatisticByName("YEAR");


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            refreshViewStat.IsRefreshing = true;
            await mainStatistics.MainStatGen();
            await CreateDefaultStat();
            mainStatistics.Chr = stats["YEAR"].Chr;
            mainStatistics.Name = "Meets per 2021";
            BindingContext = mainStatistics;

            refreshViewStat.IsRefreshing = false;

            // chartView.Chart = new DonutChart { Entries = _entrys, LabelTextSize = 40f, BackgroundColor = SKColor.Parse("#FFFFFF") };
        }

        private async void Filter1_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Style == (Style)Application.Current.Resources["ButtonCheckedStyle"])
                btn.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
            else
                btn.Style = (Style)Application.Current.Resources["ButtonCheckedStyle"];

            StatisticsModel st;
            if (btn.Text != null)
            {
                if (btn.Text.ToUpper() == "PATH")
                {
                    st = stats["PATH"];
                }
                else if (btn.Text.ToUpper() == "ROLE")
                {
                    st = stats["ROLE"];
                }
                else if (btn.Text.ToUpper() == "HIGHL")
                {
                    st = stats["HIGHL"];
                }
                else
                {
                    st = stats["STARS"];
                }
            }else
            {
                st = stats["STARS"];
            }
            await PopupNavigation.Instance.PushAsync(new StatisticallyPopUp { BindingContext = st });


            if (_latsButton != null)
            {
                if (_latsButton.Style == (Style)Application.Current.Resources["ButtonCheckedStyle"])
                    _latsButton.Style = (Style)Application.Current.Resources["ButtonUnCheckStyle"];
                else
                    _latsButton.Style = (Style)Application.Current.Resources["ButtonCheckedStyle"];
                _latsButton = btn;
            }
            else
            {
                _latsButton = btn;
            }
        }

        private void refreshView_Refreshing(object sender, EventArgs e)
        {
            OnAppearing();
        }
    }
}