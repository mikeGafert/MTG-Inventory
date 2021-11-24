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
        private IEnumerable<Card> _card_List; // DataList the DataGrid is working with
        public IEnumerable<Card> Card_List
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

        // Constructor
        public AllCardsViewModel()
        {   
            CardTypes = DataModel.cardTypes;

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
        private void PerformSelect(object commandParameter)
        {
            // Get all Filter Values


            var query = DataModel.cardList.Where(x => x.setCode == "LEA" &&
                                             x.colors.Contains(  )
                                          .Select(x => x)
                                          .OrderBy(x => x.name);

            Card_List = query.ToList();
        }

        // Button Reset
        public RelayCommand BTN_Reset => new RelayCommand(PerformReset);
        private void PerformReset(object commandParameter)
        {

            var query = DataModel.cardList.Where((x) => x.name != null)
                                 .Select((x) => x);

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