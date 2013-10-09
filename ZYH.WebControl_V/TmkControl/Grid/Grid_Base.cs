using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;
using ZYH.WebControl_V;
using System.Collections;
using System.Drawing;

namespace ZYH.WebControl_V.TmkControl.Grid
{
    public abstract class Grid_Base : ZYH.WebControl_V.CallbackControl_Base
    {

        protected GridData _Data = new GridData();

        protected System.Web.UI.WebControls.HiddenField _HiddenField = new System.Web.UI.WebControls.HiddenField();
        internal GridInnerTable _Grid = new GridInnerTable(false);
        protected TableRow _HeadRow;
        protected TableRow _FooterRow;

        #region Event
        public delegate void PageIndexChangedHandler(object sender, CallbackEventArgs e);
        public event PageIndexChangedHandler PageIndexChanged;
        public delegate void SortedHandler(object sender, CallbackEventArgs e);
        public event SortedHandler Sorted;
        public delegate void RowDataBoundHandler(object sender, GridRowBindEventArgs e);
        public event RowDataBoundHandler RowDataBound;
        public delegate void ExternalCommandHandler(object sender, GridExternalCommandEventArgs e);
        public event ExternalCommandHandler ExternalCommand;
        public delegate void RowSelectedHandler(object sender, CallbackEventArgs e);
        public event RowSelectedHandler RowSelected;

