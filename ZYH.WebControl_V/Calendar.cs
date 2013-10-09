using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZYH.WebControl_V
{
    [DefaultEvent("DateChanged"), System.Drawing.ToolboxBitmap(typeof(Calendar), "Image.ToolBox.Calendar.bmp")]
    public class Calendar : CallbackControl_Base
    {
        protected System.Web.UI.WebControls.HiddenField _HiddenField = new System.Web.UI.WebControls.HiddenField();
        protected System.Web.UI.WebControls.TextBox _TextBox = new System.Web.UI.WebControls.TextBox();
        private CalendarSetting _Settings = new CalendarSetting();

        #region Event
        public delegate void DateChangedHandler(object sender, CalendarEventArgs e);
        public event DateChangedHandler DateChanged;

        protected void OnDateChanged(CalendarEventArgs e)
        {
            if (DateChanged != null) DateChanged(this, e);
        }

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            var OriginalValue = this.Value;
            CalendarEventArgs e = new CalendarEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = this.CommandArg;
            e.CallbackOptions = this._CallbackOptions;
            e.SelectedDate = OriginalValue;
            e.IsValueValid = this.IsValueValid;
            switch (e.Command)
            {
                case CallbackCommands.Calendar_DateChanged:
                    OnDateChanged(e);
                    break;
            }
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            if (e.SelectedDate != OriginalValue) this.Value = e.SelectedDate;
            var ValueToClient = "";
            if (this.Value != DateTime.MinValue) ValueToClient = String.Format("{0:MM/dd/yyyy}", this.Value);
            CallbackResult.ControlReserved = ValueToClient;
        }
        #endregion

        #region Property
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return _TextBox.ReadOnly; }
            set { _TextBox.ReadOnly = value; }
        }

        [Category("Appearance"), DefaultValue(typeof(JQueryThemes), "Darkness")]
        public JQueryThemes Theme
        {
            get { return _Settings.Theme; }
            set { _Settings.Theme = value; }
        }

        //Format a date into a string value with a specified format.

        //The format can be combinations of the following: d - day of month (no leading zero)
        //dd - day of month (two digit) 
        //o - day of the year (no leading zeros) 
        //oo - day of the year (three digit) 
        //D - day name short 
        //DD - day name long 
        //m - month of year (no leading zero) 
        //mm - month of year (two digit) 
        //M - month name short 
        //MM - month name long 
        //y - year (two digit) 
        //yy - year (four digit) 
        //@ - Unix timestamp (ms since 01/01/1970) 
        //! - Windows ticks (100ns since 01/01/0001) 
        //'...' - literal text 
        //'' - single quote 
        //anything else - literal text 

        //There are also a number of predefined standard date formats available from $.datepicker:
        //ATOM - 'yy-mm-dd' (Same as RFC 3339/ISO 8601) 
        //COOKIE - 'D, dd M yy' 
        //ISO_8601 - 'yy-mm-dd' 
        //RFC_822 - 'D, d M y' (See RFC 822) 
        //RFC_850 - 'DD, dd-M-y' (See RFC 850) 
        //RFC_1036 - 'D, d M y' (See RFC 1036) 
        //RFC_1123 - 'D, d M yy' (See RFC 1123) 
        //RFC_2822 - 'D, d M yy' (See RFC 2822) 
        //RSS - 'D, d M y' (Same as RFC 822) 
        //TICKS - '!' 
        //TIMESTAMP - '@' 
        //W3C - 'yy-mm-dd' (Same as ISO 8601)

        [Category("Appearance"), DefaultValue("mm/dd/yy"), Description("http://docs.jquery.com/UI/Datepicker/formatDate")]
        public string DateFormat
        {
            get { return _Settings.DateFormat; }
            set { _Settings.DateFormat = value; }
        }

        private DateTime _Value;
        public DateTime Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public String Text
        {
            get { return _HiddenField.Value; }
        }

        private bool _IsValueValid = true;
        public bool IsValueValid { get { return _IsValueValid; } }

        [Category("Appearance"), Description("Get or set back color of text box when invalid value entered."), DefaultValue(typeof(System.Drawing.Color), "#FF99CC"), RefreshProperties(RefreshProperties.All), TypeConverter(typeof(System.Web.UI.WebControls.WebColorConverter))]
        public System.Drawing.Color InvalidValueBackColor
        {
            get { return System.Drawing.ColorTranslator.FromHtml(_Settings.InvalidValueBackColor); }
            set { _Settings.InvalidValueBackColor = System.Drawing.ColorTranslator.ToHtml(value); }
        }


        [Browsable(true), Description("Set a maximum selectable date via a Date object or as a string in the current dateFormat, or a number of days from today (e.g. +7) or a string of values and periods ('y' for years, 'm' for months, 'w' for weeks, 'd' for days, e.g. '+1m +1w'), or null for no limit.")]
        public string MaxDate
        {
            get { return _Settings.MaxDate; }
            set { _Settings.MaxDate = value; }
        }

        [Browsable(true), Description("Set a minimum selectable date via a Date object or as a string in the current dateFormat, or a number of days from today (e.g. +7) or a string of values and periods ('y' for years, 'm' for months, 'w' for weeks, 'd' for days, e.g. '-1y -1m'), or null for no limit.")]
        public string MinDate
        {
            get { return _Settings.MinDate; }
            set { _Settings.MinDate = value; }
        }

        [Category("Appearance"), Description("Get or set alternating back color of text box."), DefaultValue(typeof(System.Drawing.Color), "MintCream"), RefreshProperties(RefreshProperties.All), TypeConverter(typeof(System.Web.UI.WebControls.WebColorConverter))]
        public System.Drawing.Color OnFocusBackColor
        {
            get { return System.Drawing.ColorTranslator.FromHtml(_Settings.OnFocusBackColor); }
            set { _Settings.OnFocusBackColor = System.Drawing.ColorTranslator.ToHtml(value); }
        }

        private System.Drawing.Color _ReadOnlyBackColor = System.Drawing.Color.PapayaWhip;
        [Category("Appearance"), Description("Get or set alternating back color of text box."), DefaultValue(typeof(System.Drawing.Color), "PapayaWhip"), RefreshProperties(RefreshProperties.All), TypeConverter(typeof(System.Web.UI.WebControls.WebColorConverter))]
        public System.Drawing.Color ReadOnlyBackColor
        {
            get { return _ReadOnlyBackColor; }
            set { _ReadOnlyBackColor = value; }
        }

        [Category("Behavior"), Description("not applicable."), DefaultValue("not applicable")]
        public new string ClientEvent_BeforeCallback { get { return "not applicable"; } }

        [Category("Behavior"), Description("not applicable."), DefaultValue("not applicable")]
        public new string ClientEvent_AfterCallback { get { return "not applicable"; } }

        [Category("Behavior"), DefaultValue(""), Description("function BeforeCall(SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall. = false; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.ControlReserved = ControlReserved; e_BeforeCall.Text = text; ")]
        public string ClientEvent_BeforeCallback_DateChanged
        {
            get { return _Settings.ClientEvent_BeforeCallback_DateChanged; }
            set { _Settings.ClientEvent_BeforeCallback_DateChanged = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function AfterCall(SenderID, e_AfterCall) {} var e_AfterCall = new Object(); e_AfterCall.CancelCall. = false; e_AfterCall.ArgFromClient = ''; e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.Text = text; ")]
        public string ClientEvent_AfterCallback_DateChanged
        {
            get { return _Settings.ClientEvent_AfterCallback_DateChanged; }
            set { _Settings.ClientEvent_AfterCallback_DateChanged = value; }
        }
        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            _HiddenField.ID = "hd"; this.Controls.Add(_HiddenField);

            if (Page.IsPostBack)
            {
                string value = "";
                if (Page.IsCallback)
                {
                    if (CallbackManager.CallbackID == null) CallbackManager.InitCallbackParams();
                    if (CallbackManager.CallbackID == UniqueID)
                    {
                        value = CallbackManager.CallbackParameters.ControlReserved;
                    }
                    else if (CallbackManager.CallbackParameters.CallbackOptions.PostDataContainer != "NA")
                    {
                        var st = Page.Request.Params[_HiddenField.UniqueID];
                        if (st != null) { value = st; }
                    }
                }
                else
                {
                    var st = Page.Request.Params[_HiddenField.UniqueID];
                    if (st != null) { value = st; }
                }

                if (value == "")
                {
                    _Value = DateTime.MinValue;
                }
                else
                {
                    try { _Value = Convert.ToDateTime(value, new System.Globalization.CultureInfo("en-US")); }
                    catch { _Value = DateTime.MinValue; _IsValueValid = false; }
                }
            }
        }

        protected override void OnBeforeRender()
        {
            _Settings.CallbackManagerClientInstance = CallbackManager.ClientInstanceName;
            _Settings.UniqueID = this.UniqueID;
            _Settings.EventTriggerHierarchyID = this.EventTriggerHierarchyID;
            _Settings.CommandArg = this.CommandArg;

            _Settings.BackColor = System.Drawing.ColorTranslator.ToHtml(this.BackColor);
            _Settings.ServerEvents_DateChanged = (DateChanged == null) ? false : true;

            if (Enabled)
            {
                if (CallbackManager.PreRegisteredScriptResources["JQuery.Ui.min", "1.9.2"].LoadFrom == ScriptResourceLoadFrom.Disabled) { CallbackManager.Scripts.RegisterClientScriptInclude("JQuery.Ui.min", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.jquery-ui-1.9.2.custom.min.js")); }
                if (!CallbackManager.Scripts.IsScriptRegistered("CalendarJs")) { CallbackManager.Scripts.RegisterClientScriptInclude("CalendarJs", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.Calendar.js")); }
                if (!CallbackManager.Scripts.IsScriptRegistered("CalendarCss")) { CallbackManager.Scripts.RegisterCss("CalendarCss", new JQueryThemesUrl(this.Theme).Url); }
                string st = "";
                st += "var " + ClientID + "Instance = new Class_Calendar('" + this.ClientID + "'," + base._CallbackOptions.ToJson_Js() + "," + _Settings.ToJson_Js() + ");\r\n";
                st += "" + ClientID + "Instance.Init();\r\n";
                CallbackManager.Scripts.RegisterStartupScript(ClientID + "InitJs", st);
            }
            base.OnBeforeRender();
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            _TextBox.Attributes.Add("name", UniqueID); _TextBox.Attributes.Add("id", ClientID);
            _TextBox.ControlStyle.CopyFrom(this.ControlStyle);
            if (ReadOnly)
            {
                _TextBox.Attributes.Add("readonly", "readonly");
                _TextBox.Style.Add("background-color", System.Drawing.ColorTranslator.ToHtml(ReadOnlyBackColor));
            }
            writer.Write(RenderControl(_TextBox));
            if (IsValueValid)
            {
                if (this.Value == DateTime.MinValue) { _HiddenField.Value = ""; } else { _HiddenField.Value = String.Format("{0:MM/dd/yyyy}", this.Value); }
            }
            writer.Write(RenderControl(_HiddenField));
        }

        public override void RenderBeginTag(System.Web.UI.HtmlTextWriter writer) { }
        public override void RenderEndTag(System.Web.UI.HtmlTextWriter writer) { }
    }
}
