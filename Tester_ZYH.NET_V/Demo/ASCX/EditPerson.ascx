<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditPerson.ascx.cs" Inherits="Tester_ZYH.NET_V.Demo.ASCX.EditPerson" %>
<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<style type="text/css">
    .style1
    {
    }
</style>

<table style="width:100%;" id="table_EditPoerson">
    <tr>
        <td class="style1">
            <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label2" runat="server" Text="Sex"></asp:Label>
        </td>
        <td>
            <cc1:RadioButtonList ID="RadioButtonList_Sex" runat="server" 
                RepeatDirection="Horizontal">
                <Items>
                    <cc1:ListItem Selected="True" Text="Male" Value="1" />
                    <cc1:ListItem Text="Female" Value="2" />
                </Items>
            </cc1:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label3" runat="server" Text="Age"></asp:Label>
        </td>
        <td>
            <cc1:TextBox ID="TextBox_Age" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label5" runat="server" Text="Birthday"></asp:Label>
        </td>
        <td>
            <cc1:Calendar ID="Calendar_Birthday" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Label ID="Label4" runat="server" Text="Comment"></asp:Label>
        </td>
        <td>
            <cc1:TextBox ID="TextBox_Comment" runat="server" Height="110px" 
                TextMode="MultiLine" Width="360px" />
        </td>
    </tr>
    <tr>
        <td class="style1" colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label_ErrorMessage" runat="server" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <cc1:LinkButton ID="LinkButton_Save" runat="server" 
                onclick="LinkButton_Save_Click">Save</cc1:LinkButton>
        </td>
        <td>
            <asp:HyperLink ID="HyperLink_Cancel" runat="server">Cancel</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>

