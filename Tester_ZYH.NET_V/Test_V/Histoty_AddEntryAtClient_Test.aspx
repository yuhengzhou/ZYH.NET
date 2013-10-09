<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Histoty_AddEntryAtClient_Test.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Test_V.Histoty_AddEntryAtClient_Test" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function LinkButton_BeforeCall(id, e) {
        }

        function LinkButton_AfterCall(id, e) {
            $('#PlaceHolder').html(e.ArgToClient);
            var entry = id.substring(id.length - 1);
            //<%= CallbackManager1.ClientInstanceName %>.AddHistoryPoint(entry);
            CallbackManagerInstance.AddHistoryPoint(entry, 'Step: ' + entry);
        }

        function Navigate_afterCall(id, e) {
            if (e.HistoryPoint.Entry == '') { document.title = ''; } else { document.title = e.HistoryPoint.Title; }
            $('#PlaceHolder').html(e.ArgToClient);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:CallbackManager ID="CallbackManager1" runat="server" EnableHistory="True" OnNavigate="CallbackManager1_Navigate"
            ClientEvent_AfterCall_Navigate="Navigate_afterCall">
        </cc1:CallbackManager>
        <cc1:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" ClientEvent_AfterCallback="LinkButton_AfterCall"
            UpdateViewState="True">Load Tab 1</cc1:LinkButton>
        &nbsp;
        <cc1:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" ClientEvent_AfterCallback="LinkButton_AfterCall"
            UpdateViewState="True">Load Tab 2</cc1:LinkButton>
        &nbsp;
        <cc1:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" ClientEvent_AfterCallback="LinkButton_AfterCall"
            UpdateViewState="True">Load Tab 3</cc1:LinkButton>
        <div id="PlaceHolder" runat="server">
        </div>
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <br />
    <br />
    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">LinkButton</asp:LinkButton>
    </form>
</body>
</html>
