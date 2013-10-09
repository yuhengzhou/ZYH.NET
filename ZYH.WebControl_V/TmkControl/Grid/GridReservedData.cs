using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Web.UI.WebControls;
using ZYH.WebControl_V;
using System.Xml;

namespace ZYH.WebControl_V.TmkControl.Grid
{
    [Serializable, XmlRoot("Grid")]
    public class GridData : SerializableObjectBase
    {
        [XmlElement("SET")]
        public GridSetting Settings = new GridSetting();

        [XmlIgnore]
        public string Html = "";
        [XmlElement("Html"), DefaultValue("")]
        public XmlCDataSection Html_CData
        {
            get { return new XmlDocument().CreateCDataSection(Html); }
            set { Html = value.Value; }
        }
    }

    [Serializable]
    public class GridSetting : ControlSetting_Base
    {
        [DefaultValue(false), XmlElement("SE_PIC")]
        public bool ServerEvent_PageIndexChanged = false;
        [DefaultValue(false), XmlElement("SE_S")]
        public bool ServerEvent_Sorted = false;
        [DefaultValue(false), XmlElement("SE_RS")]
        public bool ServerEvent_RowSelected = false;

        [DefaultValue(10), XmlElement("PS")]
        public int PageSize = 10;
        [DefaultValue(0), XmlElement("PI")]
        public int PageIndex = 0;
        [DefaultValue(""), XmlElement("SE")]
        public string SortExpression = "";
        [DefaultValue(typeof(SortDirection), "Ascending"), XmlElement("SD")]
        public SortDirection SortDirection = SortDirection.Ascending;
        [DefaultValue(-1), XmlElement("SRI")]
        public int SelectedRowIndex = -1;

        [DefaultValue(""), XmlElement("EC")]
        public string ExternalCommand = "";
        [DefaultValue(""), XmlElement("ECA")]
        public string ExternalCommandArg = "";
        [DefaultValue(""), XmlElement("CM_EC")]
        public string ClientMethod_ExternalCommand = "";
        [DefaultValue(""), XmlElement("CE_A_EC")]
        public string ClientEvent_AfterCallback_ExternalCommand = "";
        [DefaultValue(""), XmlElement("CE_B_RS")]
        public string ClientEvent_BeforeCallback_RowSelected = "";
        [DefaultValue(""), XmlElement("CE_A_RS")]
        public string ClientEvent_AfterCallback_RowSelected = "";

    }


}
