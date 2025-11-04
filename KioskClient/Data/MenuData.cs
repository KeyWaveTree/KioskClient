using KioskClient.Model; //Models 네임스페이스 using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KioskClient.Data
{
    /// <summary>
    /// 정적 메뉴 데이터 
    /// </summary>
    public static class MenuData
    {
        /// <summary>
        /// 메뉴 카테고리 목록 
        /// </summary>
        public static List<MenuCategory> MenuCategories { get; } = new List<MenuCategory>
        {
            new MenuCategory { Id = "1", Name = "Coffee", NameKo = "커피", IconName = "Coffee", Color = "from-amber-400 to-orange-500"},
            new MenuCategory { Id = "2", Name = "Dessert", NameKo = "디저트", IconName = "Dessert", Color = "from-pink-400 to-rose-500" },
            new MenuCategory { Id = "3", Name = "Food", NameKo = "음식", IconName = "Utensils", Color = "from-green-400 to-emerald-500" },
            new MenuCategory { Id = "4", Name = "Shopping", NameKo = "쇼핑", IconName = "ShoppingBag", Color = "from-blue-400 to-cyan-500" }
        };

        public static Dictionary<string, List<MenuProduct>> MenuProducts { get; } = new Dictionary<string, List<MenuProduct>>
        {
            ["1"] = new List<MenuProduct>
            {
                new MenuProduct { Id = "c1", Name = "Americano", NameKo = "아메리카노", Price = 4500, Image = "☕" },
                new MenuProduct { Id = "c2", Name = "Latte", NameKo = "카페라떼", Price = 5000, Image = "🥛" },
                new MenuProduct { Id = "c3", Name = "Cappuccino", NameKo = "카푸치노", Price = 5200, Image = "☕" },
                new MenuProduct { Id = "c4", Name = "Espresso", NameKo = "에스프레소", Price = 4000, Image = "☕" },
            },
            
            ["2"] = new List<MenuProduct> 
            {
                new MenuProduct { Id= "d1", Name= "Cake", NameKo= "케이크", Price= 6500, Image= "🍰" },
                new MenuProduct { Id= "d2", Name= "Cookie", NameKo= "쿠키", Price= 3000, Image= "🍪" },
                new MenuProduct { Id= "d3", Name= "Macaron", NameKo= "마카롱", Price= 3500, Image= "🧁" },
                new MenuProduct { Id= "d4", Name= "Tiramisu", NameKo= "티라미수", Price= 7000, Image= "🍰" },
            },
            
            ["3"] = new List<MenuProduct> 
            {
                new MenuProduct { Id= "f1", Name= "Sandwich", NameKo= "샌드위치", Price= 6000, Image= "🥪" },
                new MenuProduct { Id= "f2", Name= "Salad", NameKo= "샐러드", Price= 8000, Image= "🥗" },
                new MenuProduct { Id= "f3", Name= "Pasta", NameKo= "파스타", Price= 12000, Image= "🍝" },
                new MenuProduct { Id= "f4", Name= "Pizza", NameKo= "피자", Price= 15000, Image= "🍕" },
            },
            
            ["4"] = new List<MenuProduct> 
            {
                new MenuProduct { Id= "s1", Name= "Tumbler", NameKo= "텀블러", Price= 25000, Image= "🥤" },
                new MenuProduct { Id= "s2", Name= "Mug", NameKo= "머그컵", Price= 15000, Image= "☕" },
                new MenuProduct { Id= "s3", Name= "Bag", NameKo= "가방", Price= 35000, Image= "👜" },
                new MenuProduct { Id= "s4", Name= "T-Shirt", NameKo= "티셔츠", Price= 28000, Image= "👕" },
            },
        };
    }
}
