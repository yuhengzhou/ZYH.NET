using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ZYH.WebControl_V
{
    public abstract class CheckRadioList_Base : ListControl_Base
    {
        public CheckRadioList_Base(ListControlTypes listControlType) : base(listControlType) { }

        #region Property
        [DefaultValue(-1)]
        public int CellPadding
        {
            get { return _Settings.CellPadding; }
            set { _Settings.CellPadding = value; }
        }

        [DefaultValue(-1)]
        public int CellSpacing
        {
            get { return _Settings.CellSpacing; }
            set { _Settings.CellSpacing = value; }
        }

        [DefaultValue(0)]
        public int RepeatColumns
        {
            get { return _Settings.RepeatColumns; }
            set { _Settings.RepeatColumns = value; }
        }

        [DefaultValue(typeof(System.Web.UI.WebControls.RepeatDirection), "Vertical")]
        public RepeatDirection RepeatDirection
        {
            get { return _Settings.RepeatDirection; }
            set { _Settings.RepeatDirection = value; }
        }

        [DefaultValue(typeof(System.Web.UI.WebControls.TextAlign), "Right")]
        public System.Web.UI.WebControls.TextAlign TextAlign
        {
            get { return _Settings.TextAlign; }
            set { _Settings.TextAlign = value; }
        }

        //public new string MaintainItemsState { get { return "not applicable"; } }
        #endregion

        public override void RenderBeginTag(HtmlTextWriter writer) { }
        public override void RenderEndTag(HtmlTextWriter writer) { }
    }
}
