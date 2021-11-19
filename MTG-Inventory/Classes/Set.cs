using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class Set
    {
        [JsonProperty("baseSetSize")]
        public int baseSetSize { get; set; }

        [JsonProperty("block")]
        public string block { get; set; }
        public List<Card> cards { get; set; }
        public string code { get; set; }
        public bool isFoilOnly { get; set; }
        public bool isNonFoilOnly { get; set; }
        public bool isOnlineOnly { get; set; }
        public string keyruneCode { get; set; }
        public string name { get; set; }
        public string releaseDate { get; set; }
        public int tcgplayerGroupId { get; set; }
        public int totalSetSize { get; set; }
        public string type { get; set; }
    }
}
