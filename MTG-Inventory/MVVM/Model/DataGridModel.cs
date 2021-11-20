using MTG_Inventory.Classes;
using MTG_Inventory.Core;
using MTG_Inventory.MVVM.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MTG_Inventory.MVVM.Model
{
    internal class DataGridModel
    {
        private static readonly string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Systempfad zum Roaming Ordner
        private static readonly string roamingFolderPath = Path.Combine(roamingPath, "MTG-Inventory"); // Pfad zum Programmordner
        private static readonly string imageFolderPath = Path.Combine(roamingFolderPath, "CardImages"); // Pfad zum CardImages Ordner
        private static readonly List<DataFile> DataFiles = new List<DataFile>() // Create File Objects
        {
            new DataFile(
                name:"AllPrintings",
                localPath:$"{Path.Combine(roamingFolderPath, "AllPrintings.json")}",
                downloadPath:@"https://mtgjson.com/api/v5/AllPrintings.json"),
            new DataFile(
                name:"CardTypes",
                localPath:@$"{Path.Combine(roamingFolderPath, "CardTypes.json")}",
                downloadPath:@"https://mtgjson.com/api/v5/CardTypes.json")
        }; // List of all relevant Files

        public static List<Card> Generate()
        {
            // Create Programm Folder id not existing
            if (!Directory.Exists(roamingFolderPath))
            {
                Directory.CreateDirectory(roamingFolderPath);
            }

            // Create CardImages Folder id not existing
            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
            }

            // Check each file in FileList if existing and up to date. If not download/update it
            foreach (DataFile dataFile in DataFiles)
            {
                CheckAndDownloadFile(dataFile);
            }

            // Read AllPrintingsJson
            string jsonString = DeSerializer.ReadJsonFile(DataFiles[0].LocalPath);

            //Generate Card List from AllPrintingsJson
            List<Card> cardList = new();
            if (!string.IsNullOrEmpty(jsonString))
            {
                // Generate Root Object from AllPrintingsJson
                RootObject rootObject = new();
                rootObject = JsonConvert.DeserializeObject<RootObject>(jsonString);

                jsonString = "";

                List<Set> setList = rootObject.Set.Values.ToList();

                for (int i = 0; i < setList.Count; i++)
                {
                    foreach (Card card in setList[i].cards)
                    {
                        if (card.rarity != "Basic Land")
                        {
                            cardList.Add(card);
                        }
                    }
                }
            }

            DownloadImages(cardList);

            return cardList;
        }

        private static void CheckAndDownloadFile(DataFile dataFile)
        {
            if (!File.Exists(dataFile.LocalPath)) // If File AllPrintingsJson does not exist, download it!            
                DownloadDataFile(dataFile);
            else // Check if AllPrintingsJson is older than 10 Days, if so download new Version
            {
                DateTime fileCreatedDate = File.GetCreationTime(dataFile.LocalPath);
                if (fileCreatedDate < DateTime.Now.AddDays(-10))
                    DownloadDataFile(dataFile);
            }
        }

        private static void DownloadDataFile(DataFile dataFile)
        {
            using (WebClient wc = new WebClient())
            {
                //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFile(new Uri($"{dataFile.DownloadPath}"), @$"{dataFile.LocalPath}");
            }
        }

        private static void DownloadImages(List<Card> cardList)
        {
            string?[] downloadedImages = Directory.GetFiles(imageFolderPath, "*.jpg")
                                                  .Select(Path.GetFileNameWithoutExtension)
                                                  .ToArray();

            List<Card> CardListWithImagesToDownload = new();

            foreach (Card card in cardList)
            {
                if (card.rarity != "Basic Land" && 
                    card.identifiers.multiverseId != null && 
                    !downloadedImages.Contains(card.identifiers.multiverseId))
                {
                    CardListWithImagesToDownload.Add(card);
                }
            }
            Util.DownloadAllMissingPictures(CardListWithImagesToDownload, imageFolderPath);
        }

        // Event to track the progress
        //void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    progressBar.Value = e.ProgressPercentage;
        //}

    }
}
