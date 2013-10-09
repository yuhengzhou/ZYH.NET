<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RadioButtonList_Test.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Test_V.RadioButtonList_Test" %>

<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
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
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" 
            NavigateUrl="javascript:Fill_RadioButtonList1('x');">HyperLink</asp:HyperLink>
        <br />
        <br />
        <cc1:RadioButtonList ID="RadioButtonList1" runat="server" 
            ClientMethod_Fill="Fill_RadioButtonList1" onfill="RadioButtonList1_Fill" 
            onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
            RepeatColumns="6">
            <Items>
                <cc1:ListItem Text="111" Value="1" />
                <cc1:ListItem Text="222" Value="2" />
                <cc1:ListItem Text="333" Value="3" />
            </Items>
        </cc1:RadioButtonList>
    </div>
    </form>
</body>
</html>
