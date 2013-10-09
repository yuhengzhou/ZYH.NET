using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZYH.WebControl_V.TmkControl.Grid
{
    public class ColumnCollection : List<Column>
    {
        public Column this[string headerText] { get { return this.Where(x => x.HeaderText == headerText).FirstOrDefault(); } }
    }
}
