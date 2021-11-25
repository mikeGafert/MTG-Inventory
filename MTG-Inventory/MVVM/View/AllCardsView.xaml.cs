using MTG_Inventory.Classes;
using MTG_Inventory.MVVM.Model;
using MTG_Inventory.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MTG_Inventory.MVVM.View
{
    /// <summary>
    /// Interaction logic for AllCardsView.xaml
    /// </summary>
    public partial class AllCardsView : UserControl
    {
        internal static Filter filter { get; set; } = new();
        public AllCardsView()
        {
            InitializeComponent();
        }

        public void Color_Checkbox_Checked(object sender, EventArgs e)
        {
            char chbxName = ((CheckBox)sender).Name.ToUpper().Last();

            filter.ColorFilterList.Add(chbxName);
        }
        public void Color_Checkbox_Unchecked(object sender, EventArgs e)
        {
            char chbxName = ((CheckBox)sender).Name.ToUpper().Last();

            for (int i = 0; i < filter.ColorFilterList.Count; i++)
            {
                if (filter.ColorFilterList[i].Equals(chbxName))
                    filter.ColorFilterList.RemoveAt(i);
            }
        }

        public void CardTypes_Checkbox_Checked(object sender, EventArgs e)
        {
            string chbStringNumber = ((CheckBox)sender).Name.Substring(((CheckBox)sender).Name.Length - 2);
            int chbNumber = Convert.ToInt32(chbStringNumber);

            filter.CardTypeFilterList.Add(DataModel.cardTypes[chbNumber]);
        }
        public void CardTypes_Checkbox_Unchecked(object sender, EventArgs e)
        {
            string chbStringNumber = ((CheckBox)sender).Name.Substring(((CheckBox)sender).Name.Length - 2);
            int chbNumber = Convert.ToInt32(chbStringNumber);

            for (int i = 0; i < filter.CardTypeFilterList.Count; i++)
            {
                if (filter.CardTypeFilterList[i].Equals(DataModel.cardTypes[chbNumber]))
                    filter.CardTypeFilterList.RemoveAt(i);
            }
        }

        private void CardSearch_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txtbx= ((TextBox)sender);

            if (txtbx.Text.Length > 2)
            {
                filter.CardSearchText = txtbx.Text;
            }
        }

        private void Title_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox txtbx = ((TextBox)sender);

            if (txtbx.Text.Length > 2)
            {
                filter.TitleSearchText = txtbx.Text;                
            }
        }        
    }
}
