using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Linq;

namespace ZYH.WebControl_V
{
    [DefaultEvent("SelectedIndexChanged")]
    public abstract class ListControl_Base : CallbackControl_Base
    {
        public ListControl_Base(ListControlTypes listControlType) { _Settings.ListControlType = listControlType; }

        protected HiddenField _HiddenField;
        protected ListControlData _Data = new ListControlData();
        protected ListControlSettings _Settings = new ListControlSettings();

        public ListControl_Base() : base(System.Web.UI.HtmlTextWriterTag.Span) { }

        #region Event
        public delegate void FillHandler(object sender, ListControlEventArgs e);
        public event FillHandler Fill;
        public delegate void SelectedIndexChangedHandler(object sender, ListControlEventArgs e);
        public event SelectedIndexChangedHandler SelectedIndexChanged;

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            ListControlEventArgs e = new ListControlEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.SelectedItems = this.SelectedItems;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CommandArg = CallbackParameters.CommandArg;
            e.CallbackOptions = this._CallbackOptions;
            switch (e.Command)
            {
                case CallbackCommands.ListBox_Fill:
                case CallbackCommands.DropDownList_Fill:
                case CallbackCommands.CheckBoxList_Fill:
                case CallbackCommands.RadioButtonList_Fill:
                    e.CallbackOptions.AfterCallAction = AfterCallActions.RefreshToSynchro;
                    OnFill(e);
                    break;
                case CallbackCommands.ListBox_SelectedIndexChanged:
                case CallbackCommands.DropDownList_SelectedIndexChanged:
                case CallbackCommands.CheckBoxList_SelectedIndexChanged:
                case CallbackCommands.RadioButtonList_SelectedIndexChanged:
                    OnSelectedIndexChanged(e);
                    break;
            }
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            _Data.Items.ResolveImgUrl(Page);
        }

        public virtual void OnFill(ListControlEventArgs e) { if (Enabled && Fill != null) Fill(this, e); }

        public virtual void OnSelectedIndexChanged(ListControlEventArgs e) { if (Enabled && SelectedIndexChanged != null)SelectedIndexChanged(this, e); }
        #endregion

        #region Property

        [Browsable(true), Category("Behavior"), PersistenceMode(PersistenceMode.InnerProperty), RefreshProperties(RefreshProperties.All)]
        public ListItemCollection Items
        {
            get { return _Data.Items; }
        }

        [Browsable(false)]
        public List<ListItem> SelectedItems
        {
            get
            {
                return Items.SelectedItems;
            }
        }

        private string _ClientMethod_Fill = "";
        [Category("Behavior"), DefaultValue(""), Description("Set to generate the client method to fire Reload. function [ClientMethod_Fill](ArgToServer){}")]
        public string ClientMethod_Fill
        {
            get { return _ClientMethod_Fill; }
            set { _ClientMethod_Fill = value; }
        }

        private string _ClientMethod_SelectAll = "";
        [Category("Behavior"), DefaultValue(""), Description("Set to generate the client method to fire Reload. function [ClientMethod_SelectAll](){}")]
        public string ClientMethod_SelectAll
        {
            get { return _ClientMethod_SelectAll; }
            set { _ClientMethod_SelectAll = value; }
        }

