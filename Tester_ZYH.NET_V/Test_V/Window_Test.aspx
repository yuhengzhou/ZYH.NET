<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Window_Test.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.Window_Test" %>

<%@ Register assembly="ZYH.WebControl_V" namespace="ZYH.WebControl_V" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
            <script type="text/javascript">
                function al(id, e) {

                }

                function bl(id, e) {

                }
               
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:CallbackManager ID="CallbackManager1" runat="server">
        </cc1:CallbackManager>
    
    </div>
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="javascript:Window1_Load('x');">HyperLink</asp:HyperLink>
    <br />
    <br />
    <cc1:Window ID="Window1" runat="server" 
        ClientMethod_LoadFromServer="Window1_Load" 
        onloadfromserver="Window1_LoadFromServer" 
        ClientEvent_AfterCallback_LoadFromServer="al" 
        ClientEvent_BeforeCallback_LoadFromServer="bl">
    </cc1:Window>
    </form>
</body>
</html>
