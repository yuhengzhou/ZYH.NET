<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadResource.aspx.cs" Inherits="Tester_ZYH.NET_V.ProtType.LoadResource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery-1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        function LoadResource() {
            $.getScript('JsResource.js', function () { alert('success!'); });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:LoadResource();">Load</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="javascript:xxx();">test</asp:HyperLink>
    </div>
    </form>
</body>
</html>
