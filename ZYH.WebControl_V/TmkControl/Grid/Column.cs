using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace ZYH.WebControl_V.TmkControl.Grid
{
    [Bindable(true)]
    public class Column
    {
        private string _HeaderText = "";
        [DefaultValue("")]
        public string HeaderText
        {
            get { return _HeaderText; }
            set { _HeaderText = value; }
        }

        private string _SortExpression = "";
        [DefaultValue("")]
        public string SortExpression
        {
            get { return _SortExpression; }
            set { _SortExpression = value; }
        }

        private string _CssClass_HeaderRow = "";
        [DefaultValue("")]
        public string CssClass_HeaderRow
        {
            get { return _CssClass_HeaderRow; }
            set { _CssClass_HeaderRow = value; }
        }

        private string _CssClass_DataRow = "";
        [DefaultValue("")]
        public string CssClass_DataRow
        {
            get { return _CssClass_DataRow; }
            set { _CssClass_DataRow = value; }
        }

        private string _CssClass_FooterRow = "";
        [DefaultValue("")]
        public string CssClass_FooterRow
        {
            get { return _CssClass_FooterRow; }
            set { _CssClass_FooterRow = value; }
        }

        private System.Web.UI.WebControls.Unit _Width;
        [DefaultValue("")]
        public System.Web.UI.WebControls.Unit Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private ITemplate _ItemTemplate;
        [DefaultValue((string)null), TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay), Browsable(false), Description("TemplateField_ItemTemplate"), PersistenceMode(PersistenceMode.InnerProperty)]
        public virtual ITemplate ItemTemplate
        {
            [System.Runtime.TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this._ItemTemplate;
            }
            set
            {
                this._ItemTemplate = value;
            }
        }




    }
}
