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
        public string ImageURL { get; set; }
        public string Color { get; set; }

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

        public async void setImageUrl(User user)
        {
            ImageURL = (await Services.FirebaseStorageProvider.GetProfilePictureUrl(user)) ?? "blank-profile.png";
        }

        public static UserListItem FromUser(User user)
        {
            UserListItem userListItem = new UserListItem();
            userListItem.Name = (user.Name)??"" +" "+ (user.SecondName) ?? "";
            switch (user.Role)
            {
                case 3:
                    {
                        userListItem.Role = "Admin";
                        break;
                    }
                case 2:
                    {
                        userListItem.Role = "Mentor";
                        break;
                    }
                case 1:
                    {
                        userListItem.Role = "Member";
                        break;
                    }
                default:
                    {
                        userListItem.Role = "Junior";
                        break;
                    }
            }
            
            userListItem.StudyingPath = ( user.Path??(new List<string> { "No path"} ) ).First();
            userListItem.setImageUrl(user);

            return userListItem;
        }
    }
}