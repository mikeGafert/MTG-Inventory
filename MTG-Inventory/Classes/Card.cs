using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class Card
    {            

        public string Artist { get; set; }
        public List<string> Availability { get; set; }
        public string BorderColor { get; set; }
        public List<string> ColorIdentity { get; set; }
        public List<string> Colors { get; set ; }
        public double ConvertedManaCost { get; set; }
        public int EdhrecRank { get; set; }
        public List<string> Finishes { get; set; }
        public List<ForeignData> ForeignData { get; set; }
        public string FrameVersion { get; set; }
        public bool HasFoil { get; set; }
        public bool HasNonFoil { get; set; }
        public Identifiers Identifiers { get; set; }
        public bool IsReprint { get; set; }
        public List<string> Keywords { get; set; }
        public string Layout { get; set; }
        public Legalities Legalities { get; set; }
        public string ManaCost { get; set; }
        public double ManaValue { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string OriginalText { get; set; }
        public string OriginalType { get; set; }
        public string Power { get; set; }
        public List<string> Printings { get; set; }
        public PurchaseUrls PurchaseUrls { get; set; }
        public string Rarity { get; set; }
        public List<object> Rulings { get; set; }
        public string SetCode { get; set; }
        public List<string> Subtypes { get; set; }
        public List<object> Supertypes { get; set; }
        public string Text { get; set; }
        public string Toughness { get; set; }
        public string Type { get; set; }
        public List<string> Types { get; set; }
        public string Uuid { get; set; }
        public List<string> Variations { get; set; }
        public string FlavorText { get; set; }

        
    }
}
