using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentOrganisation.Models
{
    class NewsModel
    {
        [MapTo("title")]
        public string Title { get; set; }
        [MapTo("content")]
        public string Content { get; set; }
        [MapTo("description")]
        public string Description { get; set; }
        [MapTo("date")]
        public DateTime Date { get; set; }
    }
}
