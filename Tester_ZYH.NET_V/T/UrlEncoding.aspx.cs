using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.T
{
    public partial class UrlEncoding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var s = Request.QueryString["a"];
            var x = Request.QueryString["b"];
            if (s == x) {
                var c = true;
            }
        }
    }
}