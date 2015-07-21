using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.T
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           // var to = "joe@joemosher.com;joe.mosher@fivestreet.me";
            var to = "joe@joemosher.com;joe.mosher@fivestreet.me";
            char[] separator = new char[] { ';', ',' };
            string[] toList = to.Split(separator);

        }
    }
}