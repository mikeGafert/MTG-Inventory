using System;
using System.Windows.Input;
using System.Windows;
using MTG_Inventory.Core;

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

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            InventoryVM = new InventoryViewModel();
            DataVM = new DataViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => 
            {
                CurrentView = HomeVM;
            });           

            InventoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = InventoryVM;
            });

            DataViewCommand = new RelayCommand(o =>
            {
                CurrentView = DataVM;
            });

            QuitCommand = new RelayCommand(o =>
            {
               Application.Current.Shutdown();
            });
        }        
    }
}
