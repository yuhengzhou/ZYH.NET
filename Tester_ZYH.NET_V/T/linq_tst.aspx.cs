using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tester_ZYH.NET_V.T
{
    public partial class linq_tst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var dc = new List<decimal>() { };
            var c = dc.AsQueryable().First();

            var ValidGradings = new List<decimal>() { 1, 5, 3 };
            var q = ValidGradings.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key);
            if (q.Count() > 0)
            {
                var AgreeScore = q.FirstOrDefault();
            }


        }
    }
}