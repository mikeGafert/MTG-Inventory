using MTG_Inventory.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MTG_Inventory.Classes;
using MTG_Inventory.MVVM.Model;
using MTG_Inventory.MVVM.View;

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

        private void PerformLoad()
        {
            var query = DataModel.cardList.Where(x => x.setCode == "LEA")
                                          .Select(x => x)
                                          .OrderBy(x => x.name);

            Card_List = query.ToList();
        }

        // Button Commands        
        // Perform Selection        
        public RelayCommand Select => new RelayCommand(PerformSelect);
        private void PerformSelect(object commandParameter)
        {
            // Get all Filter Values


            var query = DataModel.cardList.Where(x => x.setCode == "LEA")
                                          .Select(x => x)
                                          .OrderBy(x => x.name);

            Card_List = query.ToList();
        }

        public RelayCommand BTN_Reset => new RelayCommand(PerformReset);
        private void PerformReset(object commandParameter)
        {

            var query = DataModel.cardList.Where((x) => x.name != null)
                                 .Select((x) => x);

            Card_List = query.ToList();

        }

        

        // All Filter options
        // Color Filter
        List<string> Color_List { get; set; }

        private bool _color_White = false;
        public bool Color_White
        {
            get => _color_White; 
            set
            {
                _color_White = value;

                //if (!value)
                //{
                //    for (int i = 0; i < Color_List.Count; i++)
                //    {
                //        if (Color_List[i].Equals("W"))
                //            Color_List.RemoveAt(i);
                //    }
                //}
                //else
                //{
                //    Color_List.Add("W");
                //}

                OnPropertyChanged();
            }
        }

        private bool _color_Red = false;
        public bool Color_Red
        {
            get { return _color_Red; }
            set
            {
                _color_Red = value;
                OnPropertyChanged();
            }
        }

        private bool _color_Blue = false;
        public bool Color_Blue
        {
            get { return _color_Blue; }
            set
            {
                _color_Blue = value;
                OnPropertyChanged();
            }
        }

        private bool _color_Black = false;
        public bool Color_Black
        {
            get { return _color_Black; }
            set
            {
                _color_Black = value;
                OnPropertyChanged();
            }
        }

        private bool _color_Green = false;
        public bool Color_Green
        {
            get { return _color_Green; }
            set
            {
                _color_Green = value;
                OnPropertyChanged();
            }
        }

        private bool _color_Colorless = false;
        public bool Color_Colorless
        {
            get { return _color_Colorless; }
            set
            {
                _color_Colorless = value;
                OnPropertyChanged();
            }
        }

        // Card Types Filter


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