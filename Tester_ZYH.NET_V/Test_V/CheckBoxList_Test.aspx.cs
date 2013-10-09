using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class CheckBoxList_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, ZYH.WebControl_V.ListControlEventArgs e)
        {

        }

        protected void CheckBoxList1_Fill(object sender, ZYH.WebControl_V.ListControlEventArgs e)
        {
            CheckBoxList1.Items.Add(new ZYH.WebControl_V.ListItem("AAAAA", "A"));
            CheckBoxList1.Items.Add(new ZYH.WebControl_V.ListItem("BBBBB", "B"));
            CheckBoxList1.Items.Add(new ZYH.WebControl_V.ListItem("CCCCC", "C"));
        }
    }
}