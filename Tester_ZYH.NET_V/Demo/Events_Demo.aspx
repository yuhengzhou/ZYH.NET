<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events_Demo.aspx.cs" Inherits="Tester_ZYH.NET_V.Demo.Events_Demo" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function JsFunction_BeforeCall(id, e) {
            e.ArgFromClient = 'xxx';
        }
        function JsFunction_AfterCall(id, e) {
            $('#Label_CallResult').html(e.ArgToClient);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:CallbackManager ID="CallbackManager1" runat="server" CallWaitingTimeOut="90">
        </cc1:CallbackManager>
        <asp:Label ID="Label1" runat="server" Text="Events" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        All actions can triger 3 events. Sequence are &quot;ClientEvent_BeforeCall&quot;,
        &quot;ServerEvent&quot;, and &quot;ClentEvent_AfterCall&quot;. Whether to triger
        each event will depend on whether event handlers have been set.<br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Set Client Events" Font-Bold="True" Font-Size="Large"></asp:Label>
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Demo/Img/Event_Demo/SetClientEvent.png" />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Code for Client Event Handler"
            Font-Bold="True" Font-Size="Large"></asp:Label>
        <br />
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Demo/Img/Event_Demo/ClientEventHandler.png" />
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Before Call Handler Arguments" Font-Bold="True"
            Font-Size="Large"></asp:Label>
        <br />
        Some controls event argummants have extra properties.<br />
        Set e.CancelCall = true, it will cancel to fire server event and client after 
        call event.<br />
        <asp:Image ID="Image3" runat="server" 
            ImageUrl="~/Demo/Img/Event_Demo/ClientEventArg_BeforeCall.png" />
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="After Call Handler Arguments" Font-Bold="True"
            Font-Size="Large"></asp:Label>
        <br />
        <asp:Image ID="Image4" runat="server" 
            ImageUrl="~/Demo/Img/Event_Demo/ClientEventArg_AfterCall.png" />
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Server Event" Font-Bold="True"
            Font-Size="Large"></asp:Label>
        <br />
        <asp:Image ID="Image5" runat="server" 
            ImageUrl="~/Demo/Img/Event_Demo/ServerEvent.png" />
        <br />
        <br />
        <br />
        <asp:Label ID="Label7" runat="server" Text="Demo" Font-Bold="True"
            Font-Size="Large"></asp:Label>
        <br />
        <cc1:LinkButton ID="LinkButton_EventsDemo" runat="server" ClientEvent_AfterCallback="JsFunction_AfterCall"
            ClientEvent_BeforeCallback="JsFunction_BeforeCall" OnClick="LinkButton_EventsDemo_Click">Click Me for Events Demo</cc1:LinkButton>
        <br />
        <br />
        <asp:Label ID="Label_CallResult" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>
