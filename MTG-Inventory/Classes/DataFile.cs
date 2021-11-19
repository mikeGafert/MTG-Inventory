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
        public DataFile(string name, string localPath, string downloadPath)
        {
            Name = name;
            LocalPath = localPath;
            DownloadPath = downloadPath;
        }
    }
}
