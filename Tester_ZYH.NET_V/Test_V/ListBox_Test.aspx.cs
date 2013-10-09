using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class ListBox_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListBox1_SelectedIndexChanged(object sender, ZYH.WebControl_V.ListControlEventArgs e)
        {

        }

        protected void ListBox1_Fill(object sender, ZYH.WebControl_V.ListControlEventArgs e)
        {
            ListBox1.Items.Add(new ZYH.WebControl_V.ListItem("AAA", "A"));
            ListBox1.Items.Add(new ZYH.WebControl_V.ListItem("BBB", "B"));
            ListBox1.Items.Add(new ZYH.WebControl_V.ListItem("CCC", "C"));
        }
    }
}