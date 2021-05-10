using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentOrganisation.Models
{
    public class ColorsModel
    {

        public static string CollectionPath = "colors";
        [MapTo("title")]
        public string Title { get; set; }
        [MapTo("color")]
        public string Color { get; set; }
    }
}
