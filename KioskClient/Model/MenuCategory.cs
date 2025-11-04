using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient.Model
{
    /// <summary>
    /// 카테고리 데이터를 정의
    /// </summary>
    public class MenuCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameKo { get; set; }
        public string IconName { get; set; }
        public string Color { get; set; }
    }
}
