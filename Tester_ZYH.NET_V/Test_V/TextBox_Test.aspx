<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TextBox_Test.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.TextBox_Test" %>

<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 296px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    TextBox KeyUp</td>
                <td>
                    <cc1:TextBox ID="TextBox1" runat="server" onkeyup="TextBox1_KeyUp" Rows="5" 
                        TextMode="MultiLine" Width="216px" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
