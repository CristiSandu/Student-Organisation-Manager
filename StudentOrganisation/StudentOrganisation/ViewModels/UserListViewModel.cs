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
            Debug.WriteLine("In converter");
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
        public void FilterByName(string filter)
        {
            Debug.WriteLine("In FilterByName");
            IList<UserListItem> filteredItems;
            Debug.WriteLine(filter);
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
            Debug.WriteLine($"Users L ={Users.Count}");
        }
        public UserListViewModel()
        {
        
        }

        public async Task Popullate()
        {
            Debug.WriteLine("Before");
            List<User> userList = (await FirestoreUser.GetFirestoreAllUser());
            source = userList.Select( user => UserListItem.FromUser(user)).ToList();
            /*source = new List<UserListItem> {

                new UserListItem { Name= "Matei Popovici", Role = "Junior", PageModel=this },
                new UserListItem { Name= "Matei Popovici", Role = "Mentor", PageModel=this },
                new UserListItem { Name= "Matei Popovici", Role = "Member", PageModel=this }
            };*/

            Users = new ObservableCollection<UserListItem>(source);

            Debug.WriteLine($"userList L ={userList.Count}");
            Debug.WriteLine($"source L ={source.Count}");

            Debug.WriteLine("Exiting Popullate");
        }
    }
}