using MTG_Inventory.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace MTG_Inventory.Core
{
    internal class Util
    {
        // Download all missing Pictures
        private static readonly string baseUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid={0}&type=card";
        internal static void DownloadAllMissingPictures(List<Card> CardListWithImagesToDownload, string imageFolderPath)
        {
            Parallel.ForEach(Partitioner.Create(0, CardListWithImagesToDownload.Count), range =>
            {
                for (var i = range.Item1; i < range.Item2; i++)
                {
                    string multiverseID = CardListWithImagesToDownload[i].identifiers.multiverseId;

                    using (var webClient = new WebClient())
                    {
                        string url = String.Format(baseUrl, multiverseID);
                        string file = String.Format(@"{0}\{1}.jpg", imageFolderPath, CardListWithImagesToDownload[i].identifiers.multiverseId);

                        byte[] data = webClient.DownloadData(url);

                        using (MemoryStream mem = new MemoryStream(data))
                        {
                            using (var image = Image.FromStream(mem))
                            {
                                image.Save(file, ImageFormat.Jpeg);
                            }
                        }                    
                    }
                }
            });
        }


    }
}