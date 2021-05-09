using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentOrganisation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        string _UserToken;
        public AppShell()
        {
            InitializeComponent();
        }

        public AppShell(string token)
        {
            InitializeComponent();
            _UserToken = token;
        }
    }
}