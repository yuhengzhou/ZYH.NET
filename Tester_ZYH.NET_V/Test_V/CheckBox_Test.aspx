<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBox_Test.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.CheckBox_Test" %>

<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
    
    </div>
    <cc1:CheckBox ID="CheckBox1" runat="server" Font-Bold="True" Font-Italic="True" 
        Font-Size="Larger" Font-Strikeout="True" ForeColor="#FF9900" 
        oncheckedchanged="CheckBox1_CheckedChanged" Text="Like this?" />
    </form>
</body>
</html>
