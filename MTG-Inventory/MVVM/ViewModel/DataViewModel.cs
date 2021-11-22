using MTG_Inventory.Classes;
using MTG_Inventory.Core;
using MTG_Inventory.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MTG_Inventory.MVVM.ViewModel
{
    class DataViewModel : ObservableObject
    {
        public List<DataFile> DataFiles { get; set; }
        public string ImageFolderPath { get; set; }
        public string TotalImages { get; set; }

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

        public DataViewModel()
        {
            DataModel.CheckAllFiles();
            DataModel.ReadAllRelevantFiles();
            DataFiles = DataModel.DataFiles;
            DataModel.CheckImages();
            ImageFolderPath = DataModel.imageFolderPath;
            TotalImages = DataModel.totalImages;
        }

        public RelayCommand BTN_DownloadImages => new RelayCommand(DownloadImages);
        private void DownloadImages(object commandParameter)
        {
            DownloadImagesButtonText = "Downloading...";

            Downloader.DownloadAllMissingPictures();

            DownloadImagesButtonText = "Reload";
        }
    }
}
