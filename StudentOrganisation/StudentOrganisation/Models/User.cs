using System;
using System.Collections.Generic;
using Plugin.CloudFirestore.Attributes;
using System.Text;

namespace StudentOrganisation.Models
{
    public class User
    {

        public enum Pozition {Junior = 0, Member = 1, Mentor= 2, Admin=3};

        public static string CollectionPath = "users";
        [Id]
        public string Id { get; set; }
        [MapTo("name")]
        public string Name { get; set; }
        [MapTo("second_name")]
        public string SecondName { get; set; }
        [MapTo("email")]
        public string Email { get; set; }
        [MapTo("mentor")]
        public string Mentor { get; set; }
        [MapTo("path")]
        public List<string> Path { get; set; }
        [MapTo("stars")]
        public int Stars { get; set; }
        [MapTo("role")]
        public int Role { get; set; }
        [MapTo("highlists")]
        public List<string> Highlits {get; set;}
        [MapTo("is_present")]
        public bool IsPresent { get; set; }
    }
}
