using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZYH.WebControl_V
{
    [DefaultEvent("Navigate"), ToolboxItem(false)]
    public class History : CallbackControl_Base
    {
        #region Event

        public override void OnRaiseCallbackEvent(CallbackParameters CallbackParameters, CallbackResult CallbackResult)
        {
            NavigateEventArgs e = new NavigateEventArgs();
            e.CallbackManager = this.CallbackManager;
            e.ArgFromClient = CallbackParameters.ArgFromClient;
            e.Command = CallbackParameters.Command;
            e.CallbackManager = this.CallbackManager;
            //e.CommandArg = this.CommandArg;
            e.CallbackOptions = this._CallbackOptions;
            e.HistoryPoint = new HistoryPoint() { Entry = CallbackParameters.ControlReserved };
            CallbackManager.OnNavigate(e);
            CallbackResult.ArgToClient = e.ArgToClient;
            CallbackResult.CallbackOptions = e.CallbackOptions;
            CallbackResult.ControlReserved = e.HistoryPoint.ToString();
        }
        #endregion

        #region Property
        internal CallbackOptions CallbackOptions { get { return _CallbackOptions; } }
        #endregion
    }
}
