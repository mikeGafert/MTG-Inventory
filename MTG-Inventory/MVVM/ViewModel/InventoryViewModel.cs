using MTG_Inventory.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MTG_Inventory.Classes;
using MTG_Inventory.MVVM.Model;

namespace MTG_Inventory.MVVM.ViewModel
{
    class InventoryViewModel : ObservableObject
    {
        //private static readonly string myDocumentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //private static readonly string dataFolderPath = Path.Combine(myDocumentsFolderPath, "My Google Location History"); // Pfad zum Programmordner
        private static readonly string dataFolderPath = @".\Data\AllPrintingsFormatted.json";

        private static List<Card> cardList = new();

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
        private List<string> cardTypes;
        public List<string> CardTypes { get => cardTypes; set => cardTypes = value; }

        // Constructor
        public InventoryViewModel()
        {
            //if (!Directory.Exists(dataFolderPath))
            //    Directory.CreateDirectory(dataFolderPath); // Falls er nicht existiert, erstellen

            // LoadSampleData();
            CardTypes = DeSerializer.ReadCardTypes();
            cardList = InventoryModel.Generate();

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

        // Button Commands
        private IEnumerable<Card> _JsonData; // All Data from JSON Files
        public RelayCommand BTN_LoadData => new RelayCommand(PerformLoad);
        private void PerformLoad(object commandParameter)
        {
            ButtonLoadText = "Loading...";            

            _JsonData = DeSerializer.Process(dataFolderPath);

            var query = _JsonData.Where(x => x.name != null)
                                 .Select(x => x)
                                 .OrderBy(x => x.name);

            Card_List = query.ToList();

            ButtonLoadText = "Reload";           
        }

        // Perforn Selection
        public RelayCommand BTN_Select => new RelayCommand(PerformSelect);
        private void PerformSelect(object commandParameter)
        {
            if (_JsonData == null)
            {
                MessageBox.Show("Please Load Data first.", "No Data found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //else
            //{
            //    var query = _JsonData.Where((x) => x.name != null
            //                                    && x.baseSetSize != 0
            //                                    && x.releaseDate > SelectedDateFrom
            //                                    && x.releaseDate < SelectedDateUntil)
            //                        .Select((x) => x);
            //    TLO_List = query.ToList();
            //}
        }

        public RelayCommand BTN_Reset => new RelayCommand(PerformReset);
        private void PerformReset(object commandParameter)
        {
            if (_JsonData == null)
            {
                MessageBox.Show("Please Load Data first.", "No Data found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var query = _JsonData.Where((x) => x.name != null)
                                     .Select((x) => x);

                Card_List = query.ToList();
            }
        }


        // Load or Reload Data
        private string _buttonLoadText = "Load";
        public string ButtonLoadText
        {
            get { return _buttonLoadText; }
            set
            {
                _buttonLoadText = value;
                OnPropertyChanged();
            }
        }  

        
        private void LoadSampleData()
        {
            _JsonData = DeSerializer.Process(dataFolderPath);

            var query = _JsonData.Where(x => x.name != null)
                                 .Select(x => x)
                                 .OrderBy(x => x.name);

            Card_List = query.ToList();
        }
    }
}