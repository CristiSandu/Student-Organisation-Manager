using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentOrganisation.Models
{
    public class MainStatisticsModel
    {
        public int Statistics1 { get; set; }
        public string Description1 { get; set; }

        public int Statistics2 { get; set; }
        public string Description2 { get; set; }

        public int Statistics3 { get; set; }
        public string Description3 { get; set; }

        public int Statistics4 { get; set; }
        public string Description4 { get; set; }

        public Chart Chr { get; set; }
        public string Name { get; set; }


        public async Task MainStatGen()
        {
            List<string> str = new List<string> { "MVP", "Alpha", "Beta", "Gold" };
            List<string> str1 = new List<string> { "Mobile", "Limbaje", "AI", "IoT", "Azure", "Gaming" };

            int text = await Services.UserProvider.CountAll();
            Statistics1 = text;
            Dictionary<string, int> dict = await Services.UserProvider.CountPerRole();
            Statistics2 = dict["Junior"];

            string val = "";
            int max = 0;
            Dictionary<string, int> dict2 = await Services.UserProvider.CountPerHighlits();
            foreach (string s in str)
            {
                if (dict2[s] > max)
                {
                    max = dict2[s];
                    val = s;
                }
            }
            Statistics3 = max;
            Description3 = val;


            val = "";
            max = 0;
            Dictionary<string, int> dict3 = await Services.UserProvider.CountPerPath();

            foreach (string s in str1)
            {
                if (dict3[s] > max)
                {
                    max = dict3[s];
                    val = s;
                }
            }
            Statistics4 = max;
            Description4 = val;
        }
    }
}
