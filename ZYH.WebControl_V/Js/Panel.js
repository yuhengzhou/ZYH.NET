function Class_Panel(ClientID, CallbackOptions, Settings) {
    var Self = this;
    Self.Control = $('#' + ClientID); //Frame
    Self.Settings = Settings;

    Self.Init = function () {
    }

    Self.OnLoadFromServer = function (ArgFromClient) {
        var FunBeforeCall = null;
        var FunAfterCall = null;

        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_LoadFromServer, 'ArgFromClient': ArgFromClient, 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = '';
        if (Self.Settings.ClientEvent_BeforeCallback_LoadFromServer != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_LoadFromServer); }
        if (Self.Settings.ClientEvent_AfterCallback_LoadFromServer != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_LoadFromServer); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '22', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.AfterCallProcessing = function (id, e_AfterCall) {
        switch (e_AfterCall.CallbackOptions.AfterCallAction) {
            case 0: //'NA':
            case 11: //'RecallToSynchro':
            case 1: //'RefreshToSynchro':
                Self.Control.html(e_AfterCall.ControlReserved);
                break;
        }
    }
}