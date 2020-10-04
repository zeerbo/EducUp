using EducUp.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EducUp.Converters
{
    public class MasterPageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value != null && value is Type type && type != null)
            {
                if (type.Equals(typeof(ProfilePage)))
                {
                    result = "C";
                }
                else if (type.Equals(typeof(StepPage)))
                {
                    result = "D";
                }
                else if (type.Equals(typeof(EventsListTabbedPage)))
                {
                    result = "F";
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
