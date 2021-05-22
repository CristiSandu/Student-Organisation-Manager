using StudentOrganisation.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using StudentOrganisation.Services;
using System.Threading.Tasks;

namespace StudentOrganisation.ViewModels
{

    public class RoleToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Ret="#cccccc";
            string Search= (string)value;
            UserListViewModel.colors.TryGetValue(Search, out Ret);
            return Color.FromHex( Ret ?? "#cccccc");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "no";
        }
    }

    public class UserListViewModel
    {
        public static Dictionary<string, string> colors;
        public List<UserListItem> source { get; set; }
        public ObservableCollection<UserListItem> Users { get; set; }
        public void FilterByName(string filter)
        {
            IList<UserListItem> filteredItems;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filteredItems = source.Where(userItemList => userItemList.Name.ToLower().Contains(filter.ToLower())).ToList();
            }
            else
            {
                filteredItems = source;
            }
            
            foreach (var userItemList in source)
            {
                if (!filteredItems.Contains(userItemList))
                {
                    Users.Remove(userItemList);
                }
                else
                {
                    if (!Users.Contains(userItemList))
                    {
                        Users.Add(userItemList);
                    }
                }
            }
        }

        public async Task InitColors()
        {
            colors["Junior"] = (await ColorsProvider.GetColorFor("Junior")) ?? "#cccccc";
            colors["Member"] = (await ColorsProvider.GetColorFor("Member")) ?? "#cccccc";
            colors["Mentor"] = (await ColorsProvider.GetColorFor("Mentor")) ?? "#cccccc";
            colors["Admin"] = (await ColorsProvider.GetColorFor("Admin")) ?? "#cccccc";
        }

        public UserListViewModel()
        {
            colors = new Dictionary<string, string>();
        }

    }
}