using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZYH.WebControl_V
{
    public enum AsynchronousCallOptions
    {
        AbortPreviousCalls = 1,
        BlockLaterCalls = 2,
        QueueCalls = 3,
        MultipleCalls = 4
    }

    public enum AfterCallActions
    {
        NA = 0,
        RefreshToSynchro = 1,
        RecallToSynchro = 11,

        TreeView_RefreshSelf = 101,
        TreeView_RefreshChild = 102,
        TreeView_RefreshAll = 103,
        TreeView_Exception_CannotMoveToChild = 111
    }

    public enum CallWaitingBehaviors
    {
        NA = -1,
        MovingProcessingIcon = 1,
        CenteredProcessingIcon = 2,
        WaitingCursor = 3,
        Cover = 4,
        CoverAndProcessingIcon = 5,
        CoverAndWaitingCursor = 6,
        CoverAndCenteredProcessingIcon = 7
    }

    public enum OnExceptionActions
    {
        NA = -1,
        FriendlyMessage = 1,
        DebugDetail = 2,
        ClientEvent = 10,
    }

    public enum CallbackCommands
    {
        Grid_RowDataBound = -11,

        NA = -1,

        LinkButton_Click = 1,
        ListBox_Fill = 2,
        ListBox_SelectedIndexChanged = 3,
        DropDownList_Fill = 4,
        DropDownList_SelectedIndexChanged = 5,
        TextBox_KeyDown = 6,
        TextBox_KeyUp = 7,
        TextBox_GetFocus = 8,
        TextBox_LostFocus = 9,
        TextBox_TextChanged = 10,
        CheckBox_CheckChanged = 11,
        CheckBoxList_Fill = 12,
        CheckBoxList_SelectedIndexChanged = 13,
        RadioButtonList_Fill = 14,
        RadioButtonList_SelectedIndexChanged = 15,
        DirectCaller_CallServer = 16,
        Calendar_DateChanged = 17,
        Window_LoadFromServer = 18,
        Window_Close = 19,
        ThemeSelector_ThemeSelected = 20,
        History_Navigate = 21,
        Panel_LoadFromServer = 22,

        Grid_PageIndexChanged = 31,
        Grid_Sorted = 32,
        Grid_ExternalCommand = 33,
        Grid_RowSelected = 34,

        TreeView_NodeSelected = 121,
        TreeView_ExpanderClicked = 122,
        TreeView_NodeHover = 123,
        TreeView_NodeChecked = 124,
        TreeView_NodeMove = 125,
    }

    public enum LoadJQueryFrom
    {
        NA = -1,
        AspNetCDN = 1,

    }

    public enum ScriptTypes
    {
        Css = -1,
        Include = 1,
        Block = 2,
        Startup = 3
    }

    public enum ScriptResourceLoadFrom
    {
        Disabled = -1,
        BuildIn = 1,
        CDN = 2
    }

    public enum ListControlTypes
    {
        DropDownList = 1,
        ListBox = 2,
        CheckBoxList = 3,
        RadioButtonList = 4
    }

    public enum MouseKeys
    {
        NA = -1,
        LeftKey = 1,
        RightKey = 2,
        MiddleKey = 3
    }

    public enum JQueryThemes
    {
        NA = -1,
        BlackTie = 1,
        Blitzer = 2,
        Cupertino = 3,
        DarkHive = 4,
        DotLuv = 5,
        Eggplant = 6,
        ExciteBike = 7,

        Flick = 8,
        HotSneaks = 9,
        Humanity = 10,
        LeFrog = 11,
        MintChoc = 12,
        Overcast = 13,
        PepperGrinder = 14,
        Redmond = 15,

        Smoothness = 16,
        SouthStreet = 17,
        Start = 18,
        Sunny = 19,
        SwankyPurse = 20,
        Trontastic = 21,
        Darkness = 22,
        Lightness = 23,
        Vader = 24
    }

    public enum DateFormats
    {
        mmddyy = 1,
        yymmdd = 2,
        ddmmyy = 3

    }
}
