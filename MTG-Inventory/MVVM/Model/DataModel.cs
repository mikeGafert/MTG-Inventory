using MTG_Inventory.Classes;
using MTG_Inventory.Core;
using MTG_Inventory.MVVM.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static MTG_Inventory.Core.Util;

namespace MTG_Inventory.MVVM.Model
{
    internal class DataModel
    {
        private static readonly string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Systempfad zum Roaming Ordner
        private static readonly string roamingFolderPath = Path.Combine(roamingPath, "MTG-Inventory"); // Pfad zum Programmordner
        public static readonly string imageFolderPath = Path.Combine(roamingFolderPath, "CardImages"); // Pfad zum CardImages Ordner
        // List of all relevant Files
        public static readonly List<DataFile> DataFiles = new List<DataFile>()
        {
            new DataFile(
                name:"AllPrintings",
                localPath:$"{Path.Combine(roamingFolderPath, "AllPrintings.json")}",
                downloadPath:@"https://mtgjson.com/api/v5/AllPrintings.json"),
            new DataFile(
                name:"CardTypes",
                localPath:@$"{Path.Combine(roamingFolderPath, "CardTypes.json")}",
                downloadPath:@"https://mtgjson.com/api/v5/CardTypes.json")
        };
        public static string jsonString = "";

        // Global Data
        public static List<Card> cardList = new();
        public static List<string> cardTypes = new();
        public static List<string?> downloadedImages = new();
        public static List<Card> CardListWithImagesToDownload = new();
        public static string totalImages;
        public static int totalImagesCount;

        public static void CheckAllFiles()
        {
            // Create Programm Folder if not existing
            if (!Directory.Exists(roamingFolderPath))
                Directory.CreateDirectory(roamingFolderPath);


            // Create CardImages Folder if not existing
            if (!Directory.Exists(imageFolderPath))
                Directory.CreateDirectory(imageFolderPath);


            // Check each file in FileList if existing and up to date.
            List<DataFile> DataFilesToDownload = new();
            for (int i = 0; i < DataFiles.Count; i++)
            {
                if (File.Exists(DataFiles[i].LocalPath))
                {
                    DataFiles[i].IsExisting = true;

                    DataFiles[i].FileCreationDate = File.GetCreationTime(DataFiles[i].LocalPath);
                    DataFiles[i].IsUpToDate = DataFiles[i].FileCreationDate < DateTime.Now.AddDays(-10) ? true : false;

                    DataFiles[i].FileSize = FileSizeFormatter.FormatSize(new FileInfo(DataFiles[i].LocalPath).Length);
                }
                else
                {
                    try
                    {
                        DataFilesToDownload.Add(DataFiles[i]);

                        DataFiles[i].IsExisting = true;
                        DataFiles[i].IsUpToDate = true;
                    }
                    catch (Exception ex)
                    {
                        DataFiles[i].Ex = ex.Message;
                        DataFiles[i].FailedToDownload = true;
                        DataFiles[i].IsExisting = false;
                        DataFiles[i].IsUpToDate = false;
                    }
                }
            }
            if (DataFilesToDownload.Count > 0)
            {
                _ = Downloader.DownloadDataFileAsync(DataFilesToDownload);
            }
        }

        public static void ReadAllRelevantFiles()
        {
            for (int i = 0; i < DataFiles.Count; i++)
            {
                if (DataFiles[i].IsExisting)
                {
                    //jsonString = DeSerializer.ReadJsonFile(DataFiles[i].LocalPath);
                    jsonString = File.ReadAllText(DataFiles[i].LocalPath);

                    GenerateObjectsFromData(i);
                }
            }
        }

        public static void GenerateObjectsFromData(int i)
        {
            //Generate Card List from AllPrintingsJson
            if (DataFiles[i].Name == DataFiles[0].Name)
            {
                if (!string.IsNullOrEmpty(jsonString))
                {
                    // Generate Root Object from AllPrintingsJson
                    RO_Cards rootObject = new();
                    rootObject = JsonConvert.DeserializeObject<RO_Cards>(jsonString);

                    jsonString = "";

                    List<Set> setList = rootObject.Set.Values.ToList();

                    for (int j = 0; j < setList.Count; j++)
                    {
                        foreach (Card card in setList[j].cards)
                        {
                            if (card.rarity != "Basic Land")
                            {
                                cardList.Add(card);
                            }
                        }
                    }
                }
            }

            //Generate CardTypes List from CardTypesJson
            else if (DataFiles[i].Name == DataFiles[1].Name)
            {
                RO_CardTypes rootObject = new();

                rootObject = JsonConvert.DeserializeObject<RO_CardTypes>(jsonString);

                jsonString = "";

                foreach (var item in rootObject.CardTypes.Keys)
                {
                    cardTypes.Add(item.ToLower());
                }
            }
        }

        public static (string, int, int) CheckImages()
        {
            downloadedImages = Directory.GetFiles(imageFolderPath, "*.jpg")
                                        .Select(Path.GetFileNameWithoutExtension)
                                        .ToList();

            foreach (Card card in cardList)
            {
                if (card.rarity != "Basic Land" &&
                    card.identifiers.multiverseId != null &&
                    !downloadedImages.Contains(card.identifiers.multiverseId))
                {
                    CardListWithImagesToDownload.Add(card);
                }
            }

            if (downloadedImages.Count != CardListWithImagesToDownload.Count)
            {
                totalImagesCount = downloadedImages.Count;
                totalImages = $"{downloadedImages.Count} Pictures of {downloadedImages.Count + CardListWithImagesToDownload.Count} Pictures downloaded.";
            }
            else
            {
                totalImages = $"All {downloadedImages.Count} Pistures downloaded.";
            }
            return (totalImages, downloadedImages.Count, CardListWithImagesToDownload.Count);
        }
    }
}
