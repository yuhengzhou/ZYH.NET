using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Demo
{
    public partial class AsyncCallOptions_Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Session["ProcessingIndex"] = 0;
            }
        }

        protected void LinkButton_QueuedCallsCannotBeAborted_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
        }

        protected void LinkButton_BlockLaterCalls_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
        }

        protected void LinkButton_AbortPrevios_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
        }

        protected void LinkButton_QueueCalls_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
        }

        protected void LinkButton_MultipleCalls_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            var start = DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond;
            int ProcessingIndex = -1;
            //int ProcessingIndex = (int)Session["ProcessingIndex"];
            //ProcessingIndex++;
            //Session["ProcessingIndex"] = ProcessingIndex;

            System.Threading.Thread.Sleep(3000);

            e.ArgToClient = "Call Index: " + e.ArgFromClient + "; ProcessingIndex: " + ProcessingIndex + "; Processing start: " + start + "; Processing end: " + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond;

        }
    }
}