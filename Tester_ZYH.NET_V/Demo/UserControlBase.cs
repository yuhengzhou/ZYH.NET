using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tester_ZYH.NET_V.Demo
{
    public class UserControlBase : System.Web.UI.UserControl
    {
        public PageBase MyPage { get; set; }
        public bool IsInitLoad { get; set; }
        public Guid PersonId { get; set; }
        protected override void OnInit(EventArgs e)
        {
            MyPage = (PageBase)Page;
            base.OnInit(e);
        }
    }
}