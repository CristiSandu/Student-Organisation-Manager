using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace StudentOrganisation.Convertors
{
    class DescriptionConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string val = value.ToString();
                if (val.Length <= 30)
                    return $"{val.Substring(0, Math.Min(val.Length, 30))}";
                else
                    return $"{val.Substring(0, Math.Min(val.Length, 29))}...";

            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
