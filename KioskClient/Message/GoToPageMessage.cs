using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient.Message
{
    public enum PageName { Test }

    public class GoToPageMessage
    {
        public PageName pageName { get; private set; }
        public object? param { get; private set; }

        //pageName : 일반적인 페이지 이동
        // + _param : 파라미터를 참조하는 페이지 이동
        public GoToPageMessage(PageName pageName, object? _param = null)
        { 
            this.pageName = pageName;
            this.param = _param;
        }

        
        public class UpdateMessage
        {

        }
    }
}
