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
        public RelayCommand AllCardsViewCommand { get; set; }
        public RelayCommand DataViewCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public InventoryViewModel InventoryVM { get; set; }
        public DataViewModel DataVM { get; set; }
        public AllCardsViewModel AllCardsVM { get; set; }

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

        public static HomeViewModel homeVM;
        public static DataViewModel dataVM;
        public static InventoryViewModel inventoryVM;
        public static AllCardsViewModel allCardsVM;

        public MainViewModel()
        {
            homeVM ??= new HomeViewModel();
            dataVM ??= new DataViewModel();
            inventoryVM ??= new InventoryViewModel();
            allCardsVM ??= new AllCardsViewModel();

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

            AllCardsViewCommand = new RelayCommand(o =>
            {
                CurrentView = allCardsVM;
            });

            QuitCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });
        }
    }
}
