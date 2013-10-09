function Class_Grid(ClientID, CallbackOptions) {
    var Self = this;
    Self.HidenField = $('#' + ClientID + '_hd');
    Self.Control = $('#' + ClientID + '_Grid');
    Self.Xml;
    Self.Html;

    Self.Serialize = function () {
        var xml = '<Grid>';
        xml += '<SET>';
        xml += '<CMCI>' + Self.Settings.CallbackManagerClientInstance + '</CMCI>';
        xml += '<UID>' + Self.Settings.UniqueID + '</UID>';
        xml += '<HID>' + Self.Settings.EventTriggerHierarchyID + '</HID>';
        if (Self.Settings.CommandArg !== '') xml += '<CA>' + Self.Settings.CommandArg + '</CA>';
        if (Self.Settings.PageSize != 10) xml += '<PS>' + Self.Settings.PageSize + '</PS>';
        if (Self.Settings.PageIndex != 0) xml += '<PI>' + Self.Settings.PageIndex + '</PI>';
        if (Self.Settings.SortExpression != '') xml += '<SE>' + Self.Settings.SortExpression + '</SE>';
        if (Self.Settings.SortDirection != 'Ascending') xml += '<SD>' + Self.Settings.SortDirection + '</SD>';
        if (Self.Settings.ServerEvent_PageIndexChanged) xml += '<SE_PIC>true</SE_PIC>';
        if (Self.Settings.ServerEvent_Sorted) xml += '<SE_S>true</SE_S>';
        if (Self.Settings.ServerEvent_RowSelected) xml += '<SE_RS>true</SE_RS>';
        if (Self.Settings.ClientMethod_ExternalCommand != '') xml += '<CM_EC>' + Self.Settings.ClientMethod_ExternalCommand + '</CM_EC>';
        if (Self.Settings.ClientEvent_AfterCallback_ExternalCommand != '') xml += '<CM_EC>' + Self.Settings.ClientEvent_AfterCallback_ExternalCommand + '</CM_EC>';
        if (Self.Settings.ExternalCommand !== '') xml += '<EC>' + Self.Settings.ExternalCommand + '</EC>';
        if (Self.Settings.ExternalCommandArg !== '') xml += '<ECA>' + Self.Settings.ExternalCommandArg + '</ECA>';
        if (Self.Settings.ClientEvent_AfterCallback_ExternalCommand != '') xml += '<CE_A_EC>' + Self.Settings.ClientEvent_AfterCallback_ExternalCommand + '</CE_A_EC>';
        if (Self.Settings.ClientEvent_BeforeCallback_RowSelected != '') xml += '<CE_B_RS>' + Self.Settings.ClientEvent_BeforeCallback_RowSelected + '</CE_B_RS>';
        if (Self.Settings.ClientEvent_AfterCallback_RowSelected != '') xml += '<CE_A_RS>' + Self.Settings.ClientEvent_AfterCallback_RowSelected + '</CE_A_RS>';
        if (Self.Settings.SelectedRowIndex != -1) xml += '<SRI>' + Self.Settings.SelectedRowIndex + '</SRI>';
        xml += '</SET>';
        xml += '</Grid>';
        return xml;
    }
    Self.Deserialize = function () {
        var DOM = XMLtoDOM(Self.Xml);
        var Setting_DOM = $('SET', DOM);
        Self.Settings = new Object();
        Self.Settings.CallbackManagerClientInstance = $('CMCI', Setting_DOM).text();
        Self.Settings.UniqueID = $('UID', Setting_DOM).text();
        Self.Settings.EventTriggerHierarchyID = $('HID', Setting_DOM).text();
        Self.Settings.CommandArg = $('CA', Setting_DOM).text();
        Self.Settings.ClientEvent_CallTimeOutHandler = $('CE_CTO', Setting_DOM).text();
        var EA = $('EA', Setting_DOM).text(); if (EA == '') { Self.Settings.ExceptionAction = 'ShortDebugMessage'; } else { Self.Settings.ExceptionAction = EA; }
        Self.Settings.ClientEvent_ExceptionHandler = $('CE_EH', Setting_DOM).text();
        var ACO = $('ACO', Setting_DOM).text(); if (ACO == '') { Self.Settings.AsynchronousCallOptions = 'BlockLaterCalls'; } else { Self.Settings.AsynchronousCallOptions = ACO; }
        var US = $('US', Setting_DOM).text(); if (US == '') { Self.Settings.UpdateState = true; } else { Self.Settings.UpdateState = false; }

        Self.Settings.PageSize = $('PS', Setting_DOM).text(); if (Self.Settings.PageSize == '') Self.Settings.PageSize = 10; else Self.Settings.PageSize = parseInt(Self.Settings.PageSize);
        Self.Settings.PageIndex = $('PI', Setting_DOM).text(); if (Self.Settings.PageIndex == '') Self.Settings.PageIndex = 0; else Self.Settings.PageIndex = parseInt(Self.Settings.PageIndex);
        Self.Settings.SortExpression = $('SE', Setting_DOM).text();
        Self.Settings.SortDirection = $('SD', Setting_DOM).text(); if (Self.Settings.SortDirection == '') Self.Settings.SortDirection = 'Ascending';
        var SE_PIC = $('SE_PIC', Setting_DOM).text(); if (SE_PIC == '') { Self.Settings.ServerEvent_PageIndexChanged = false; } else { Self.Settings.ServerEvent_PageIndexChanged = (SE_PIC.toLowerCase() == 'true'); }
        var SE_S = $('SE_S', Setting_DOM).text(); if (SE_S == '') { Self.Settings.ServerEvent_Sorted = false; } else { Self.Settings.ServerEvent_Sorted = (SE_S.toLowerCase() == 'true'); }
        var SE_RS = $('SE_RS', Setting_DOM).text(); if (SE_RS == '') { Self.Settings.ServerEvent_RowSelected = false; } else { Self.Settings.ServerEvent_RowSelected = (SE_RS.toLowerCase() == 'true'); }
        Self.Settings.ClientMethod_ExternalCommand = $('CM_EC', Setting_DOM).text();
        Self.Settings.ExternalCommand = $('EC', Setting_DOM).text();
        Self.Settings.ExternalCommandArg = $('ECA', Setting_DOM).text();

        Self.Settings.ClientEvent_AfterCallback_ExternalCommand = $('CE_A_EC', Setting_DOM).text();
        Self.Settings.ClientEvent_BeforeCallback_RowSelected = $('CE_B_RS', Setting_DOM).text();
        Self.Settings.ClientEvent_AfterCallback_RowSelected = $('CE_A_RS', Setting_DOM).text();
        Self.Settings.SelectedRowIndex = $('SRI', Setting_DOM).text(); if (Self.Settings.SelectedRowIndex == '') Self.Settings.SelectedRowIndex = -1; else Self.Settings.SelectedRowIndex = parseInt(Self.Settings.SelectedRowIndex);

        Self.Html = $('Html', DOM).text();
    }

    Self.Init = function () {
        Self.Xml = $.base64Decode(Self.HidenField.val());
        Self.Deserialize();
    }

    Self.PreviousClick = function () {
        Self.PageingClick(Self.Settings.PageIndex - 1);
    }

    Self.PageingClick = function (PageIndex) {
        Self.Settings.PageIndex = PageIndex;

        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvent_PageIndexChanged, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize();
        e_BeforeCall.PageIndex = PageIndex;
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '31', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.NextClick = function () {
        Self.PageingClick(Self.Settings.PageIndex + 1);
    }

    Self.SortClick = function (SortExpression) {
        if (Self.Settings.SortExpression == SortExpression) {
            if (Self.Settings.SortDirection == 'Ascending') Self.Settings.SortDirection = 'Descending'; else Self.Settings.SortDirection = 'Ascending';
        }
        else
        { Self.Settings.SortExpression = SortExpression; Self.Settings.SortDirection = 'Ascending'; }

        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvent_Sorted, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize();
        e_BeforeCall.SortExpression = Self.Settings.SortExpression;
        e_BeforeCall.SortDirection = Self.Settings.SortDirection;
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '32', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.OnExternalCommand = function (ExternalCommand, ExternalCommandArg) {
        Self.Settings.ExternalCommand = ExternalCommand;
        Self.Settings.ExternalCommandArg = ExternalCommandArg;

        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': false, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize();
        e_BeforeCall.SortExpression = Self.Settings.SortExpression;
        e_BeforeCall.SortDirection = Self.Settings.SortDirection;
        if (Self.Settings.ClientEvent_AfterCallback_ExternalCommand != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_ExternalCommand); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '33', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.OnRowSelected = function (SelectedRowIndex) {
        Self.Settings.SelectedRowIndex = SelectedRowIndex;

        var FunBeforeCall = null;
        var FunAfterCall = null;
        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvent_RowSelected, 'ArgFromClient': '', 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = Self.Serialize();
        e_BeforeCall.SelectedRowIndex = SelectedRowIndex;
        if (Self.Settings.ClientEvent_BeforeCallback_RowSelected != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_RowSelected); }
        if (Self.Settings.ClientEvent_AfterCallback_RowSelected != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_RowSelected); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '34', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.AfterCallProcessing = function (id, e_AfterCall) {
        Self.Xml = e_AfterCall.ControlReserved;
        Self.Deserialize();
        Self.HidenField.val($.base64Encode(Self.Serialize()));

        switch (e_AfterCall.CallbackOptions.AfterCallAction) {
            case 11: //'RecallToSynchro':
                var xml = Self.Serialize();
                if (e_AfterCall.Context.e_BeforeCall.ControlReserved != xml) {
                    e_AfterCall.Context.e_BeforeCall.ControlReserved = xml;
                    e_AfterCall.ReFiredCallback = true;
                    e_AfterCall.CallbackManager_ClientInstance.DelayReFireCallback(e_AfterCall.Context);
                    break;
                }
                else {
                    Self.Clear();
                    Self.Control.append(Self.Html);
                }
                break;
            case 0: //'NA':
            case 1: //'RefreshToSynchro':
                Self.Clear();
                Self.Control.append(Self.Html);
                break;
        }
    }

    Self.Clear = function () {
        $('tr', Self.Control).remove();
    }
}