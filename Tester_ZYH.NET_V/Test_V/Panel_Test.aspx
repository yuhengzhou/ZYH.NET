<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Panel_Test.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.Panel_Test" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:CallbackManager ID="CallbackManager1" runat="server">
    </cc1:CallbackManager>
    <asp:Panel ID="Panel1" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </asp:Panel>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    <cc1:Panel ID="Panel2" runat="server" Font-Italic="True" >
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></cc1:Panel>
    <br />
    <div>
    </div>
    <br />
    <cc1:Panel ID="Panel3" runat="server" Width="360px" Height="213px" 
        BorderWidth="5px" BorderColor="#FF0066" BorderStyle="Dotted" 
        ScrollBars="Auto" ClientMethod_LoadFromServer="Load_P3" 
        onloadfromserver="Panel3_LoadFromServer">
    </cc1:Panel>
    <br />
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="javascript:Load_P3('1');">Load tab 1</asp:HyperLink>
&nbsp;
    <asp:HyperLink ID="HyperLink2" runat="server"  NavigateUrl="javascript:Load_P3('2');">Load tab 2</asp:HyperLink>
&nbsp;
    <asp:HyperLink ID="HyperLink3" runat="server"  NavigateUrl="javascript:Load_P3('3');">Load tab 3</asp:HyperLink>
    <br />
    <cc1:Panel ID="Panel5" runat="server">
    </cc1:Panel>
    </form>
</body>
</html>
