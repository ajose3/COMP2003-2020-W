using MobileApp.Data;
using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;

namespace MobileApp.ViewModels
{
    [QueryProperty("ShellQuery", "query")]
    public class SearchViewModel : INotifyPropertyChanged
    {

        public string searchQuery { get; set; }
        public IList<Product> FilteredProducts { get; set; }

        public string ShellQuery
        {
            set
            {
                searchQuery = Uri.UnescapeDataString(value);
                OnPropertyChanged("searchQuery");
                FilteredProducts = ProductData.Products.Where(x => x.Name.ToLower().Contains(searchQuery.ToLower())).ToList();
                OnPropertyChanged("FilteredProducts");
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