        private string _ClientMethod_UnselectAll = "";
        [Category("Behavior"), DefaultValue(""), Description("Set to generate the client method to fire Reload. function [ClientMethod_UnselectAll](){}")]
        public string ClientMethod_UnselectAll
        {
            get { return _ClientMethod_UnselectAll; }
            set { _ClientMethod_UnselectAll = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function BeforeCall(SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall. = false; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.ControlReserved = ControlReserved; e_BeforeCall['ListControlObj'] = dl")]
        public string ClientEvent_BeforeCallback_SelectedIndexChanged
        {
            get { return _Settings.ClientEvent_BeforeCallback_SelectedIndexChanged; }
            set { _Settings.ClientEvent_BeforeCallback_SelectedIndexChanged = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function AfterCall(SenderID, e_AfterCall) {} //var e_AfterCall = new Object(); e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.ArgToClient = ArgToClient;")]
        public string ClientEvent_AfterCallback_SelectedIndexChanged
        {
            get { return _Settings.ClientEvent_AfterCallback_SelectedIndexChanged; }
            set { _Settings.ClientEvent_AfterCallback_SelectedIndexChanged = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function BeforeCall(SenderID, e_BeforeCall) {} var e_BeforeCall = new Object(); e_BeforeCall.CancelCall. = false; e_BeforeCall.ArgFromClient = ''; e_BeforeCall.ControlReserved = ControlReserved; e_BeforeCall['ListControlObj'] = dl")]
        public string ClientEvent_BeforeCallback_Fill
        {
            get { return _Settings.ClientEvent_BeforeCallback_Fill; }
            set { _Settings.ClientEvent_BeforeCallback_Fill = value; }
        }

        [Category("Behavior"), DefaultValue(""), Description("function AfterCall(SenderID, e_AfterCall) {} //var e_AfterCall = new Object(); e_AfterCall.ControlReserved = ControlReserved; e_AfterCall.ArgToClient = ArgToClient;")]
        public string ClientEvent_AfterCallback_Fill
        {
            get { return _Settings.ClientEvent_AfterCallback_Fill; }
            set { _Settings.ClientEvent_AfterCallback_Fill = value; }
        }

        [Category("Behavior"), DefaultValue("not applicable"), Description("not applicable.")]
        public new string ClientEvent_BeforeCallback { get { return "not applicable"; } }

        [Category("Behavior"), DefaultValue("not applicable"), Description("not applicable.")]
        public new string ClientEvent_AfterCallback { get { return "not applicable"; } }
        #endregion

        private void InitPostData()
        {
            var st = Page.Request.Params[_HiddenField.UniqueID];
            if (!string.IsNullOrEmpty(st))
            {
                var xml = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(st));
                _Data = SerializableObjectBase.CreateFromXML<ListControlData>(xml);
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
                        this._CallbackOptions = CallbackManager.CallbackParameters.CallbackOptions;
                        _Data = SerializableObjectBase.CreateFromXML<ListControlData>(xml);
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
            _Settings.CallbackManagerClientInstance = CallbackManager.ClientInstanceName;
            _Settings.UniqueID = this.UniqueID;
            _Settings.EventTriggerHierarchyID = this.EventTriggerHierarchyID;
            _Settings.CommandArg = this.CommandArg;

            _Settings.ServerEvents_Fill = (Fill == null) ? false : true;
            _Settings.ServerEvents_SelectedIndexChanged = (SelectedIndexChanged == null) ? false : true;

            string st = "";
            switch (_Settings.ListControlType)
            {
                case ListControlTypes.DropDownList:
                case ListControlTypes.ListBox:
                    if (!CallbackManager.Scripts.IsScriptRegistered("ListControlJs")) { CallbackManager.Scripts.RegisterClientScriptInclude("ListControlJs", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.ListControl.js")); }
                    st += "var " + ClientID + "Instance = new Class_ListControl('" + ClientID + "'," + base._CallbackOptions.ToJson_Js() + "," + _Settings.ToJson_Js() + ");\r\n";
                    st += ClientID + "Instance.Init();\r\n";
                    CallbackManager.Scripts.RegisterStartupScript(ClientID + "InitJs", st);
                    break;
                case ListControlTypes.CheckBoxList:
                case ListControlTypes.RadioButtonList:
                    if (!CallbackManager.Scripts.IsScriptRegistered("CheckRadioListJs")) { CallbackManager.Scripts.RegisterClientScriptInclude("CheckRadioListJs", Page.ClientScript.GetWebResourceUrl(this.GetType(), "ZYH.WebControl_V.Js.CheckRadioList.js")); }
                    st += "var " + ClientID + "Instance = new Class_CheckRadioList('" + ClientID + "'," + base._CallbackOptions.ToJson_Js() + "," + _Settings.ToJson_Js() + ");\r\n";
                    st += ClientID + "Instance.Init();\r\n";
                    CallbackManager.Scripts.RegisterStartupScript(ClientID + "InitJs", st);
                    break;
            }
            if (Fill != null)
            {
                if (!CallbackManager.Scripts.IsScriptRegistered(ClientID + "_FillJs"))
                {
                    if (ClientMethod_Fill == "") throw new Exception("Please set 'ClientMethod_Fill' for control '" + ID + "'.");
                    st = "";
                    st += "function " + ClientMethod_Fill + "(ArgToServer) {\r\n";
                    st += ClientID + "Instance.OnFill(ArgToServer);\r\n";
                    st += "}\r\n";
                    CallbackManager.Scripts.RegisterClientScriptBlock(ClientID + "_FillJs", st);
                }
            }

            base.OnBeforeRender();
        }
    }
}