        protected virtual void OnBuildHeader()
        {
            _HeadRow = new TableRow(); _Grid.Rows.Add(_HeadRow); _HeadRow.CssClass = this._CssClass_HeaderRow;
            int i = 0;
            foreach (Column c in Columns)
            {
                TableCell td = new TableCell(); _HeadRow.Cells.Add(td); td.Width = c.Width; td.CssClass = c.CssClass_HeaderRow;
                if (c.SortExpression == "")
                { Label lb = new Label(); td.Controls.Add(lb); lb.Text = c.HeaderText; }
                else
                {
                    HyperLink hk = new HyperLink(); hk.ID = "h" + i.ToString(); td.Controls.Add(hk); hk.Text = c.HeaderText;
                    hk.NavigateUrl = "javascript:" + ClientID + "Instance.SortClick('" + c.SortExpression + "');";
                    if (this.SortExpression == c.SortExpression)
                    {
                        if (this.SortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
                        { hk.CssClass = "icon sortDown"; }
                        else
                        { hk.CssClass = "icon sortUp"; }
                    }
                }
                i++;
            }
        }

        protected virtual void OnBuildDataRow(object rowDataItem, int rowIndex)
        {
            Row row = new Row(rowIndex, rowDataItem); Rows.Add(row); _Grid.Rows.Add(row);
            if (ClientEvent_BeforeCallback_RowSelected != "" || RowSelected != null) { row.Attributes.Add("onclick", "javascript:" + ClientID + "Instance.OnRowSelected(" + rowIndex + ");"); }
            if (rowIndex % 2 == 0) { row.CssClass = this.CssClass; } else { row.CssClass = this.CssClass_AlternateRow; }
            if (this.SelectedRowIndex == rowIndex) { row.CssClass = this.CssClass_SelectedRow; }

            foreach (Column c in Columns)
            {
                TableCell td = new TableCell(); row.Cells.Add(td);
                c.ItemTemplate.InstantiateIn(td);
            }

            GridRowBindEventArgs e = new GridRowBindEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = "";
            e.Command = CallbackCommands.Grid_RowDataBound;
            e.CommandArg = this.CommandArg;
            e.RowDataItem = rowDataItem;
            e.Row = row;
            if (RowDataBound == null) throw new Exception("must use event \"RowDataBound\" to bind data to controls. This is definded by \"ItemTemplate\"");
            RowDataBound(this, e);
            foreach (TableCell td in row.Cells)
            {
                foreach (Control cc in td.Controls)
                { if (cc.ID != null)cc.ID = cc.ID + "_" + rowIndex.ToString(); }
            }
        }

        protected virtual void OnBuildFooter(int DataSource_Count)
        {
            _FooterRow = new TableRow(); _Grid.Rows.Add(_FooterRow); _FooterRow.CssClass = this._CssClass_FooterRow;
            TableCell td = new TableCell(); _FooterRow.Cells.Add(td); td.ColumnSpan = Columns.Count;
            Table T = new Table(); td.Controls.Add(T); T.Width = new Unit("100%");
            TableRow tr = new TableRow(); T.Rows.Add(tr);
            td = new TableCell(); tr.Cells.Add(td);
            HyperLink hk = new HyperLink(); hk.ID = "Previous"; td.Controls.Add(hk); hk.Text = "Previous";
            if (PageIndex == 0) { hk.Enabled = false; } else { hk.Enabled = true; hk.NavigateUrl = "javascript:" + ClientID + "Instance.PreviousClick();"; }

            //int pageCount = Convert.ToInt32(Math.Round((Convert.ToDouble(DataSource.Count) / PageSize), MidpointRounding.AwayFromZero));
            int pageCount = (DataSource_Count / PageSize);
            if ((DataSource_Count % PageSize) != 0) pageCount++;

            int start; int end;
            if (PageIndex < 9) { start = 0; } else { start = (PageIndex / 10) * 10; }
            end = start + 10;
            if (end > pageCount)
            {
                end = pageCount; start = end - 10;
                if (start < 0) start = 0;
            }
            if (start > 0)
            {
                td = new TableCell(); tr.Cells.Add(td);
                hk = new HyperLink(); hk.ID = "hkf"; td.Controls.Add(hk); hk.Text = "1"; hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(0);";
                td = new TableCell(); tr.Cells.Add(td);
                hk = new HyperLink(); hk.ID = "hks"; td.Controls.Add(hk); hk.Text = "..."; hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(" + (start - 1).ToString() + ");";
            }
            for (int i = start; i < end; i++)
            {
                Label lb;
                if (i == PageIndex)
                {
                    td = new TableCell(); tr.Cells.Add(td);
                    lb = new Label(); lb.ID = "l" + i.ToString(); td.Controls.Add(lb); lb.Text = (i + 1).ToString();
                }
                else
                {
                    td = new TableCell(); tr.Cells.Add(td);
                    hk = new HyperLink(); hk.ID = "p" + i.ToString(); td.Controls.Add(hk); hk.Text = (i + 1).ToString();
                    hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(" + i.ToString() + ");";
                }
            }
            if (end < pageCount - 1)
            {
                td = new TableCell(); tr.Cells.Add(td);
                hk = new HyperLink(); hk.ID = "hke"; td.Controls.Add(hk); hk.Text = "..."; hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(" + (end).ToString() + ");";
                td = new TableCell(); tr.Cells.Add(td);
                hk = new HyperLink(); hk.ID = "hkl"; td.Controls.Add(hk); hk.Text = pageCount.ToString(); hk.NavigateUrl = "javascript:" + ClientID + "Instance.PageingClick(" + (pageCount - 1).ToString() + ");";
            }

            td = new TableCell(); tr.Cells.Add(td);
            hk = new HyperLink(); hk.ID = "Next"; td.Controls.Add(hk); hk.Text = "Next";
            if (PageIndex == pageCount - 1) { hk.Enabled = false; } else { hk.Enabled = true; hk.NavigateUrl = "javascript:" + ClientID + "Instance.NextClick();"; }
        }

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            CallbackEventArgs e = new CallbackEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = this.CommandArg;
            e.CallbackOptions = this._CallbackOptions;
            switch (e.Command)
            {
                case CallbackCommands.Grid_PageIndexChanged:
                    OnPageIndexChanged(e);
                    break;
                case CallbackCommands.Grid_Sorted:
                    SelectedRowIndex = -1;
                    OnSorted(e);
                    break;
                case CallbackCommands.Grid_ExternalCommand:
                    OnExternalCommand(CallbackParameters, e);
                    break;
                case CallbackCommands.Grid_RowSelected:
                    OnRowSelected(e);
                    break;
            }
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            _Grid.RenderItemsOnly = true;
            _Data.Html = this.RenderControl(_Grid);
            CallbackResult.ControlReserved = _Data.ToXML();
        }

        protected virtual void OnSorted(CallbackEventArgs e)
        {
            if (Sorted != null) Sorted(this, e);
        }

