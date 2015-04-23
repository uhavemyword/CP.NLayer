namespace CP.NLayer.Client.WpfClient.Common
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            if (targetType == typeof(Color))
            {
                return (Color)ColorConverter.ConvertFromString(s);
            }
            else if (targetType == typeof(Brush))
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(s));
            }
            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}