using MTG_Inventory.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MTG_Inventory.Classes;
using MTG_Inventory.MVVM.Model;
using MTG_Inventory.MVVM.View;
using System.Windows.Controls;

namespace MTG_Inventory.MVVM.ViewModel
{
    class AllCardsViewModel : ObservableObject
    {
        private IEnumerable<Card?> _card_List; // DataList the DataGrid is working with
        public IEnumerable<Card?> Card_List
        {
            get { return _card_List; }
            set
            {
                _card_List = value;
                OnPropertyChanged();
            }
        }

        // Card Type List        
        public List<string> CardTypes { get; set; }
        public Filter Filter 
        { get => filter;
            set 
            {
                filter = value;
                PerformSelect();
            }
        }

        // Constructor
        public AllCardsViewModel()
        {
            CardTypes = DataModel.cardTypes;
            Filter = AllCardsView.filter;

            // LoadSampleData();
            PerformLoad();
        }

        // Initial Load of Data
        private void PerformLoad()
        {
            var query = DataModel.cardList.Where(x => x.setCode == "LEA")
                                          .Select(x => x)
                                          .OrderBy(x => x.name);

            Card_List = query.ToList();
        }

        // Button Commands        
        // Button Select        
        public RelayCommand BTN_Select => new RelayCommand(PerformSelect);
        private void PerformSelect(object commandParameter = null)
        {
            Card_List = null;
            // Get all Filter Values            

            var query = DataModel.cardList.Where(x => x.types.Any(item => Filter.CardTypeFilterList.Contains(item.ToLower())) &&
                                                      x.colors.Any(item => Filter.ColorFilterList.Contains(Convert.ToChar(item.ToUpper()))) &&
                                                      x.name.Contains(Filter.TitleSearchText))
                                          .Select(x => x)
                                          .OrderBy(x => x.name);

            Card_List = query.ToList();
        }        

        // Button Reset
        public RelayCommand BTN_Reset => new RelayCommand(PerformReset);
        private void PerformReset(object commandParameter)
        {
            Card_List = null;

            var query = DataModel.cardList.Where(x => x.setCode == "LEB")
                                          .Select(x => x)
                                          .OrderBy(x => x.name);

            Card_List = query.ToList();

        }




        // DatePicker
        private DateTime _selectedDateFrom = new DateTime(2016, 08, 01);
        public DateTime SelectedDateFrom
        {
            get { return _selectedDateFrom; }
            set
            {
                _selectedDateFrom = value;
                OnPropertyChanged();
            }
        }
        private DateTime _selectedDateUntil = DateTime.Now;
        private Filter filter;

        public DateTime SelectedDateUntil
        {
            get { return _selectedDateUntil; }
            set
            {
                _selectedDateUntil = value;
                OnPropertyChanged();
            }
        }
    }
}