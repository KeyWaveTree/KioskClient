using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient.Model
{
    /// <summary>
    /// 개별 상품 데이터 정의
    /// </summary>
    public class MenuProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NameKo { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
