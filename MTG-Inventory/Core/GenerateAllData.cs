using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MTG_Inventory.Classes;
using Newtonsoft.Json;

namespace MTG_Inventory.Core
{
    internal class GenerateAllData
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
        };

        public static List<Card> Generate()
        {
            if (!Directory.Exists(roamingFolderPath)) // Falls er nicht existiert, erstellen
            {
                Directory.CreateDirectory(roamingFolderPath);
            }

            foreach (DataFile dataFile in DataFiles)
            {
                CheckAndDownloadFile(dataFile);
            }

            // Read AllPrintingsJson
            string jsonString = DeSerializer.ReadJsonFile(DataFiles[0].LocalPath);

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
           
        private static readonly string BASE_URL = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid={temp}&type=card";
        private static void DownloadImages()
        {
            string? [] downloaded = Directory.GetFiles(imageFolderPath, "*.jpg")
                                             .Select(Path.GetFileName)
                                             .ToArray();

        }

        // Event to track the progress
        //void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    progressBar.Value = e.ProgressPercentage;
        //}
    }
}