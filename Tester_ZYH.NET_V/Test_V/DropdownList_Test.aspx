<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DropdownList_Test.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.DropdownList_Test" %>

<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function fa(id, e) { 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
    
    </div>
    <cc1:DropDownList ID="DropDownList1" runat="server" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
        ClientEvent_AfterCallback_Fill="fa" UpdateControlState="True" 
        UpdateViewState="True" Width="200px" EnableViewState="False">
        <Items>
            <cc1:ListItem Text="AAA" Value="A" />
            <cc1:ListItem Text="BBB" Value="B" />
            <cc1:ListItem Text="CCC" Value="C" />
        </Items>
    </cc1:DropDownList>
    <br />
    <br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <br />
    <br />
    <cc1:DropDownList ID="DropDownList2" runat="server" 
        ClientMethod_Fill="Fill_DropdownList2" onfill="DropDownList2_Fill">
    </cc1:DropDownList>
&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="javascript:Fill_DropdownList2('x');">Fill DropdownList2</asp:HyperLink>
    <br />
    <br />
    </form>
</body>
</html>
