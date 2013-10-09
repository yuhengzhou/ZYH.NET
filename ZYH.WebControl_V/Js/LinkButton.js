function SwitchImg(objId, imgUrl) {
    document.getElementById(objId).src = imgUrl;
}

function LinkButton_DoCallback(CallbackManager_ClientInstance, UniqueID, ClientID, CallbackCommand, CommandArg, ClientEvent_BeforeCallback, ClientEvent_AfterCallback, CancelCall, EventTriggerHierarchyID, CallbackOptions) {
    var e_BeforeCall = { 'CancelCall': CancelCall, 'ArgFromClient': '', 'CommandArg': CommandArg, 'EventTriggerHierarchyID': EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
    e_BeforeCall.ControlReserved = '';
    CallbackManager_ClientInstance.CallServer(UniqueID, ClientID, CallbackCommand, ClientEvent_BeforeCallback, e_BeforeCall, LinkButton_AfterCallProcessing, ClientEvent_AfterCallback);
}

function LinkButton_AfterCallProcessing(id, e_AfterCall) {
    e_AfterCall.CommandArg = e_AfterCall.Context.e_BeforeCall.CommandArg;
}

notifyScriptLoaded();