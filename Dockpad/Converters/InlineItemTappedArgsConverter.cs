﻿using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Dockpad.Converters
{
    class InlineItemTappedArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var inlineitemTappedEventArgs = value as InlineItemTappedEventArgs;
            if (inlineitemTappedEventArgs == null)
            {
                throw new ArgumentException("Expected value to be of type InlineItemTappedEventArgs", nameof(value));
            }
            return inlineitemTappedEventArgs.InlineEvent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

