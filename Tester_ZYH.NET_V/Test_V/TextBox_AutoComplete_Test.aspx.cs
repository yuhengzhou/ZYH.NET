using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.Test_V
{
    public partial class TextBox_AutoComplete_Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox1_KeyUp(object sender, ZYH.WebControl_V.TextBoxEventArg e)
        {
            if (e.Text == "") return;
            string st = "";
            //TMAccountModel.TMAccountEntities t = new TMAccountModel.TMAccountEntities();
            //var q = t.subscribers.Where(x => x.first_name.Contains(e.Text));
            //foreach (TMAccountModel.subscriber s in q)
            //{
            //    if (st != "") st += Convert.ToChar(255);
            //    st += s.first_name + " " + s.last_name;
            //}
            for (var i = 0; i < 50; i++)
            {
                st += "XXX"+i + Convert.ToChar(255);
            }
            e.ArgToClient = st;
            System.Threading.Thread.Sleep(1000);
        }
    }
}