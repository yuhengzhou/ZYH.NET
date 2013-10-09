using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZYH.WebControl_V.TmkControl.Grid
{
    public class RowCollection : List<Row>
    {
        public new Row this[int RowIndex] { get { return this.Where(x => x.RowIndex == RowIndex).FirstOrDefault(); } }
    }
}
