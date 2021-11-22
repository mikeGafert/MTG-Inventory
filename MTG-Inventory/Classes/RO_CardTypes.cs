using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    class RO_CardTypes
    {
        [JsonProperty("data")]
        public Dictionary<string, object> CardTypes { get; set; }
    }
}
