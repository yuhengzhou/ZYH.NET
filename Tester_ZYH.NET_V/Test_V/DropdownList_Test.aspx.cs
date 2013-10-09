using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class DropdownList_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, ZYH.WebControl_V.ListControlEventArgs e)
        {

        }

        protected void DropDownList2_Fill(object sender, ZYH.WebControl_V.ListControlEventArgs e)
        {
            DropDownList2.Items.Add(new ZYH.WebControl_V.ListItem("AAA","A"));
            DropDownList2.Items.Add(new ZYH.WebControl_V.ListItem("BBB","B"));
            DropDownList2.Items.Add(new ZYH.WebControl_V.ListItem("CCC","C"));
        }

    }
}