using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ZYH.WebControl_V.TmkControl
{
    [DefaultEvent("ThemeSelected"), ToolboxBitmap(typeof(ThemeSelector), "Image.ToolBox.ThemeSelector.gif")]
    public class ThemeSelector : CallbackControl_Base
    {
        protected HiddenField _HiddenField;
        ThemeSelectorSetting _Settings = new ThemeSelectorSetting();
        ThemeSelectorData _Data = new ThemeSelectorData();

        public ThemeSelector() : base(System.Web.UI.HtmlTextWriterTag.Div) { }

        #region Event
        public delegate void ThemeSelectedHandler(object sender, ThemeSelectorEventArgs e);
        public event ThemeSelectedHandler ThemeSelected;

        protected virtual void OnThemeSelected(ThemeSelectorEventArgs e)
        {
            if (ThemeSelected != null) ThemeSelected(this, e);
        }

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            ThemeSelectorEventArgs e = new ThemeSelectorEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = this.CommandArg;
            e.CallbackOptions = this._CallbackOptions;
            var SelectedTheme = this.Themes.Where(x => x.Selected).FirstOrDefault();
            if (SelectedTheme != null) e.SelectedThemeValue = SelectedTheme.Value;
            switch (e.Command)
            {
                case CallbackCommands.ThemeSelector_ThemeSelected:
                    OnThemeSelected(e);
                    break;
            }
            if (SelectedTheme != null && SelectedTheme.Value != e.SelectedThemeValue)
            {
                this.Themes.ForEach(x => x.Selected = false);
                this.Themes.Where(x => x.Value == e.SelectedThemeValue).First().Selected = true;
            }
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            CallbackResult.ControlReserved = _Data.ToXML(this.ClientID);
        }
        #endregion

        #region Property
        [Browsable(true), Category("Behavior"), PersistenceMode(PersistenceMode.InnerProperty), RefreshProperties(RefreshProperties.All)]
        public ThemeCollection Themes
        {
            get { return _Data.Themes; }
        }

        [Browsable(false)]
        public Theme SelectedTheme
        {
            get
            {
                return Themes.SelectedTheme;
            }
        }

        [Category("Behavior"), DefaultValue(""), Description("function BeforeCall(SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall. = false; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.ControlReserved = ControlReserved; e_BeforeCall.Control = control")]
        public string ClientEvent_BeforeCallback_SelectedIndexChanged
        {
            get { return _Settings.ClientEvent_BeforeCallback_ThemeSelected; }
            set { _Settings.ClientEvent_BeforeCallback_ThemeSelected = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function AfterCall(SenderID, e_AfterCall) {} //var e_AfterCall = new Object(); e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.ArgToClient = ArgToClient;")]
        public string ClientEvent_AfterCallback_SelectedIndexChanged
        {
            get { return _Settings.ClientEvent_AfterCallback_ThemeSelected; }
            set { _Settings.ClientEvent_AfterCallback_ThemeSelected = value; }
        }

        [Category("Behavior"), DefaultValue("not applicable"), Description("not applicable.")]
        public new string ClientEvent_BeforeCallback { get { return "not applicable"; } }

        [Category("Behavior"), DefaultValue("not applicable"), Description("not applicable.")]
        public new string ClientEvent_AfterCallback { get { return "not applicable"; } }
        #endregion

        private void InitPostData()
        {
            string st = Page.Request.Params[_HiddenField.UniqueID];
            if (!string.IsNullOrEmpty(st))
            {
                var xml = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(st));
                _Data = SerializableObjectBase.CreateFromXML<ThemeSelectorData>(xml);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            _HiddenField = new HiddenField(); _HiddenField.ID = "hd"; this.Controls.Add(_HiddenField);
            base.OnInit(e);
            if (Page.IsPostBack)
            {
                string xml;
                if (Page.IsCallback)
                {
                    if (CallbackManager.CallbackID == null) CallbackManager.InitCallbackParams();
                    if (CallbackManager.CallbackID == UniqueID)
                    {
                        xml = CallbackManager.CallbackParameters.ControlReserved;
                        _Data = SerializableObjectBase.CreateFromXML<ThemeSelectorData>(xml);
                    }
                    else if (CallbackManager.CallbackParameters.CallbackOptions.PostDataContainer != "NA")
                    {
                        InitPostData();
                    }
                }
                else
                {
                    InitPostData();
                }

            }
        }

        protected override void OnBeforeRender()
        {
            if (!DesignMode)
            {
                _Settings.CallbackManagerClientInstance = CallbackManager.ClientInstanceName;
                _Settings.UniqueID = this.UniqueID;
                _Settings.EventTriggerHierarchyID = this.EventTriggerHierarchyID;
                _Settings.CommandArg = this.CommandArg;

                _Settings.ServerEvents_ThemeSelected = (ThemeSelected == null) ? false : true;

                _HiddenField.Value = Convert.ToBase64String(new System.Text.UTF8Encoding().GetBytes(_Data.ToXML(this.ClientID)));

                string st = "";
                if (!CallbackManager.Scripts.IsScriptRegistered("ThemeSelectorJs")) { CallbackManager.Scripts.RegisterClientScriptInclude("ThemeSelectorJs", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.TmkControl.Js.ThemeSelector.js")); }
                st += "var " + ClientID + "_Instance = new Class_ThemeSelector('" + ClientID + "'," + base._CallbackOptions.ToJson_Js() + "," + _Settings.ToJson_Js() + ");";
                st += ClientID + "_Instance.Init();";
                CallbackManager.Scripts.RegisterStartupScript(ClientID + "InitJs", st);
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (this.Themes.Count == 0)
            {
                if (DesignMode) writer.Write("[" + this.ID + "]");
            }
            else
            {
                foreach (Theme t in this.Themes)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Id, t.ClientId);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, t.Selected ? t.CssClass_Selected : t.CssClass);
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:" + ClientID + "_Instance.ThemeSelected('" + t.Value + "');");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(t.Text);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                    writer.WriteLine();
                }
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);
            if (!DesignMode)
            {
                writer.WriteLine();
                writer.Write(RenderControl(_HiddenField));
            }
        }
    }
}
