using System;
using System.Globalization;
using System.Windows.Data;

namespace Volo.Abp.Desktop.UI.Converters
{
    public class BoolToYesNoStrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && bool.TryParse(value.ToString(), out bool result))
            {
                if (result)
                    return "是";
                else
                    return "否";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}