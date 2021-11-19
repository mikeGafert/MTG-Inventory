using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTG_Inventory.Classes
{
    internal class Meta
    {
        private DateTime date;
        private string version;
        public DateTime Date { get => date; set => date = value; }
        public string Version { get => version; set => version = value; }     

        public Meta(string date, string version)
        {
            Date = Convert.ToDateTime(date);
            Version = version;
        }
    }
}
