using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;

namespace StudentOrganisation.Models
{
    public class StatisticsModel
    {
        public Chart Chr { get; set; }
        public string Name { get; set; }

        public async static Task<StatisticsModel> GetStatisticByName(string name)
        {
            Dictionary<string, int> dict;
            StatisticsModel statisticsModel = new StatisticsModel();
            Entry entry;
            List<Entry> entry_list = new List<Entry>();

            if (name.ToLower() == "path")
            {
                dict = await Services.UserProvider.CountPerPath();
                statisticsModel.Name = "Users Per Path";
            }
            else if (name.ToLower() == "role")
            {
                dict = await Services.UserProvider.CountPerRole();
                statisticsModel.Name = "Users Per Role";

            }
            else if (name.ToLower() == "highl")
            {
                dict = await Services.UserProvider.CountPerHighlits();
                statisticsModel.Name = "Users Per Highlits";

            }
            else if (name.ToLower() == "stars")
            {
                List<Models.User> list_usr = await Services.UserProvider.GetUsersWithMoreStars(5);
                statisticsModel.Name = "The moste stars";
                Random rand = new Random();
                Color chartColor;
                foreach (var user in list_usr)
                {
                    chartColor = Color.FromRgb(rand.Next(150), rand.Next(150), rand.Next(150));
                    entry = new Entry(user.Stars)
                    {
                        Color = SKColor.Parse(chartColor.ToHex().ToString()),
                        ValueLabelColor = SKColor.Parse(chartColor.ToHex().ToString()),
                        Label = $"{user.Name} {user.SecondName.Substring(0, 1)}.",
                        ValueLabel = $"{user.Stars}"
                    };

                    entry_list.Add(entry);
                }

                statisticsModel.Chr = new LineChart { Entries = entry_list, LabelOrientation = Microcharts.Orientation.Horizontal, ValueLabelOrientation = Orientation.Horizontal, LabelTextSize = 40f, BackgroundColor = SKColor.Parse("#FFFFFF") };

                return statisticsModel;
            }
            else if (name.ToLower() == "year")
            {
                Dictionary<int, int> dictEvMoths = await Services.MeetsProvider.CountPerYear(2021);
                Random rand = new Random();
                Color chartColor;
                foreach (var kvp in dictEvMoths)
                {
                    chartColor = Color.FromRgb(rand.Next(150), rand.Next(150), rand.Next(150));
                    string i = new DateTime(2015, kvp.Key, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en")).Substring(0, 2);
                    int j = 0;
                    entry = new Entry(kvp.Value)
                    {
                        Color = SKColor.Parse(chartColor.ToHex().ToString()),
                        ValueLabelColor = SKColor.Parse(chartColor.ToHex().ToString()),
                        Label = $"{i}.",
                        ValueLabel = $"{kvp.Value}"
                    };

                    entry_list.Add(entry);
                }
                statisticsModel.Chr = new LineChart { Entries = entry_list, LabelOrientation = Microcharts.Orientation.Horizontal, ValueLabelOrientation = Orientation.Horizontal, LabelTextSize = 40f, BackgroundColor = SKColor.Parse("#FFFFFF") };
                return statisticsModel;
            }
            else
            {
                dict = new Dictionary<string, int>();
                return statisticsModel;
            }


            foreach (var kvp in dict)
            {
                string color = await Services.ColorsProvider.GetColorFor(kvp.Key);
                int i = 0;
                entry = new Entry(kvp.Value)
                {
                    Color = SKColor.Parse(color),
                    ValueLabelColor = SKColor.Parse(color),
                    Label = $"{kvp.Key}",
                    ValueLabel = $"{kvp.Value}"
                };

                entry_list.Add(entry);
            }

            statisticsModel.Chr = new DonutChart { Entries = entry_list, LabelTextSize = 40f, BackgroundColor = SKColor.Parse("#FFFFFF") };
            return statisticsModel;
        }
    }
}
