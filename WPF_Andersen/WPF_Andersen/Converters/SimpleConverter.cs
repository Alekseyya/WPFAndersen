using System;
using System.Windows.Data;

namespace WPF_Andersen.Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    class SimpleConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int returnedValue;

            if (int.TryParse((string)value, out returnedValue))
            {
                return returnedValue;
            }
  //<--------          //Вот тут не знаю как вместо эксемшена на форме предупредения кидать
            throw new Exception("The text is not a number");
        }
    }
}
