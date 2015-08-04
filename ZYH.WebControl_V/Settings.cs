using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Xml;

namespace ZYH.WebControl_V
{
    public class CallbackManagerSetting : SerializableObjectBase
    {
        public string ClientInstanceName = "CallbackManagerInstance";
        public bool EnableHistory = false;
        public int CallWaitingTimeOut = 15;
        public CallWaitingBehaviors CallWaitingBehavior = CallWaitingBehaviors.MovingProcessingIcon;
        public string CallWaitingCoverColor = "Black";
        public decimal CallWaitingCoverTransparency = 0.3M;
        public string CallWaitingIcon = "";
        public OnExceptionActions OnExceptionActions = OnExceptionActions.DebugDetail;
        public bool ServerEvents_Navigate = false;
        public string ClientEvent_BeforeCall_Navigate = "";
        public string ClientEvent_AfterCall_Navigate = "";
        public string ClientEvent_OnException = "";
        public string ClientEvent_OnCallTimeOut = "";
    }

    [Serializable]
    public class ControlSetting_Base : SerializableObjectBase
    {
        [XmlElement("CMCI"), DefaultValue("")]
        public string CallbackManagerClientInstance = "";
        [XmlElement("UID"), DefaultValue("")]
        public string UniqueID = "";
        [XmlElement("HID"), DefaultValue("")]
        public string EventTriggerHierarchyID = "";
        [XmlElement("CA"), DefaultValue("")]
        public string CommandArg = "";
    }

    #region ListControl
    [Serializable]
    public class ListControlSettings : ControlSetting_Base
    {
        public System.Web.UI.WebControls.TextAlign TextAlign = System.Web.UI.WebControls.TextAlign.Right;
        public int RepeatColumns = 0;
        public System.Web.UI.WebControls.RepeatDirection RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Vertical;
        public int CellPadding = -1;
        public int CellSpacing = -1;
        public ListControlTypes ListControlType;
        public bool ServerEvents_Fill = false;
        public bool ServerEvents_SelectedIndexChanged = false;
        public string ClientEvent_BeforeCallback_SelectedIndexChanged = "";
        public string ClientEvent_AfterCallback_SelectedIndexChanged = "";
        public string ClientEvent_BeforeCallback_Fill = "";
        public string ClientEvent_AfterCallback_Fill = "";
    }
    #endregion

    #region TextBox
    [Serializable]
    public class TextBoxSettings : ControlSetting_Base
    {
        public string ClientEvent_BeforeCallback_GetFocus = "";
        public string ClientEvent_AfterCallback_GetFocus = "";
        public string ClientEvent_BeforeCallback_LostFocus = "";
        public string ClientEvent_AfterCallback_LostFocus = "";
        public string ClientEvent_BeforeCallback_KeyDown = "";
        public string ClientEvent_AfterCallback_KeyDown = "";
        public string ClientEvent_BeforeCallback_KeyUp = "";
        public string ClientEvent_AfterCallback_KeyUp = "";

        public int KeyUpEventDelay = 200;
        public string OnFocusBackColor = "MintCream";

        public bool ServerEvents_KeyDown = false;
        public bool ServerEvents_KeyUp = false;
        public bool ServerEvents_GetFocus = false;
        public bool ServerEvents_LostFocus = false;
    }

    #endregion

    #region Calendar
    [Serializable]
    public class CalendarSetting : ControlSetting_Base
    {
        public JQueryThemes Theme = JQueryThemes.Darkness;
        public string DateFormat = "mm/dd/yy";
        public string MaxDate = "";
        public string MinDate = "";

        public bool ServerEvents_DateChanged = false;
        public string ClientEvent_BeforeCallback_DateChanged = "";
        public string ClientEvent_AfterCallback_DateChanged = "";

        public string BackColor = "";
        public string OnFocusBackColor = "MintCream";
        public string InvalidValueBackColor = "#FF99CC";
    }
    #endregion

    #region Window
    [Serializable]
    public class WindowSetting : ControlSetting_Base
    {
        public string ClientMethod_LoadFromServer = "";
        public string ClientEvent_BeforeCallback_LoadFromServer = "";
        public string ClientEvent_AfterCallback_LoadFromServer = "";
        public string ClientEvent_BeforeCallback_Close = "";
        public string ClientEvent_AfterCallback_Close = "";

        public bool ServerEvents_LoadFromServer = false;
        public bool ServerEvents_Close = false;

        public bool IsModelWindow = false;
        public bool DragAble = true;
        public bool HasCloseButton = true;
        public bool DragLimit0x0 = true;
        public string CloseButtonUrl = "";
        public string CloseButtonUrl_MouseOver = "";
        public bool HasMinimalButton = false;
        public string MinimalButtonUrl = "";
        public string MinimalButtonUrl_MouseOver = "";

        public int Width;
        public int Height;

        public string CssClass_Frame = "";
        public string CssClass_Header = "";
        public string CssClass_CloseBotton = "";
        public string CssClass_MinimalBotton = "";

        public string Title = "";
    }
    #endregion

    #region Panel
    [Serializable]
    public class PanelSetting : ControlSetting_Base
    {
        public string ClientMethod_LoadFromServer = "";
        public string ClientEvent_BeforeCallback_LoadFromServer = "";
        public string ClientEvent_AfterCallback_LoadFromServer = "";

        public bool ServerEvents_LoadFromServer = false;
    }
    #endregion

    #region ThemeSelector
    [Serializable]
    public class ThemeSelectorSetting : ControlSetting_Base
    {
        public bool ServerEvents_ThemeSelected = false;
        public string ClientEvent_BeforeCallback_ThemeSelected = "";
        public string ClientEvent_AfterCallback_ThemeSelected = "";
    }
    #endregion
}
