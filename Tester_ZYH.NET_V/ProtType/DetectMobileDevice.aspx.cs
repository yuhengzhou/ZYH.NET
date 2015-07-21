using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.ProtType
{
    public partial class DetectMobileDevice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser.IsMobileDevice)
            {
                var a = true;
                Response.Write("is mobile device.");
            }
            else { 
                Response.Write("not mobile device.");
            }
        }
    }
}