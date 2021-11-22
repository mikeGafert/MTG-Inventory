using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class DataFile
    {
        public string Name { get; set; }
        public string LocalPath { get; set; }
        public string DownloadPath { get; set; }
        public bool IsExisting { get; set; }
        public string FileCreationTime { get; set; }
        public string DateOfFileCreation { get; set; }
        private DateTime _fileCreationDate;
        public DateTime FileCreationDate
        {
            get { return _fileCreationDate; }
            set 
            { 
                _fileCreationDate = value;
                FileCreationTime = $"{value.Hour}:{value.Minute}";
                DateOfFileCreation = $"{value.Month}/{value.Day}/{value.Year}";
            }
        }
        public string FileSize { get; set; }
        public bool IsUpToDate { get; set; }
        public string FileVersion { get; set; }
        public bool FailedToDownload { get; set; }
        public string Ex { get; set; }


        public DataFile(string name, string localPath, string downloadPath)
        {
            Name = name;
            LocalPath = localPath;
            DownloadPath = downloadPath;
        }
    }
}
