﻿using StudentOrganisation.Models;
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
        public void FilterByName(string filter)
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
                new UserListItem(Name: "Popica von Brailangels", Role: "Mentor", this,Path:"Rome"),
                new UserListItem(Name: "Matei Popovici", Role: "Junior", this,Path:"Limbaje"),
                new UserListItem(Name: "Adrian Margineanu", Role: "Mentor", this,Path:"Limbaje"),
                new UserListItem(Name: "Victor Tudose", Role: "Admin", this,Path:"IoT"),
                new UserListItem(Name: "Crysti Sandu", Role: "Mentor", this),
                new UserListItem(Name: "Luci Iliescu", Role: "Dat Afara", this),
                new UserListItem(Name: "Bogdi Piele", Role: "Mentor", this),
                new UserListItem(Name: "Robert Raiu", Role: "Mentor", this,Path:"IoT"),
                new UserListItem(Name: "Stefan Pana", Role: "Junior", this,Path:"IoT"),
            };
            roleToColorConverter = new RoleToColorConverter();
            Users = new ObservableCollection<UserListItem>(source);
        }
    }
}