<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBoxList_Test.aspx.cs"
    Inherits="Tester_ZYH.NET_V.Test_V.CheckBoxList_Test" %>

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
    </div>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:Fill_CheckBoxList1('5');">Fill CheckBoxList</asp:HyperLink>
    <br />
    <br />
    <div id="D1">
        <cc1:CheckBoxList ID="CheckBoxList1" runat="server" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged"
            ClientMethod_Fill="Fill_CheckBoxList1" OnFill="CheckBoxList1_Fill" 
            RepeatColumns="5" PostDataContainer="D1">
            <Items>
                <cc1:ListItem Text="AAA" Value="A" />
                <cc1:ListItem Text="BBB" Value="B" />
                <cc1:ListItem Text="CCC" Value="C" />
            </Items>
        </cc1:CheckBoxList>
    </div>
    </form>
</body>
</html>
