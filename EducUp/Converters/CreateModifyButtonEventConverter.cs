using EducUp.Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EducUp.Converters
{
    public class CreateModifyButtonEventConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;

            if (value != null && value is NewEventPageMode eventPageMode && parameter is string button && !string.IsNullOrEmpty(button))
            {
                switch (eventPageMode)
                {
                    case NewEventPageMode.NewEvent:
                        if (button.Equals("create"))
                        {
                            result = true;
                        }
                        else if (button.Equals("modify"))
                        {
                            result = false;
                        }
                        break;

                    case NewEventPageMode.ModifyEvent:
                        if (button.Equals("create"))
                        {
                            result = false;
                        }
                        else if (button.Equals("modify"))
                        {
                            result = true;
                        }
                        break;

                    default:
                        break;
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
