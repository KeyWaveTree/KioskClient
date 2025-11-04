using System;
using System.Globalization; // [오류 수정] CultureInfo를 위해 추가
using System.Windows.Data;  // [오류 수정] IValueConverter를 위해 추가

/*
 * [오류 수정]
 * 1. 이 클래스가 'IValueConverter' 인터페이스를 구현(상속)하지 않았습니다.
 * 이것이 C# 프로젝트 전체의 빌드를 실패시켜 25개의 XAML 오류를 유발한
 * 핵심 원인입니다.
 * 2. 'IValueConverter'를 상속하고 'Convert' 및 'ConvertBack' 메서드를 구현합니다.
 */
namespace KioskClient.Converters
{
    public class CurrencyConverter : IValueConverter // [오류 수정] IValueConverter 상속
    {
        /// <summary>
        /// ViewModel(int) -> View(string)
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                // Figma 예제의 StringFormat='{}{0:N0}'과 동일한 결과
                return $"{intValue:N0}원";
            }
            return "0원";
        }

        /// <summary>
        /// [오류 수정] IValueConverter는 ConvertBack을 반드시 구현해야 합니다.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}