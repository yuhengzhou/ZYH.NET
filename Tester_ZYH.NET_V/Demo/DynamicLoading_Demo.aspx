<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicLoading_Demo.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Demo.DynamicLoading_Demo" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function LoadTab_AfterCall(id, e) {
            $('#PlaceHolder_Tab').html(e.ArgToClient);
        }
        function Navigate_AfterCall(id, e) {
            if (e.HistoryPoint.Title != '') document.title = e.HistoryPoint.Title;
            $('#PlaceHolder_Tab').html(e.ArgToClient);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:CallbackManager ID="CallbackManager1" runat="server" EnableHistory="True" 
        ClientEvent_AfterCall_Navigate="Navigate_AfterCall" 
        onnavigate="CallbackManager1_Navigate">
    </cc1:CallbackManager>
    <asp:Label ID="Label1" runat="server" Text="Incrementally Load HTML and Corresponding Resources"
        Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="(Dynamic Loading)" Font-Bold="True" 
        Font-Size="Large"></asp:Label>
    <br />
    <table style="width: 100%;">
        <tr>
            <td>
                &nbsp;
                <cc1:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" 
                    ClientEvent_AfterCallback="LoadTab_AfterCall">Tab 1</cc1:LinkButton>
            </td>
            <td>
                &nbsp;
                <cc1:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click" 
                    ClientEvent_AfterCallback="LoadTab_AfterCall">Tab 2</cc1:LinkButton>
            </td>
            <td>
                &nbsp;
                <cc1:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" 
                    ClientEvent_AfterCallback="LoadTab_AfterCall">Tab 3</cc1:LinkButton>
            </td>
        </tr>
    </table>
    <div id="PlaceHolder_Tab" runat="server">
    </div>
    </form>
    </body>
</html>
