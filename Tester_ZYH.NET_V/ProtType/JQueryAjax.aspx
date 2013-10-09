<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JQueryAjax.aspx.cs" Inherits="Tester_ZYH.NET_V.test_ajax.JQueryAjax" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery-1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        function CallServer(onError, onComplete, Context) {
            var p = {
                type: 'post',
                timeout: 25000,
                data: { __EVENTTARGET: '', __EVENTARGUMENT: '', __CALLBACKPARAM: 'vvv', __CALLBACKID: 'TestLink1s' },
                success: OnSuccess,
                beforeSend: SetWaitingStatus
            };
            if (onError) { p.error = Error; } else { p.error = OnError; }
            if (onComplete) { p.complete = Complete; } else { p.complete = CancelWaitingStatus; }
            if (Context) { p.context = Context; }
            $.ajax(p);
        }

        function OnError() { }
        function SetWaitingStatus() { }
        function CancelWaitingStatus() { }

        function OnSuccess(Result, Status) {
            alert(Result);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a id="callServer" href="javascript:CallServer();">Call Server</a>
        <br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
    </div>
    <cc1:TestLink ID="TestLink1" runat="server" />
    </form>
</body>
</html>
