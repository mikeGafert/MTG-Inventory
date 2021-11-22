using MTG_Inventory.Classes;
using MTG_Inventory.MVVM.Model;
using MTG_Inventory.MVVM.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTG_Inventory.Core
{
    internal class Downloader
    {
        /// <summary>
        /// Download all missing Pictures
        /// </summary>
        #region        

        // Code from My Question on https://stackoverflow.com/questions/70049316/tying-to-download-huge-amount-of-files-more-efficent-in-c-sharp

        private static readonly string _baseUrlPattern = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid={0}&type=card";

        private static readonly HttpClient _httpClient = new HttpClient();

        internal static void DownloadAllMissingPictures(CancellationToken cancellationToken = default)
        {
            using (_httpClient)
            {
                ServicePointManager.DefaultConnectionLimit = 8;

                var parallelOptions = new ParallelOptions()
                {
                    MaxDegreeOfParallelism = 10,
                    CancellationToken = cancellationToken,
                };
                Parallel.ForEachAsync(DataModel.CardListWithImagesToDownload, parallelOptions, async (image, ct) =>
                {
                    string imageId = image.identifiers.multiverseId;
                    string url = String.Format(_baseUrlPattern, imageId);
                    string filePath = Path.Combine(DataModel.imageFolderPath, $"{imageId}.jpg");

                    using HttpResponseMessage response = await _httpClient.GetAsync(url, ct);
                    response.EnsureSuccessStatusCode();

                    using FileStream fileStream = File.OpenWrite(filePath);
                    await response.Content.CopyToAsync(fileStream);
                }).Wait();
            }
        }

        //My original Code
        //internal static void DownloadAllMissingPictures()
        //{
        //    HttpClientHandler handler = new HttpClientHandler();
        //    handler.MaxConnectionsPerServer = 8;
        //    HttpClient _httpClient2 = new HttpClient(handler);

        //    Parallel.ForEach(Partitioner.Create(0, DataModel.CardListWithImagesToDownload.Count), async range =>
        //    {
        //        for (var i = range.Item1; i < range.Item2; i++)
        //        {
        //            string multiverseID = DataModel.CardListWithImagesToDownload[i].identifiers.multiverseId;

        //            string url = String.Format(_baseUrlPattern, multiverseID);
        //            string filePath = String.Format(@"{0}\{1}.jpg", DataModel.imageFolderPath, multiverseID);

        //            using HttpResponseMessage response = await _httpClient2.GetAsync(url);
        //            response.EnsureSuccessStatusCode();

        //            using FileStream fileStream = File.OpenWrite(filePath);
        //            await response.Content.CopyToAsync(fileStream);
        //        }
        //    });
        //}
        #endregion

        public static async Task DownloadDataFileAsync(int i)
        {
            using (_httpClient)
            {
                Uri uri = new Uri(DataModel.DataFiles[i].DownloadPath);
                var response = await _httpClient.GetAsync(uri);
                using (var fs = new FileStream(@$"{DataModel.DataFiles[i].LocalPath}",
                    FileMode.CreateNew))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }
        }
    }
}
