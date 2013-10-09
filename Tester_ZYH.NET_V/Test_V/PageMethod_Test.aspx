<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageMethod_Test.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Test_V.PageMethod_Test" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Call_A() {
            var p = { "Name": "xxx", "Sex": true, "Salary": 10000 };
            var rP = A(p, 1500, BeforCall, AfterCall);
        }
        function BeforCall(id, e) {
            e.OverrideCallbackManagerSettings.OnExceptionActions = 10;
            e.OverrideCallbackManagerSettings.ClientEvent_OnException = OnError;
        }
        function AfterCall(id, e) {

        }
        function OnError(id, state, ErrorThrown, context) {
            alert(context.XHR.responseText);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:CallbackManager ID="CallbackManager1" runat="server" EnablePageMethods="True">
        </cc1:CallbackManager>
        <br />
    </div>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:Call_A();">Call Page Method A</asp:HyperLink>
    </form>
</body>
</html>
