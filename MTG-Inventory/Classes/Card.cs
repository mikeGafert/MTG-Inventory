using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class Card
    {
        public string artist { get; set; }
        public List<string> availability { get; set; }
        public string borderColor { get; set; }
        public List<string> colorIdentity { get; set; }
        public List<string> colors { get; set; }
        public double convertedManaCost { get; set; }
        public int edhrecRank { get; set; }
        public List<string> finishes { get; set; }
        public List<ForeignData> foreignData { get; set; }
        public string frameVersion { get; set; }
        public bool hasFoil { get; set; }
        public bool hasNonFoil { get; set; }
        public Identifiers identifiers { get; set; }
        public bool isReprint { get; set; }
        public List<string> keywords { get; set; }
        public string layout { get; set; }
        public Legalities legalities { get; set; }
        public string manaCost { get; set; }
        public double manaValue { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string originalText { get; set; }
        public string originalType { get; set; }
        public string power { get; set; }
        public List<string> printings { get; set; }
        public PurchaseUrls purchaseUrls { get; set; }
        public string rarity { get; set; }
        public List<Ruling> rulings { get; set; }
        public string setCode { get; set; }
        public List<string> subtypes { get; set; }
        public List<string> supertypes { get; set; }
        public string text { get; set; }
        public string toughness { get; set; }
        public string type { get; set; }
        public List<string> types { get; set; }
        public string uuid { get; set; }
        public List<string> variations { get; set; }
        public string flavorText { get; set; }
        public bool? isAlternative { get; set; }
        public LeadershipSkills leadershipSkills { get; set; }
    }
}
