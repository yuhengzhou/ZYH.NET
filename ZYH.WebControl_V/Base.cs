using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;
using System.ComponentModel;

namespace ZYH.WebControl_V
{
    public abstract class Base : System.Web.UI.WebControls.WebControl, INamingContainer
    {

        public Base() : base() { }
        public Base(HtmlTextWriterTag tag) : base(tag) { }
        public Base(string tag) : base(tag) { }


        public string RenderControl(Control control)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            control.RenderControl(hw);
            return sb.ToString();
        }
    }
}
