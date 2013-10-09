using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.ProtType
{
    public partial class IsControlOnThePage : System.Web.UI.Page
    {
        private CheckBox CK = new CheckBox();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            var x1 = CK.Page;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PlaceHolder1.Controls.Add(CK);
            var x2 = CK.Page;
            PlaceHolder1.Controls.Clear();
        }

        protected override void OnPreRender(EventArgs e)
        {
            var x3 = CK.Page;
            base.OnPreRender(e);
        }
    }
}