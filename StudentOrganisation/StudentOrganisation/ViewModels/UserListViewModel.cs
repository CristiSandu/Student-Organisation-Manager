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

namespace StudentOrganisation.ViewModels
{

    public class UserListItem
    {
        public UserListViewModel PageModel { get; set; }
        public User user { get; set; }
        public UserListItem(String Name,String Role, UserListViewModel PageModel)
        {
            this.PageModel = PageModel;
            this.user = new User(Name,Role);
        }
    }

    public class RoleToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "Junior":
                    return Color.FromHex("#4FC1E8");
                case "Menber":
                    return Color.FromHex("#AC92EB");
                case "Mentor":
                    return Color.FromHex("#FFCE54");
                case "Admin":
                    return Color.FromHex("#A0D568");
                default:
                    return Color.FromHex("#B8D8D8");
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "no";
        }
    }
    public class UserListViewModel
    {
        public List<UserListItem> source { get; set; }

        public ObservableCollection<UserListItem> Users { get; set; }
        public RoleToColorConverter roleToColorConverter {get;set;}
        public ICommand FilterCommand => new Command<string>(FilterItems);
        public void FilterItems(string filter)
        {
            IList<UserListItem> filteredItems;
            Debug.WriteLine(filter);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filteredItems = source.Where(userItemList => userItemList.user.Name.ToLower().Contains(filter.ToLower())).ToList();
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
        public UserListViewModel()
        {
            source = new List<UserListItem>
            {
                new UserListItem(Name: "Popica von Brailangels", Role: "Roman Legion", this),
                new UserListItem(Name: "Matei Popovici", Role: "Junior", this),
                new UserListItem(Name: "Adrian Margineanu", Role: "Mentor", this),
                new UserListItem(Name: "Victor Tudose", Role: "Admin", this),
                new UserListItem(Name: "Crysti Sandu", Role: "Mentor", this),
                new UserListItem(Name: "Luci Iliescu", Role: "Dat Afara", this),
                new UserListItem(Name: "Bogdi Piele", Role: "Mentor", this),
                new UserListItem(Name: "Robert Raiu", Role: "Mentor", this),
                new UserListItem(Name: "Stefan Pana", Role: "Junior", this),
                new UserListItem(Name: "Dinu Mazur", Role: "Iesi afara", this)
            };
            roleToColorConverter = new RoleToColorConverter();
            Users = new ObservableCollection<UserListItem>(source);
        }
    }
}
