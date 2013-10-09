<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListBox_Test.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.ListBox_Test" %>

<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript">
            function a(id, e) {

            }

            function b(id, e) {

            }
            function af(id, e) {

            }

            function bf(id, e) {

            }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
    
    </div>
    <cc1:ListBox ID="ListBox1" runat="server" Height="160px" 
        onselectedindexchanged="ListBox1_SelectedIndexChanged" Width="170px" 
        ClientMethod_Fill="Fill_ListBox1" onfill="ListBox1_Fill" 
        SelectionMode="Multiple" 
        ClientEvent_AfterCallback_SelectedIndexChanged="a" 
        ClientEvent_BeforeCallback_SelectedIndexChanged="b" CommandArg="xXx" 
        ClientEvent_AfterCallback_Fill="af" ClientEvent_BeforeCallback_Fill="bf">
        <Items>
            <cc1:ListItem Text="11111" Value="1" />
            <cc1:ListItem Text="22222" Value="2" />
            <cc1:ListItem Text="33333" Value="3" />
        </Items>
    </cc1:ListBox>
    &nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="javascript:Fill_ListBox1('5');">Fill List Box</asp:HyperLink>
    </form>
</body>
</html>
