using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Demo
{
    public partial class Events_Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton_EventsDemo_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            e.ArgToClient = e.ArgFromClient + ", current time is: " + DateTime.Now.ToString();
        }
    }
}