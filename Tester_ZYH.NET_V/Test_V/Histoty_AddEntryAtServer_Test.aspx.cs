using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class Histoty_AddEntryAtServer_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            { LoadTab(false); }
            else
            {
                //CallbackManager1.ViewStates["tab"] = "1";
                LoadTab(false);
            }
        }

        protected void LinkButton1_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            CallbackManager1.AddHistoryPoint("1", "Step: 1");
            CallbackManager1.ViewStates["tab"] = "1";
            e.ArgToClient = LoadTab(true);
        }

        protected void LinkButton2_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            CallbackManager1.AddHistoryPoint("2", "Step: 2");
            CallbackManager1.ViewStates["tab"] = "2";
            e.ArgToClient = LoadTab(true);
        }

        protected void LinkButton3_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            CallbackManager1.AddHistoryPoint("3", "Step: 3");
            CallbackManager1.ViewStates["tab"] = "3";
            e.ArgToClient = LoadTab(true);
        }

        private string LoadTab(bool RenderMe)
        {
            System.Threading.Thread.Sleep(300);
            string rt = "";
            PlaceHolder.Controls.Clear();
            Control c = null; ;
            switch (CallbackManager1.ViewStates["tab"])
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
                PlaceHolder.Controls.Add(c);
                if (RenderMe) rt = CallbackManager1.RenderControl(c);
            }
            return rt;
        }

        protected void CallbackManager1_Navigate(ZYH.WebControl_V.NavigateEventArgs e)
        {
            switch (e.HistoryPoint.Entry)
            {
                case "":
                    CallbackManager1.ViewStates["tab"] = "";
                    break;
                case "1":
                    CallbackManager1.ViewStates["tab"] = "1";
                    break;
                case "2":
                    CallbackManager1.ViewStates["tab"] = "2";
                    break;
                case "3":
                    CallbackManager1.ViewStates["tab"] = "3";
                    break;
            }
            e.ArgToClient = LoadTab(true);
            e.HistoryPoint.Title = "Step: " + e.HistoryPoint.Entry;
        }
    }
}