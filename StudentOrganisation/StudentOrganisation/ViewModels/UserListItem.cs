using StudentOrganisation.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static StudentOrganisation.Models.User;

namespace StudentOrganisation.ViewModels
{
    public class UserListItem
    {
        public UserListViewModel PageModel { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string StudyingPath { get; set; }

        public static IList<UserListItem> FilterByName(string filter, IList<UserListItem> Source)
        {
            IList<UserListItem> filteredItems;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                filteredItems = Source.Where(userItemList => userItemList.Name.ToLower().Contains(filter.ToLower())).ToList();
            }
            else
            {
                filteredItems = Source;
            }
            return filteredItems;
        }

        public static UserListItem FromUser(User user)
        {
            UserListItem userListItem = new UserListItem();
            userListItem.Name = user.Name + user.SecondName;
            switch (user.GetPozition())
            {
                case Pozition.Admin:
                    userListItem.Role = "Admin";
                    break;
                case Pozition.Mentor:
                    userListItem.Role = "Mentor";
                    break;
                case Pozition.Member:
                    userListItem.Role = "Member";
                    break;
                default:
                    userListItem.Role = "Junior";
                    break;
            }

            userListItem.StudyingPath = user.Path;
            return userListItem;
        }
    }
}