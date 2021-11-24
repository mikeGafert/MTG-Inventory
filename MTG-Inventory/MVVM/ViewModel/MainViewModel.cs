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
        public RelayCommand ScannerViewCommand { get; set; }
        public RelayCommand QuitCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public InventoryViewModel InventoryVM { get; set; }
        public DataViewModel DataVM { get; set; }
        public ScannerViewModel ScannerVM { get; set; }
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

        public MainViewModel()
        {
            HomeVM ??= new HomeViewModel();
            DataVM ??= new DataViewModel();
            InventoryVM ??= new InventoryViewModel();
            AllCardsVM ??= new AllCardsViewModel();
            ScannerVM ??= new ScannerViewModel();

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

            AllCardsViewCommand = new RelayCommand(o =>
            {
                CurrentView = AllCardsVM;
            });

            ScannerViewCommand = new RelayCommand(o =>
            {
                CurrentView = ScannerVM;
            });

            QuitCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });
        }
    }
}
