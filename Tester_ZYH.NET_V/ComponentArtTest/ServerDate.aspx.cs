using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.ComponentArtTest
{
    public partial class ServerDate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CallBack1_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            switch (e.Parameter)
            {
                case "date":
                    lblServerDateTime.Text = "Current server date: <b>" + DateTime.Now.Date.ToString("MMMM dd, yyyy") + "</b>";
                    break;
                case "weekDay":
                    lblServerDateTime.Text = "Current server week day: <b>" + DateTime.Now.DayOfWeek.ToString() + "</b>";
                    break;
                case "time":
                    lblServerDateTime.Text = "Current server time: <b>" + DateTime.Now.ToLongTimeString() + "</b>";
                    break;
            }
           // lblServerDateTime.RenderControl(e.Output); 

        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}