using System;
using System.Collections.Generic;
using System.Text;

namespace StudentOrganisation.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string ImageUrl { get; set; }

        public string Path { get; set; }
        public List<User> Juniors { get; set; }
        public User Mentor { get; set; }

        public User(string Name, string Role="Membru",string Path="Mobile")
        {
            this.Name = Name;
            this.Role = Role;
            this.Path = Path;
        }
    }
}
