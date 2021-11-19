using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MTG_Inventory.Core
{
    public class DateTimeToString : IValueConverter
    {
        private string _format = "dd-MM-yy";
        public string Format { get => _format; set => _format = value; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DateTime DateTimeValue = (DateTime)value;
                return DateTimeValue.ToString(Format);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string strValue = value.ToString();
                DateTime DateTimeValue;
                if (DateTime.TryParse(strValue, out DateTimeValue))
                    return DateTimeValue;
                return strValue;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
    public class UnixTimeStamp : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                double unixTimeStamp = System.Convert.ToDouble(value);
                System.DateTime result = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                result = result.AddSeconds(unixTimeStamp).ToLocalTime();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                long longDateTime = System.Convert.ToInt64(dtDateTime);
                long unixTimeStamp = System.Convert.ToInt64(value) - longDateTime;
                return unixTimeStamp;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
