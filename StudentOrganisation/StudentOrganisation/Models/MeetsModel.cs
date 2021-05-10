using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentOrganisation.Models
{
    public class MeetsModel
    {
        public static string CollectionPath = "meets";
        [MapTo("title")]
        public string Title { get; set; }
        [MapTo("description")]
        public string Description { get; set; }
        [MapTo("role")]
        public int Role { get; set; }
        [MapTo("location")]
        public string Location { get; set; }
        [MapTo("link")]
        public string link { get; set; }
        [MapTo("attendance_list")]
        public List<string> AttendanceList { get; set; }
        [MapTo("date")]
        public DateTime Date { get; set; }
    }
}
