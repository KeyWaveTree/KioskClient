using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient.Message
{

    //View 폴더에 Popup 관련 창이 생길때 마다 enum에 추가 
    public enum PopUpName {PopupTest, Close}
    public class PopUpMessage(PopUpName popUpName, object? param1 = null, object? param2 = null)
    {
        public PopUpName popUpName { get; private set; } = popUpName;
        public object? param1 { get; private set; } = param1;
        public object? param2 { get; private set; } = param2;
        public string? returnString { get; set; } = "";
    }
}
