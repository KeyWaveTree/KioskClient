using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace KioskClient.Converters
{
    /// <summary>
    /// 아이콘 이름을 이모지로 변환
    /// </summary>
    public class IconConverter : IValueConverter
    {
        // ... (Figma 예제와 동일)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string iconName)
            {
                return iconName switch
                {
                    "Coffee" => "☕",
                    "Dessert" => "🍰",
                    "Utensils" => "🍴",
                    "ShoppingBag" => "🛍️",
                    _ => "📦"
                };
            }
            return "📦";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
