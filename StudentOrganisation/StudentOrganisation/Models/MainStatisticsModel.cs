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


        public async Task MainStatGen()
        {/*
            List<string> str = new List<string> { "MVP", "Alpha", "Beta", "Gold" };
            List<string> str1 = new List<string> { "Mobile", "Limbaje", "AI", "IoT", "Azure", "Gaming" };

            int text = await Services.UserProvider.CountAll();
            Statistics1 = text;
            Dictionary<string, int> dict = await Services.UserProvider.CountPerRole();
            Statistics2 = dict["Junior"];

            Dictionary<string, int> dict2 = await Services.UserProvider.CountPerHighlits();
            List<String> myKeys = dict2.Keys.ToList();
            var list = myKeys.Except(str).ToList();
            Statistics3 = dict2[list[0]];
            Description3 = $"{list[0].ToUpper()}";

            Dictionary<string, int> dict3 = await Services.UserProvider.CountPerPath();
            List<String> myKeys3 = dict3.Keys.ToList();
            var list3 = myKeys3.Except(str1).ToList();
            Statistics4 = dict3[list3[0]];
            Description4 = $"{list3[0].ToUpper()}";*/
        }
    }
}
