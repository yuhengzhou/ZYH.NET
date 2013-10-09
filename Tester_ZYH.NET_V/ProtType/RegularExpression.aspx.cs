using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tester_ZYH.NET_V.ProtType
{
    public partial class RegularExpression : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string st = File.ReadAllText(Server.MapPath("~/ProtType/EmailLead_RDC.txt"));
            //string pattern = @"\b[Ff]irst\s+Name\:\s*(\w+\s*\w+\r)";
            string pattern = @"(?<=\b[Ff]irst\s+Name\:\s)(\w+\s?\w+)";
            Match m = Regex.Match(st, pattern);

        }
    }
}