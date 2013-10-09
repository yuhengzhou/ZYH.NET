<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoryTab1.ascx.cs" Inherits="Tester_ZYH.NET_V.Test_V.ASCX.HistoryTab1" %>
<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>
<asp:Label ID="Label1" runat="server" Text="this is tab 1"></asp:Label>
<br />
<br />
------callback controls-------<br />
<cc1:DropDownList ID="DropDownList1" runat="server" 
    onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
    UpdateViewState="True">
    <Items>
        <cc1:ListItem Text="1111" Value="1" />
        <cc1:ListItem Text="22222" Value="2" />
        <cc1:ListItem Text="33333" Value="3" />
        <cc1:ListItem Text="44444" Value="4" />
        <cc1:ListItem Text="55555" Value="5" />
    </Items>
</cc1:DropDownList>
<br />
<br />
<cc1:CheckBox ID="CheckBox1" runat="server" 
    oncheckedchanged="CheckBox1_CheckedChanged" />
<br />
<br />
<cc1:TextBox ID="TextBox1" runat="server" onkeyup="TextBox1_KeyUp" />
<br />
<cc1:RadioButtonList ID="RadioButtonList1" runat="server" 
    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
    <Items>
        <cc1:ListItem Text="aaa" Value="a" />
        <cc1:ListItem Text="bbb" Value="b" />
        <cc1:ListItem Text="ccc" Value="c" />
    </Items>
</cc1:RadioButtonList>
<br />
<br />
------postback controls--------<br />
<br />
<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
    <asp:ListItem>Postback Test 1</asp:ListItem>
    <asp:ListItem>Postback Test 2</asp:ListItem>
    <asp:ListItem>Postback Test 3</asp:ListItem>
</asp:DropDownList>
<br />
<br />
<asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Postback test</asp:LinkButton>

