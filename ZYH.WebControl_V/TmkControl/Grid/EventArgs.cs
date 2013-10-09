using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZYH.WebControl_V;
using System.Web.UI.WebControls;

namespace ZYH.WebControl_V.TmkControl.Grid
{
    public class GridRowBindEventArgs : CallbackEventArgs
    {
        public Row Row;
        public object RowDataItem;
    }

    public class GridExternalCommandEventArgs : CallbackEventArgs
    {
        public string ExternalCommand = "";
        public string ExternalCommandArg = "";
    }
}
