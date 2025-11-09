using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient.Message
{
    /// <summary>
    /// 이동할 페이지 정의
    /// </summary>
    public enum PageName
    {
        Welcome,
        Choice,
        Menu,
    }

    public class GoToPageMessage
    {
        public PageName PageName { get; private set; }
        public object? Param { get; private set; }

        //pageName : 일반적인 페이지 이동
        // + _param : 파라미터를 참조하는 페이지 이동
        public GoToPageMessage(PageName pageName, object? _param = null)
        { 
            this.PageName = pageName;
            this.Param = _param;
        }

        
        public class UpdateMessage
        {

        }
    }
}
