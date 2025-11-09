using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace KioskClient.Converters
{
    /// <summary>
    /// [오류 수정] MainWindow의 홈 버튼 활성/비활성화를 위한 컨버터
    /// (Null이 아니면 True 반환)
    /// </summary>
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 값이 Null이 아니면 true (활성화), Null이면 false (비활성화)
            bool isNotNUll = value != null;
            if(targetType == typeof(Visibility))
            {
                bool shouldInvert = parameter?.ToString() != "False";
                if (shouldInvert) return isNotNUll ? Visibility.Collapsed : Visibility.Visible;
                else return isNotNUll ? Visibility.Visible : Visibility.Collapsed;
            }

            return isNotNUll;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
