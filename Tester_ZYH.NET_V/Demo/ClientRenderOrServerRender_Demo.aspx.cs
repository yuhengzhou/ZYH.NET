using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Demo
{
    public partial class ClientRenderOrServerRender_Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, ZYH.WebControl_V.CallbackEventArgs e)
        {
            Table T = new Table();
            for (int i = 0; i < 10000; i++)
            {
                TableRow tr = new TableRow(); T.Controls.Add(tr);
                TableCell td = new TableCell(); tr.Cells.Add(td);
                td.Width = 100;
                td.Height = 25;
                td.Style.Add("border", "solid red 1px");
                td.Text = i.ToString();
            }

            e.ArgToClient = CallbackManager1.RenderControl(T);
        }
    }
}