using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class Set
    {
        public int BaseSetSize { get; set; }
        public string Block { get; set; }
        
        public List<Card> cards { get; set; }
    }
}
