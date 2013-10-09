using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.ProtType
{
    public partial class MsRegisterJs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToLongTimeString();
            ScriptManager.RegisterStartupScript(UpdatePanel1, LinkButton1.GetType(), "StartUp1", "DirectAlert('direct alert!');", true);
            ScriptManager.RegisterStartupScript(UpdatePanel1, LinkButton1.GetType(), "StartUp", "ALERT();", true);
            ScriptManager.RegisterClientScriptInclude(UpdatePanel1, LinkButton1.GetType(), "Include", "../Test_V/RegisterJsTest.js");
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, LinkButton1.GetType(), "Block", "function DirectAlert(m){alert(m);}", true);

        }
    }
}