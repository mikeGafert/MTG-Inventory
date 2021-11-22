using System;
using System.Windows.Input;
using System.Windows;
using MTG_Inventory.Core;
using MTG_Inventory.MVVM.Model;

namespace MTG_Inventory.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }
        public RelayCommand DataViewCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public InventoryViewModel InventoryVM { get; set; }
        public DataViewModel DataVM { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public static HomeViewModel homeVM = new HomeViewModel();
        public static DataViewModel dataVM = new DataViewModel();
        public static InventoryViewModel inventoryVM = new InventoryViewModel();

        public MainViewModel()
        {          

            

            CurrentView = homeVM;

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = homeVM;
            });           

            InventoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = inventoryVM;
            });

            DataViewCommand = new RelayCommand(o =>
            {
                CurrentView = dataVM;
            });

            QuitCommand = new RelayCommand(o =>
            {
               Application.Current.Shutdown();
            });
        }        
    }
}