        protected virtual void OnPageIndexChanged(CallbackEventArgs e)
        {
            if (PageIndexChanged != null) PageIndexChanged(this, e);
        }

        protected virtual void OnExternalCommand(CallbackParameters CallbackParameters, CallbackEventArgs ee)
        {
            GridExternalCommandEventArgs e = new GridExternalCommandEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = this.CommandArg;
            e.CallbackOptions = this._CallbackOptions;
            e.ExternalCommand = _Data.Settings.ExternalCommand;
            e.ExternalCommandArg = _Data.Settings.ExternalCommandArg;
            if (ExternalCommand != null) ExternalCommand(this, e);
            ee.ArgToClient = e.ArgToClient;
            ee.CallbackOptions = e.CallbackOptions;
        }

        protected virtual void OnRowSelected(CallbackEventArgs e)
        {
            if (RowSelected != null) RowSelected(this, e);
        }
        #endregion

        #region Property
        private int _CellPadding = -1;
        [DefaultValue(-1)]
        public int CellPadding
        {
            get { return _CellPadding; }
            set { _CellPadding = value; }
        }

        private int _CellSpacing = 0;
        [DefaultValue(0)]
        public int CellSpacing
        {
            get { return _CellSpacing; }
            set { _CellSpacing = value; }
        }

        protected string _EmptyDataText = "No Data";
        [DefaultValue("No Data")]
        public string EmptyDataText
        {
            get { return _EmptyDataText; }
            set { _EmptyDataText = value; }
        }

        protected ColumnCollection _Columns = new ColumnCollection();
        [PersistenceMode(PersistenceMode.InnerProperty), Bindable(true)]
        public ColumnCollection Columns { get { return _Columns; } }

        [DefaultValue(0)]
        public int PageIndex
        {
            get { return _Data.Settings.PageIndex; }
            set { _Data.Settings.PageIndex = value; }
        }

        [DefaultValue(10)]
        public int PageSize
        {
            get { return _Data.Settings.PageSize; }
            set { _Data.Settings.PageSize = value; }
        }

        [Browsable(false)]
        public SortDirection SortDirection
        {
            get { return _Data.Settings.SortDirection; }
            set { _Data.Settings.SortDirection = value; }
        }

        //protected string _SortExpression = "";
        [Browsable(false)]
        public string SortExpression
        {
            get { return _Data.Settings.SortExpression; }
            set { _Data.Settings.SortExpression = value; }
        }

        private System.Data.DataView _DataSource;
        [Browsable(false)]
        public System.Data.DataView DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        private IQueryable _QueryableSource;
        [Browsable(false)]
        public IQueryable QueryableSource
        {
            get { return _QueryableSource; }
            set { _QueryableSource = value; }
        }

        private string _CssClass_FooterRow = "";
        [DefaultValue("")]
        public string CssClass_FooterRow
        {
            get { return _CssClass_FooterRow; }
            set { _CssClass_FooterRow = value; }
        }

        private string _CssClass_HeaderRow = "";
        [DefaultValue("")]
        public string CssClass_HeaderRow
        {
            get { return _CssClass_HeaderRow; }
            set { _CssClass_HeaderRow = value; }
        }

        private string _CssClass_AlternateRow = "";
        [DefaultValue("")]
        public string CssClass_AlternateRow
        {
            get { return _CssClass_AlternateRow; }
            set { _CssClass_AlternateRow = value; }
        }

        private string _CssClass_SelectedRow = "";
        [DefaultValue("")]
        public string CssClass_SelectedRow
        {
            get { return _CssClass_SelectedRow; }
            set { _CssClass_SelectedRow = value; }
        }

        private RowCollection _Rows = new RowCollection();
        [Browsable(false)]
        public RowCollection Rows { get { return _Rows; } }

        [Browsable(false)]
        public int SelectedRowIndex
        {
            get { return _Data.Settings.SelectedRowIndex; }
            set { _Data.Settings.SelectedRowIndex = value; }
        }

