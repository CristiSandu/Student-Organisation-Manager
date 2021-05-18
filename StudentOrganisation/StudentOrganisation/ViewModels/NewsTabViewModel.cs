using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentOrganisation.ViewModels
{
    class NewsTabViewModel : INotifyPropertyChanged
    {
        const int RefreshDuration = 2;
        int itemNumber = 1;
        bool isRefreshing;
        bool isVis;

        public bool IsVis
        {
            get { return isVis; }
            set
            {
                if (App._role == 2 || App._role == 3)
                    isVis = true;
                else
                    isVis = false;
            }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }



        public ObservableCollection<Models.NewsModel> News { get; private set; }
        public ObservableCollection<Models.LinksModel> Links { get; private set; }

        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());

        public NewsTabViewModel()
        {
            
           AddItemsAsync();
        }

        async Task AddItemsAsync()
        {
            News = new ObservableCollection<Models.NewsModel>(await Services.NewsProvider.GetAll());
            Links = new ObservableCollection<Models.LinksModel>(await Services.LinksProvider.GetAll());
        }

        async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            await AddItemsAsync();
            IsRefreshing = false;
        }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
