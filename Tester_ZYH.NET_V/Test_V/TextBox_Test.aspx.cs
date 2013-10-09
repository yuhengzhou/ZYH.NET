using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class TextBox_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_KeyUp(object sender, ZYH.WebControl_V.TextBoxEventArg e)
        {
            System.Threading.Thread.Sleep(1000);
        }
    }
}