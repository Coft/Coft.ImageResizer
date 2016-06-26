using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using static Coft.ImageResizer.Models.Helpers.Enums;

namespace Coft.ImageResizer.WPFClient.Converters
{
    class ProcessingStatusToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Collapsed;

            ProcessingStatus inputStatus = (ProcessingStatus)value;
            ProcessingStatus expectedStatus = (ProcessingStatus)parameter;

            if (inputStatus == expectedStatus)
            {
                visibility = Visibility.Visible;
            }

            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
