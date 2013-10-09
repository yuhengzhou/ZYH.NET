function CheckBox_OnCheckedChanged(CallbackManager_ClientInstance, UniqueID, ClientID, CallbackCommand, CommandArg, ClientEvent_BeforeCallback, ClientEvent_AfterCallback, CancelCall, EventTriggerHierarchyID, CallbackOptions) {
    var checked;
    if ($('#' + ClientID + ':checked').length == 0) checked = false; else checked = true;
    //Call Server
    var e_BeforeCall = { 'CancelCall': CancelCall, 'ArgFromClient': '', 'CommandArg': CommandArg, 'EventTriggerHierarchyID': EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
    e_BeforeCall.ControlReserved = checked.toString();
    e_BeforeCall.Checked = checked;
    CallbackManager_ClientInstance.CallServer(UniqueID, ClientID, CallbackCommand, ClientEvent_BeforeCallback, e_BeforeCall, CheckBox_AfterCallProcessing, ClientEvent_AfterCallback);
}

function CheckBox_AfterCallProcessing(id, e_AfterCall) {
    e_AfterCall.Checked = e_AfterCall.ControlReserved.toLowerCase() === 'true';
    e_AfterCall.CommandArg = e_AfterCall.Context.e_BeforeCall.CommandArg;
    var ControlChecked;
    if ($('#' + id + ':checked').length == 0) ControlChecked = false; else ControlChecked = true;
    if (ControlChecked != e_AfterCall.Checked) {
        switch (e_AfterCall.CallbackOptions.AfterCallAction) {
            case 0://'NA':
                break;
            case 11://'RecallToSynchro':
                e_AfterCall.Context.e_BeforeCall.ControlReserved = ControlChecked.toString();
                e_AfterCall.ReFiredCallback = true;
                e_AfterCall.CallbackManager_ClientInstance.DelayReFireCallback(e_AfterCall.Context);
                break;
            case 1://'RefreshToSynchro':
                document.getElementById(id).checked = (e_AfterCall.Checked.toLowerCase() === 'true');
                break;
        }
    }
}
