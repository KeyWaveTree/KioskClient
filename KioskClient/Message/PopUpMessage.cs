using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient.Message
{

    //View 폴더에 Popup 관련 창이 생길때 마다 enum에 추가 
    public enum PopUpName { ShowPurchase, Show360, Close }
    public class PopUpMessage(PopUpName popUpName, object? param1 = null, object? param2 = null)
    {
        public PopUpName PopUpName { get; private set; } = popUpName;
        public object? Param1 { get; private set; } = param1;
        public object? Param2 { get; private set; } = param2;
        public string? ReturnString { get; set; } = "";
    }
}
