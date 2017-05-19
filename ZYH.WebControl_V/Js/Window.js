var ZYH_WebControl_IV_ActiveWindows = new Array();

function Class_Window(ClientID, CallbackOptions, Settings) {
    var Self = this;
    Self.Control = $('#' + ClientID); //Frame
    Self.Settings = Settings;
    Self.Top = -1;
    Self.Left = -1;
    Self.IsUnderDrag = false;
    var DragOffSet = { x: 0, y: 0 };

    Self.Init = function () {
    }

    Self.OnLoadFromServer = function (ArgFromClient, Top, Left) {
        if (typeof (ArgFromClient) === 'undefined') { ArgFromClient = ''; }
        if (Top) { Self.Top = Top; }
        if (Left) { Self.Left = Left; }

        var FunBeforeCall = null;
        var FunAfterCall = null;

        var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_LoadFromServer, 'ArgFromClient': ArgFromClient, 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
        e_BeforeCall.ControlReserved = '';
        if (Self.Settings.ClientEvent_BeforeCallback_LoadFromServer != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_LoadFromServer); }
        if (Self.Settings.ClientEvent_AfterCallback_LoadFromServer != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_LoadFromServer); }
        eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '18', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
    }

    Self.AfterCallProcessing = function (id, e_AfterCall) {
        switch (e_AfterCall.CallbackOptions.AfterCallAction) {
            case 0: //'NA':
            case 11: //'RecallToSynchro':
            case 1: //'RefreshToSynchro':
                if (e_AfterCall.Context.Command == '18') {
                    var DOM = XMLtoDOM(e_AfterCall.ControlReserved);
                    var Title = $('Title', DOM).text();
                    var Html = $('Html', DOM).text();
                    RenderWindow(Title, Html);
                }
                break;
        }
    }

    Self.InnerHtml = $('#' + ClientID + 'InnerHtml').html();
    Self.Show = function (Title, Html) {
        if (typeof (Title) === 'undefined') { Title = Self.Settings.Title; }
        if (typeof (Html) === 'undefined') { Html = Self.InnerHtml; }
        RenderWindow(Title, Html);
    }

    function RenderWindow(Title, Html) {
        //        var div_Frame = document.createElement('div');
        Self.Control.empty();
        Self.Control.bind('click', ResetZindex);
        if (Self.Settings.CssClass_Frame == '') {
            Self.Control[0].style.backgroundColor = "#FFFFFF";
            Self.Control[0].style.borderStyle = 'solid';
            Self.Control[0].style.borderWidth = '1px';
            Self.Control[0].style.borderColor = '#b2b2b2';
        }
        else
        { Self.Control[0].className = Self.Settings.CssClass_Frame; }
        Self.Control[0].style.position = 'absolute';
        if (Self.Settings.Width != -1) { Self.Control[0].style.width = Self.Settings.Width + 'px'; }
        if (Self.Settings.Height != -1) { Self.Control[0].style.height = Self.Settings.Height + 'px'; }

        var div = document.createElement('div'); Self.Control[0].appendChild(div);
        div.style.padding = '3px';

        var div_Header = document.createElement('div'); div.appendChild(div_Header);
        if (Self.Settings.HasCloseButton) {
            var lk_Close = document.createElement('a'); div_Header.appendChild(lk_Close);
            lk_Close.href = 'javascript:' + ClientID + 'Instance.OnClose();';
            if (Self.Settings.CssClass_CloseBotton == '') {
                var img = $(document.createElement('img')); lk_Close.appendChild(img[0]); img[0].src = Self.Settings.CloseButtonUrl;
                img.hover(function () { this.src = Self.Settings.CloseButtonUrl_MouseOver; }, function () { this.src = Self.Settings.CloseButtonUrl; });
                img.css('float', 'right').css('padding', '3px').css('border', '0');
            } else
            { lk_Close.className = Self.Settings.CssClass_CloseBotton; }
        }
        if (Self.Settings.HasMinimalButton) {
            var lk_Mini = document.createElement('a'); div_Header.appendChild(lk_Mini);
            lk_Mini.href = 'javascript:' + ClientID + 'Instance.OnMini();';
            if (Self.Settings.CssClass_MinimalBotton == '') {
                var img = $(document.createElement('img')); lk_Mini.appendChild(img[0]); img[0].src = Self.Settings.MinimalButtonUrl;
                img.hover(function () { this.src = Self.Settings.MinimalButtonUrl_MouseOver; }, function () { this.src = Self.Settings.MinimalButtonUrl; });
                img.css('float', 'right').css('padding', '3px').css('border', '0');
            } else
            { lk_Mini.className = Self.Settings.CssClass_MinimalBotton; }
        }
        if (Title != '') {
            var span_Title = document.createElement('span'); div_Header.appendChild(span_Title); span_Title.innerHTML = Title;
        }
        if (Self.Settings.DragAble) {
            div_Header.style.cursor = 'move';
            $(div_Header).bind('mousedown', OnStartDrag);
        }
        if (Self.Settings.CssClass_Header == '') {
            div_Header.style.backgroundColor = '#006790';
            div_Header.style.height = '25px';
            div_Header.style.fontSize = '16px';
            div_Header.style.color = '#FFFFFF';
            div_Header.style.fontWeight = 'bold';
            div_Header.style.padding = '5px';
        } else
        { div_Header.className = Self.Settings.CssClass_Header; }

        var div_Content = document.createElement('div'); div_Content.id = ClientID + '_Content'; div.appendChild(div_Content);

        div_Content.innerHTML = Html;
        //$('form')[0].appendChild(Self.Control[0]);
        Self.Control.show();
        if (!ZYH_WebControl_IV_ActiveWindows.contains(Self.Control)) {
            if (Self.Top != -1 && Self.Left != -1) {
                Self.Control[0].style.top = Self.Top + 'px';
                Self.Control[0].style.left = Self.Left + 'px';
            }
            else {
                SetWindowPosition();
            }
            ResetZindex();
            if (Self.Settings.IsModelWindow) {
                eval(Self.Settings.CallbackManagerClientInstance).LoadCover(null, 5000);
            }
            ZYH_WebControl_IV_ActiveWindows.push(Self.Control);
        } else {
            ResetZindex();
        }
    }

    Self.OnMini = function () { $('#' + ClientID + '_Content').toggle(); }

    function SetWindowPosition() {
        var scrollTop = $(window).scrollTop();
        var top = ($(window).height() / 2) - (Self.Control.height() / 2);
        var left = ($(window).width() / 2) - (Self.Control.width() / 2);
        if (Self.Control.offsetParent().length > 0) {
            var offP = Self.Control.offsetParent().offset();
            top -= offP.top;
            left -= offP.left;
        }
        if (top < 0) top = 0;
        if (left < 0) left = 0;
        Self.Control[0].style.top = top + scrollTop + 'px';
        Self.Control[0].style.left = left + 'px';
    }

    function OnStartDrag(ee) {
        if (ee.which == 1) {
            if (!Self.IsUnderDrag) {
                Self.IsUnderDrag = true;

                MousePosition.x = ee.pageX; MousePosition.y = ee.pageY;
                DragOffSet.x = MousePosition.x - Self.Control.position().left;
                DragOffSet.y = MousePosition.y - Self.Control.position().top;
                OnWidgetDrag(ee);
                if ($.browser.msie) {
                    Self.Control[0].style.filter = 'alpha(opacity=80)';
                    document.onselectstart = function () { return false; }
                }
                else {
                    Self.Control[0].style.opacity = '0.8';
                }
                $(document).bind('mousemove', OnWidgetDrag);
                $(document).bind('mouseup', OnWidgetDrop);
            }
        }

        ResetZindex();
        return false;
    }

    function OnWidgetDrag(ee) {
        MousePosition.x = ee.pageX; MousePosition.y = ee.pageY;
        var top = MousePosition.x - DragOffSet.x;
        var left = MousePosition.y - DragOffSet.y;
        if (Self.Settings.DragLimit0x0) {
            if (top < 0) { top = 0; }
            if (left < 0) { left = 0; }
        }
        Self.Control[0].style.left = top + "px";
        Self.Control[0].style.top = left + "px";
    }

    function OnWidgetDrop(ee) {
        $(document).unbind('mousemove', OnWidgetDrag);
        $(document).unbind('mouseup', OnWidgetDrop);
        if ($.browser.msie) {
            Self.Control[0].style.filter = '';
            document.onselectstart = null;
        }
        else {
            Self.Control[0].style.opacity = '1';
        }
        Self.IsUnderDrag = false;
    }

    function ResetZindex() {
        for (var i = 0; i < ZYH_WebControl_IV_ActiveWindows.length; i++) {
            ZYH_WebControl_IV_ActiveWindows[i].css('zIndex', '4900');
        }
        Self.Control.css('zIndex', '5100');
    }

    Self.Close = function (ArgFromClient) { Self.OnClose(ArgFromClient); }

    Self.OnClose = function (ArgFromClient) {
        if (typeof (ArgFromClient) === 'undefined') { ArgFromClient = ''; }
        try {
            Self.Control.hide();
            Self.Control.unbind('click', ResetZindex);
            ZYH_WebControl_IV_ActiveWindows.remove(Self.Control);
            if (Self.Settings.IsModelWindow) {
                eval(Self.Settings.CallbackManagerClientInstance).UnloadCover();
            }
        } catch (err) { }

        var M = eval(Self.Settings.CallbackManagerClientInstance);
        var s = M.SetControlState(ClientID, 'IL', 'False');

        if (Self.Settings.ServerEvents_Close) {
            var FunBeforeCall = null;
            var FunAfterCall = null;
            var e_BeforeCall = { 'CancelCall': !Self.Settings.ServerEvents_LoadFromServer, 'ArgFromClient': ArgFromClient, 'CommandArg': Self.Settings.CommandArg, 'Instance': Self, 'EventTriggerHierarchyID': Self.Settings.EventTriggerHierarchyID, 'CallbackOptions': CallbackOptions };
            e_BeforeCall.ControlReserved = '';
            if (Self.Settings.ClientEvent_BeforeCallback_Close != '') { FunBeforeCall = eval(Self.Settings.ClientEvent_BeforeCallback_Close); }
            if (Self.Settings.ClientEvent_AfterCallback_Close != '') { FunAfterCall = eval(Self.Settings.ClientEvent_AfterCallback_Close); }
            eval(Self.Settings.CallbackManagerClientInstance).CallServer(Self.Settings.UniqueID, ClientID, '19', FunBeforeCall, e_BeforeCall, Self.AfterCallProcessing, FunAfterCall);
        }
    }
}