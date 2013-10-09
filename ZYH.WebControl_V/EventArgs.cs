using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace ZYH.WebControl_V
{
    public class CallbackEventArgs : EventArgs
    {
        public CallbackCommands Command = CallbackCommands.NA;
        public CallbackManager CallbackManager;
        public string ArgFromClient = "";
        public string ArgToClient = "";
        public string CommandArg = "";
        private CallbackOptions _CallbackOptions = new CallbackOptions();
        public CallbackOptions CallbackOptions { get { return _CallbackOptions; } set { _CallbackOptions = value; } }
    }

    public class HistoryPoint
    {
        public string Entry = "";
        public string Title = "";

        public override string ToString()
        {
            return Title.Length + "|" + Title + "|" + Entry.Length + "|" + Entry;
        }
    }

    public class NavigateEventArgs : CallbackEventArgs
    {
        public HistoryPoint HistoryPoint;
    }

    public class CheckBoxEventArgs : CallbackEventArgs
    {
        public bool Checked;
    }

    public class ListControlEventArgs : CallbackEventArgs
    {
        public List<ListItem> SelectedItems;
    }

    public class TextBoxEventArg : CallbackEventArgs
    {
        public string Text = "";
        public int KeyCode;
        public MouseKeys MouseKey = MouseKeys.NA;
        public bool Ctrl = false;
        public bool Alt = false;
        public bool Shift = false;
        public int CursorPosition = 0;
    }

    public class CalendarEventArgs : CallbackEventArgs
    {
        public DateTime SelectedDate;
        public bool IsValueValid;
    }

    public class WindowEventArgs : CallbackEventArgs
    {
        public string Title = "";
        public string HtmlToBeLoaded = "";
        public bool RenderMe;
        public bool IsInitLoad;
        public bool IsLoaded;
    }

    public class PanelEventArgs : CallbackEventArgs
    {
        public string HtmlToBeLoaded = "";
        public bool RenderMe;
        public bool IsInitLoad;
        public bool IsLoaded;
    }

    public class ThemeSelectorEventArgs : CallbackEventArgs
    {
        public string SelectedThemeValue;
    }
}
