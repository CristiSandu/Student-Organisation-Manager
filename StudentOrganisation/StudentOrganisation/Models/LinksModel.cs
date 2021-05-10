using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentOrganisation.Models
{
    public class LinksModel
    {
        public static string CollectionPath = "websites"
        [MapTo("title")]
        public string Title { get; set; }
        [MapTo("url")]
        public string URL { get; set; }
        [MapTo("date")]
        public DateTime Date { get; set; }
    }
}
