using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Demo
{
    public partial class DynamicLoading_Demo : PageBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _CallbackManager = CallbackManager1;
            _PlaceHolder_Tab = PlaceHolder_Tab;

            _CallbackManager.Refrence.Add("MyPage", this);
            _CallbackManager.Refrence.Add("", this);
            // _CallbackManager.Refrence.Add("", "xxx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            { LoadTab(false, false); }
        }

        protected void LinkButton1_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            CurrentTab = CurrentTabs.Tab1;
            AddHistory();
            e.ArgToClient = LoadTab(true, true);
        }

        protected void LinkButton2_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            CurrentTab = CurrentTabs.Tab2;
            AddHistory();
            e.ArgToClient = LoadTab(true, true);
        }

        protected void LinkButton3_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            CurrentTab = CurrentTabs.Tab3;
            AddHistory();
            e.ArgToClient = LoadTab(true, true);
        }

        protected void CallbackManager1_Navigate(ZYH.WebControl_V.NavigateEventArgs e)
        {
            switch (e.HistoryPoint.Entry)
            {
                case "1":
                    CurrentTab = CurrentTabs.Tab1;
                    break;
                case "2":
                    CurrentTab = CurrentTabs.Tab2;
                    break;
                case "3":
                    CurrentTab = CurrentTabs.Tab3;
                    break;
            }
            e.ArgToClient = LoadTab(true, true);
        }
    }

}