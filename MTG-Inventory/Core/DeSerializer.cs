using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows;
using System;
using System.Diagnostics;
using MTG_Inventory.Classes;
using System.Collections;

namespace MTG_Inventory
{
    internal class DeSerializer
    {
        private static List<string> fileNames = new();
        private static Queue<string> folders = new();
        private static List<Card>? fullCardList = new();
        private static List<Card>? tempCardList = new();     


        public static List<Card> Process(string selection)
        {

            if (File.Exists(selection))
            {
                ProcessFile(selection);
            }
            else if (Directory.Exists(selection))
            {
                ProcessDirectory(selection);

                // Process all Files found in all directories found
                foreach (string filePath in fileNames)
                {
                    ProcessFile(selection);
                }
            }
            //else
            //{
            //    MessageBox.Show("{0} is not a valid file or directory.", selection);
            //}
            return fullCardList;
        }

        private static void ProcessDirectory(string targetDirectory)
        {
            // Add all Filepaths to files List found in this directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string file in fileEntries)
                fileNames.Add(file);

            // Recurse into all subdirectories process them.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                folders.Enqueue(subdirectory);
            while (folders.Count > 0)
            {
                ProcessDirectory(folders.Dequeue());
            }
        }

        private static void ProcessFile(string filePath)
        {
            tempCardList = null;
            GetDataListFromJsonFile(filePath);

            foreach (var item in tempCardList)
            {
                fullCardList.Add(item);
            }

            Debug.WriteLine("Processed file '{0}'.", filePath);
        }


        // Deserializer
        internal static void GetDataListFromJsonFile(string filePath)
        {
            string jsonString = ReadJsonFile(filePath); // From JSON Raw Data to JSON String
            DeSerializeJsonString(jsonString); // From JSON string to Object List 

        }

        /// <summary>
        /// Reads JSON File and removes \n
        /// </summary>
        /// <param name="fp">Path of the File read</param>
        /// <returns>String with JSON RAW Data</returns>
        internal static string ReadJsonFile(string fp)
        {
            string jsonString = File.ReadAllText(fp);

            jsonString = jsonString.Replace("\n", "");

            return jsonString;
        }

        /// <summary>
        /// Reads the RAW Data from an JSON String and makes Objects out of it
        /// </summary>
        /// <param name="s">String with JSON RAW Data</param>
        /// <returns>Collection of TimeLineObjects</returns>
        private static void DeSerializeJsonString(string jsonString)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                DateParseHandling = DateParseHandling.DateTimeOffset,
                ObjectCreationHandling = ObjectCreationHandling.Reuse
            };


            var jsonObject = JsonConvert.DeserializeObject(jsonString);





            //foreach (var dict in tempCollection.Values)
            //{
            //    foreach (var item in dict)
            //    {
            //        if (dict.Keys.ToString() == "cards")
            //        {

            //            tempCardList.Add(new Card(
            //                ));
            //        }
            //    }
            //}




            //if (tempCollection != null)
            //{
            //for (int i = 0; i < tempCollection.Cards.Count; i++)
            //{
            //    tempCardList.Add(tempCollection.Cards[i]);
            //}
            // }
        }

        
    }
}
