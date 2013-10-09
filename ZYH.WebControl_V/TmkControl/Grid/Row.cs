using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ZYH.WebControl_V.TmkControl.Grid
{
    public class Row : TableRow, IDataItemContainer
    {
        public Row(int RowIndex, object DataItem) { _RowIndex = RowIndex; _DataItemIndex = RowIndex; _DisplayIndex = RowIndex; _DataItem = DataItem; }

        private int _RowIndex;
        public int RowIndex
        {
            get { return _RowIndex; }
        }

        #region IDataItemContainer Members

        private object _DataItem;
        public object DataItem
        {
            get { return _DataItem; }
        }

        private int _DataItemIndex;
        public int DataItemIndex
        {
            get { return _DataItemIndex; }
        }

        private int _DisplayIndex;
        public int DisplayIndex
        {
            get { return _DisplayIndex; }
        }

        #endregion
    }
}
