using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class Panel_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Panel3_LoadFromServer(object sender, ZYH.WebControl_V.PanelEventArgs e)
        {
            System.Threading.Thread.Sleep(300);
            Panel3.Controls.Clear();
            Control c = null; ;
            switch (e.ArgFromClient)
            {
                case "1":
                    c = LoadControl("~/Test_V/ASCX/HistoryTab1.ascx");
                    c.ID = "step1";
                    break;
                case "2":
                    c = LoadControl("~/Test_V/ASCX/HistoryTab2.ascx");
                    c.ID = "step2";
                    break;
                case "3":
                    c = LoadControl("~/Test_V/ASCX/HistoryTab3.ascx");
                    c.ID = "step3";
                    break;
            }


            if (c != null)
            {
                Panel3.Controls.Add(c);
                if (e.RenderMe) e.HtmlToBeLoaded = CallbackManager1.RenderControl(c);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

    }
}