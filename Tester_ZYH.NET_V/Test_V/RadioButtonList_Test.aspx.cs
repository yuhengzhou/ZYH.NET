using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class RadioButtonList_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, ZYH.WebControl_V.ListControlEventArgs e)
        {

        }

        protected void RadioButtonList1_Fill(object sender, ZYH.WebControl_V.ListControlEventArgs e)
        {
            RadioButtonList1.Items.Add(new ZYH.WebControl_V.ListItem("AAAAA", "A"));
            RadioButtonList1.Items.Add(new ZYH.WebControl_V.ListItem("BBBBB", "B"));
            RadioButtonList1.Items.Add(new ZYH.WebControl_V.ListItem("CCCCC", "C"));
        }
    }
}