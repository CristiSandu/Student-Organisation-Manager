using StudentOrganisation.Models;
using StudentOrganisation.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace StudentOrganisation.Controllers
{
    public class UserDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Junior { get; set; }
        public DataTemplate Member { get; set; }
        public DataTemplate Mentor { get; set; }
        public DataTemplate Admin { get; set; }
        public DataTemplate Default { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Debug.WriteLine("In OnSelectTemplate");
            switch (((UserListItem)item).Role)
            {
                case "Junior":
                    return Junior;
                case "Member":
                    return Member;
                case "Mentor":
                    return Mentor;
                case "Admin":
                    return Admin;
                default:
                    return Default;
            }
        }
    }
}
