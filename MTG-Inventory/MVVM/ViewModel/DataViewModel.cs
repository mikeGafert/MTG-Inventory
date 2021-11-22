using MTG_Inventory.Classes;
using MTG_Inventory.Core;
using MTG_Inventory.MVVM.Model;
using MTG_Inventory.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.IO;

namespace MTG_Inventory.MVVM.ViewModel
{
    class DataViewModel : ObservableObject
    {
        public List<DataFile> DataFiles { get; set; }
        public string ImageFolderPath { get; set; }

        // AllDataJson Info Block
        private int _pGBar_AllDataJsonPath = 0;
        public int PGBar_AllDataJsonPath
        {
            get { return _pGBar_AllDataJsonPath; }
            set
            {
                _pGBar_AllDataJsonPath = value;
                OnPropertyChanged();
            }
        }

        // CarTypesJson Info Block
        private int _pGBar_CardTypesPath = 0;
        public int PGBar_CardTypesPath
        {
            get { return _pGBar_CardTypesPath; }
            set
            {
                _pGBar_CardTypesPath = value;
                OnPropertyChanged();
            }
        }

        // Image Info Block
        private int _pGBar_ImageFolderPath = 0;
        public int PGBar_ImageFolderPath
        {
            get { return _pGBar_ImageFolderPath; }
            set 
            { 
                _pGBar_ImageFolderPath = value;
                OnPropertyChanged();
            }
        }
        private int _downloadedImagesCount = 0;
        public int DownloadedImagesCount
        {
            get { return _downloadedImagesCount; }
            set
            {
                _downloadedImagesCount = value;
                OnPropertyChanged();
            }
        }
        private int _cardListWithImagesToDownloadCount = 100;
        public int CardListWithImagesToDownloadCount
        {
            get { return _cardListWithImagesToDownloadCount; }
            set
            {
                _cardListWithImagesToDownloadCount = value;
                OnPropertyChanged();

            }
        }
        private string _totalImages;
        public string TotalImages
        {
            get { return _totalImages; }
            set
            {
                _totalImages = value;
                OnPropertyChanged();
            }
        }
        private string _downloadImagesButtonText = "Download";
        public string DownloadImagesButtonText
        {
            get { return _downloadImagesButtonText; }
            set
            {
                _downloadImagesButtonText = value;
                OnPropertyChanged();
            }
        }
        private string _checkImagesButtonText = "Check images";

        public string CheckImagesButtonText
        {
            get { return _checkImagesButtonText; }
            set
            {
                _checkImagesButtonText = value;
                OnPropertyChanged();
            }
        }

        public DataViewModel()
        {
            DataModel.CheckAllFiles();
            DataModel.ReadAllRelevantFiles();
            //DataModel.CheckImages(); // Not relevant in Programm start sequence

            DataFiles = DataModel.DataFiles;
            if (DataFiles[0].IsExisting)
                PGBar_AllDataJsonPath = 1;
            if (DataFiles[1].IsExisting)
                PGBar_CardTypesPath = 1;
            if (!Directory.Exists(DataModel.imageFolderPath))
                PGBar_ImageFolderPath = 1;            
            ImageFolderPath = DataModel.imageFolderPath;
            TotalImages = DataModel.totalImages;
        }


        public RelayCommand Click_CheckImages => new RelayCommand(CheckImages);
        private void CheckImages(object commandParameter)
        {
            CheckImagesButtonText = "Checking...";

            (TotalImages, DownloadedImagesCount, CardListWithImagesToDownloadCount) = DataModel.CheckImages();

            CheckImagesButtonText = "Checked";

        }

        public RelayCommand Click_DownloadImages => new RelayCommand(DownloadImages);
        private void DownloadImages(object commandParameter)
        {
            DownloadImagesButtonText = "Downloading...";

            Downloader.DownloadAllMissingPictures();

            DownloadImagesButtonText = "Reload";
        }
    }
}
