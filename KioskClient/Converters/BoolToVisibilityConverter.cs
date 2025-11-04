using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KioskClient.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        /*
         * [오류 수정]
         * 1. 'Convert' 메서드의 매개변수 이름에 오타가 있었습니다.
         * (targetTyep -> targetType, param -> parameter, cultrue -> culture)
         * C#은 대소문자를 구분하므로 'IValueConverter'의 시그니처와
         * 정확히 일치해야 합니다.
         */
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = value is bool b && b;

            //파라미터가 inverted일때 반전
            if (parameter != null && parameter.ToString().Equals("Inverted", StringComparison.OrdinalIgnoreCase))
            {
                boolValue = !boolValue;
            }

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}