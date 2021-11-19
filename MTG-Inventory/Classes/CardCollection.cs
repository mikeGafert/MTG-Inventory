using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class CardCollection
    {
        private List<Card> cards;

        public List<Card> Cards { get => cards; set => cards = value; }
    }
}
