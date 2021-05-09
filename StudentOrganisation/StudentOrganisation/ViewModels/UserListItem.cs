using StudentOrganisation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StudentOrganisation.ViewModels
{
    public class UserListItem
    {
        public UserListViewModel PageModel { get; set; }
        public User user { get; set; }
        public UserListItem(String Name, String Role, UserListViewModel PageModel)
        {
            this.PageModel = PageModel;
            this.user = new User(Name, Role);
        }
        public static IList<UserListItem> FilterByName(string filter,IList<UserListItem> Source)
        {
            IList<UserListItem> filteredItems;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filteredItems = Source.Where(userItemList => userItemList.user.Name.ToLower().Contains(filter.ToLower())).ToList();
            }
            else
            {
                filteredItems = Source;
            }
            return filteredItems;
        }
    }
}
