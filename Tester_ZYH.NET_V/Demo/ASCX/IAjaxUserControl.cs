using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tester_ZYH.NET_V.Demo.ASCX
{
    public class PageBase : System.Web.UI.Page
    {
        public ZYH.WebControl_V.CallbackManager CallbackManager { get; set; }
    }

    public interface IAjaxUserControl
    {
        PageBase MyPage { get;  }
    }

    public interface IAjaxUserControl_Editor
    {
        Guid KeyId { get; set; }
    }
}