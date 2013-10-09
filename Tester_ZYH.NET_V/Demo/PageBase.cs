using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Tester_ZYH.NET_V.Demo
{
    public class PageBase : System.Web.UI.Page
    {
        public ZYH.WebControl_V.CallbackManager _CallbackManager { get; set; }
        public HtmlGenericControl _PlaceHolder_Tab { get; set; }

        public CurrentTabs CurrentTab
        {
            get { var s = _CallbackManager.ViewStates["CurrentTab"]; if (String.IsNullOrEmpty(s)) { return CurrentTabs.Tab1; } else { return (CurrentTabs)Convert.ToInt32(s); } }
            set { _CallbackManager.ViewStates["CurrentTab"] = Convert.ToInt32(value).ToString(); }
        }

        protected virtual string LoadTab(bool IsInitLoad, bool RenderMe)
        {
            string rt = "";
            UserControlBase c;
            switch (CurrentTab)
            {
                default:
                case CurrentTabs.Tab1:
                    c = (UserControlBase)LoadControl("~/Demo/ASCX/Tab1.ascx");
                    c.ID = "Tab1";
                    break;
                case CurrentTabs.Tab2:
                    c = (UserControlBase)LoadControl("~/Demo/ASCX/Tab2.ascx");
                    c.ID = "Tab2";
                    break;
                case CurrentTabs.Tab3:
                    c = (UserControlBase)LoadControl("~/Demo/ASCX/Tab3.ascx");
                    c.ID = "Tab3";
                    break;
            }
            c.IsInitLoad = IsInitLoad;
            _PlaceHolder_Tab.Controls.Clear();
            _PlaceHolder_Tab.Controls.Add(c);
            if (RenderMe) rt = _CallbackManager.RenderControl(c);
            return rt;
        }

        public void AddHistory()
        {
            switch (CurrentTab )
            {
                default:
                case  CurrentTabs.Tab1:
                    _CallbackManager.AddHistoryPoint("1", "Dynamic Loading Demo - Tab 1");
                    break;
                case CurrentTabs.Tab2:
                    _CallbackManager.AddHistoryPoint("2", "Dynamic Loading Demo - Tab 2");
                    break;
                case CurrentTabs.Tab3:
                    _CallbackManager.AddHistoryPoint("3", "Dynamic Loading Demo - Tab 3");
                    break;
            }
        }
    }

    public enum CurrentTabs
    {
        Tab1 = 1,
        Tab2 = 2,
        Tab3 = 3
    }
}