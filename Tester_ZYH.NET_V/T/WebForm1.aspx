﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Tester_ZYH.NET_V.T.WebForm1" ValidateRequest="false" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #xxx
        {
            height: 211px;
            width: 429px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    <iframe id="xxx" height="400" src="../Demo/Default.aspx" width="600"></iframe>
    <br />
    <br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="87px" TextMode="MultiLine" 
        Width="265px"></asp:TextBox>
    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">LinkButton</asp:LinkButton>
    </form>
</body>
</html>
