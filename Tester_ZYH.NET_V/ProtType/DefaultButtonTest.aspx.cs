using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.ProtType
{
    public partial class DefaultButtonTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = "LinkButton1";
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
    }
}