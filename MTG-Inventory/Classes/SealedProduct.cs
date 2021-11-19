using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class SealedProduct
    {
        public Identifiers identifiers { get; set; }
        public string name { get; set; }
        public PurchaseUrls purchaseUrls { get; set; }
        public string releaseDate { get; set; }
        public string uuid { get; set; }
    }
}
