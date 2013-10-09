using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace ZYH.WebControl_V
{
    [ToolboxItem(false)]
    public class TestLink : WebControl, ICallbackEventHandler
    {
        public string GetCallbackResult()
        {
            //throw new NotImplementedException();
            return "xxx";
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            //throw new NotImplementedException();
        }
    }
}
