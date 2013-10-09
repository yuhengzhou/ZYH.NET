using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class Window_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Window1_LoadFromServer(object sender, ZYH.WebControl_V.WindowEventArgs e)
        {
            Control c = Page.LoadControl("~/Test_V/ASCX/Window1.ascx");
            c.ID = "ascx1";
            Window1.Controls.Add(c);
            e.Title = "This is Title of the window.";
            if (e.RenderMe) e.HtmlToBeLoaded = CallbackManager1.RenderControl(c);
        }
    }
}