        //[DefaultValue(typeof(Color), "Black"), Description(""), Browsable(true), Category("Appearance"), TypeConverter(typeof(WebColorConverter))]
        //public Color CallWaitingCoverColor
        //{
        //    get { return System.Drawing.ColorTranslator.FromHtml(_CallbackManagerReserved.CallWaitingCoverColor); }
        //    set { _CallbackManagerReserved.CallWaitingCoverColor = System.Drawing.ColorTranslator.ToHtml(value); }
        //}

        [Category("Behavior"), Description("not applicable."), DefaultValue("not applicable")]
        public new string ClientEvent_BeforeCallback { get { return "not applicable"; } }

        [Category("Behavior"), Description("not applicable."), DefaultValue("not applicable")]
        public new string ClientEvent_AfterCallback { get { return "not applicable"; } }

        [Category("Behavior"), Description("function (ExternalCommand, ExternalCommandArg)")]
        public string ClientMethod_ExternalCommand
        {
            get { return _Data.Settings.ClientMethod_ExternalCommand; }
            set { _Data.Settings.ClientMethod_ExternalCommand = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function [AfterCallback_ExternalCommand](SenderID, e_AfterCall) {} //var e_AfterCall = new Object(); e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.ArgToClient = ArgToClient; e_AfterCall.AfterCallAction = AfterCallAction; e_AfterCall.ClientInstance = Self; e_AfterCall.Context = Context;")]
        public string ClientEvent_AfterCallback_ExternalCommand
        {
            get { return _Data.Settings.ClientEvent_AfterCallback_ExternalCommand; }
            set { _Data.Settings.ClientEvent_AfterCallback_ExternalCommand = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function [BeforeCall](SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall = false/true; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.CommandArg = CommandArg; e_BeforeCall.ControlReserved = [ControlReserved]; e_BeforeCall.EventTriggerHierarchyID = EventTriggerHierarchyID; e_BeforeCall.SortExpression = Self.Settings.SortExpression; e_BeforeCall.SortDirection = Self.Settings.SortDirection;")]
        public string ClientEvent_BeforeCallback_RowSelected
        {
            get { return _Data.Settings.ClientEvent_BeforeCallback_RowSelected; }
            set { _Data.Settings.ClientEvent_BeforeCallback_RowSelected = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function [AfterCallback_RowSelected](SenderID, e_AfterCall) {} //var e_AfterCall = new Object(); e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.ArgToClient = ArgToClient; e_AfterCall.AfterCallAction = AfterCallAction; e_AfterCall.ClientInstance = Self; e_AfterCall.Context = Context;")]
        public string ClientEvent_AfterCallback_RowSelected
        {
            get { return _Data.Settings.ClientEvent_AfterCallback_RowSelected; }
            set { _Data.Settings.ClientEvent_AfterCallback_RowSelected = value; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _Grid.ID = "Grid"; this.Controls.Add(_Grid);
            _HiddenField.ID = "hd"; this.Controls.Add(_HiddenField);

            if (Page.IsPostBack)
            {
                string xml;
                if (Page.IsCallback)
                {
                    if (CallbackManager.CallbackID == UniqueID)
                    {
                        xml = CallbackManager.CallbackParameters.ControlReserved;
                        _Data = SerializableObjectBase.CreateFromXML<GridData>(xml);
                    }
                }
                else
                {
                    string st = Page.Request.Params[_HiddenField.UniqueID];
                    if (!string.IsNullOrEmpty(st))
                    {
                        xml = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(st));
                        _Data = SerializableObjectBase.CreateFromXML<GridData>(xml);
                    }
                }
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
        L:
            _Grid.Rows.Clear();

            OnBuildHeader();

            int DataSource_Count = -1;
            if (QueryableSource != null)
            {
                int start = PageSize * PageIndex;
                int end = start + PageSize - 1;
                var i = 0;
                foreach (var d in QueryableSource)
                {
                    if (i >= start && i <= end) OnBuildDataRow(d, i);
                    i++;
                }

                int TPageIndex = PageIndex;
                int MaxPage = i / PageSize;
                if ((i % PageSize) > 0) MaxPage++;
                if (PageIndex >= MaxPage) PageIndex = MaxPage - 1;
                if (PageIndex < 0) PageIndex = 0;
                if (TPageIndex != PageIndex) { goto L; }

                DataSource_Count = i;
                if (i > 0) { OnBuildFooter(DataSource_Count); }
            }
            else if (DataSource != null)
            {
                DataSource_Count = DataSource.Count;
                if (this.SortExpression != "")
                {

                    string sort = this.SortExpression;
                    if (this.SortDirection == System.Web.UI.WebControls.SortDirection.Descending) sort += " DESC";
                    DataSource.Sort = sort;
                }
                int start = PageSize * PageIndex;
                int end = start + PageSize;
                if (end > DataSource_Count - 1) end = DataSource_Count;
                for (int i = start; i < end; i++)
                {
                    OnBuildDataRow(DataSource[i], i);
                }
                if (DataSource_Count > 0) OnBuildFooter(DataSource_Count);
            }
            else
            {
                TableRow row = new TableRow(); _Grid.Rows.Add(row);
                TableCell td = new TableCell(); row.Cells.Add(td);
                td.Text = EmptyDataText;
            }
            base.OnDataBinding(e);
        }

        protected override void OnBeforeRender()
        {
            _Data.Settings.CallbackManagerClientInstance = CallbackManager.ClientInstanceName;
            _Data.Settings.UniqueID = this.UniqueID;
            _Data.Settings.EventTriggerHierarchyID = this.EventTriggerHierarchyID;
            _Data.Settings.CommandArg = this.CommandArg;

            _Data.Settings.ServerEvent_PageIndexChanged = (PageIndexChanged == null) ? false : true;
            _Data.Settings.ServerEvent_Sorted = (Sorted == null) ? false : true;
            _Data.Settings.ServerEvent_RowSelected = (RowSelected == null) ? false : true;

            if (Enabled)
            {
                if (!CallbackManager.Scripts.IsScriptRegistered("GridJs")) { CallbackManager.Scripts.RegisterClientScriptInclude("GridJs", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.TmkControl.Js.Grid.js")); }
                string st = "";
                st += "var " + ClientID + "Instance = new Class_Grid('" + ClientID + "'," + base._CallbackOptions.ToJson_Js() + ");";
                st += "" + ClientID + "Instance.Init();";
                CallbackManager.Scripts.RegisterStartupScript(ClientID + "InitJs", st);

                if (ExternalCommand != null)
                {
                    if (ClientMethod_ExternalCommand == "") throw new Exception("Please set 'ClientMethod_ExternalCommand' for control '" + ID + "'.");
                    if (!CallbackManager.Scripts.IsScriptRegistered(ClientID + "ExCmd"))
                    {
                        st = "function " + ClientMethod_ExternalCommand + " (ExternalCommand, ExternalCommandArg){" + ClientID + "Instance.OnExternalCommand(ExternalCommand,ExternalCommandArg); }";
                        CallbackManager.Scripts.RegisterClientScriptBlock(ClientID + "ExCmd", st);
                    }
                }
            }
            base.OnBeforeRender();


        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            if (this.Width.IsEmpty) this.Width = 450;
            _Grid.ControlStyle.CopyFrom(this.ControlStyle);
            _Grid.CellPadding = this.CellPadding;
            _Grid.CellSpacing = this.CellSpacing;
            if (DesignMode)
            {
                if (Columns.Count == 0)
                { writer.Write("<tr><td>please go to HTML Source to edit \"Columns\" property.</td></tr>"); }
                else
                { this.DataBind(); }
            }
            if (!DesignMode)
            {
                _HiddenField.Value = Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes(_Data.ToXML()));
            }
            base.RenderContents(writer);
        }

    }

    [ToolboxItem(false)]
    internal class GridInnerTable : System.Web.UI.WebControls.Table
    {
        internal bool RenderItemsOnly;
        internal GridInnerTable(bool _RenderItemsOnly) { RenderItemsOnly = _RenderItemsOnly; }
        public override void RenderBeginTag(HtmlTextWriter writer) { if (!RenderItemsOnly) { base.RenderBeginTag(writer); } }
        public override void RenderEndTag(HtmlTextWriter writer) { if (!RenderItemsOnly) { base.RenderEndTag(writer); } }
    }
}
