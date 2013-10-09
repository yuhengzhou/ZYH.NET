using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZYH.WebControl_V.DesignMode
{
    internal class CallbackManager : System.Web.UI.Design.ControlDesigner
    {
        private ZYH.WebControl_V.CallbackManager _Instance;

        public override void Initialize(System.ComponentModel.IComponent component)
        {
            _Instance = (ZYH.WebControl_V.CallbackManager)component;
            base.Initialize(component);
        }

        public override string GetDesignTimeHtml()
        {
            var st = _Instance.ShowControlInf_Design();
            return st;
        }

    }
}
