<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tab1.ascx.cs" Inherits="Tester_ZYH.NET_V.Demo.ASCX.Tab1" %>
<%@ Register Assembly="ZYH.WebControl_V" Namespace="ZYH.WebControl_V" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        width: 117px;
    }
</style>
&nbsp;
<table style="width: 100%;">
    <tr>
        <td class="style1">
            <asp:Label ID="Label1" runat="server" Text="Search:"></asp:Label>
        </td>
        <td>
            <div id="div_TextBox_Search">
                <cc1:TextBox ID="TextBox_Search" runat="server" onkeyup="TextBox_Search_KeyUp" 
                    PostDataContainer="div_TextBox_Search" />
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div id="div_GV" runat="server">
                <asp:GridView ID="GridView_ListPerson" runat="server" EmptyDataText="No Data" OnRowDataBound="GridView_ListPerson_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink_Edit" runat="server">Edit</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:Load_EditPerson();">Add</asp:HyperLink>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<cc1:Window ID="Window_EditPerson" runat="server" ClientMethod_LoadFromServer="Load_EditPerson"
    OnLoadFromServer="Window_EditPerson_LoadFromServer">
</cc1:Window>
