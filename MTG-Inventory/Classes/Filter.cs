using MTG_Inventory.Core;
using MTG_Inventory.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class Filter : ObservableObject
    {
        // Color Filter
        public List<char> ColorFilterList { get; set; } = new List<char>();

        // Card Types Filter
        public List<string> CardTypeFilterList { get; set; } = new List<string>();


        // Search Fields
        private string _titleSearchText = "";
        public string TitleSearchText
        {
            get { return _titleSearchText; }
            set 
            { 
                _titleSearchText = value;
                OnPropertyChanged();
            }
        }

        private string _cardSearchText = "";
        public string CardSearchText
        {
            get { return _cardSearchText; }
            set 
            { 
                _cardSearchText = value;
                OnPropertyChanged();
            }
        }

        public Filter()
        {
        }
    }
}
