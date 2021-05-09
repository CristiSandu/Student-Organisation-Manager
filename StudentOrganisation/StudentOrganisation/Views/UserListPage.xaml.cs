using StudentOrganisation.Models;
using StudentOrganisation.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserListPage : ContentPage
    {
        public UserListPage()
        {
            InitializeComponent();
            BindingContext = new UserListViewModel();
        }
    }
}