<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Tester_ZYH.NET_V.Test_V.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../ProtType/jquery-1.8.3.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var serviceUrl = "https://tpodev11-8i.corp.homestore.net/dataservice/DocumentStorage.svc";
            var operation = serviceUrl + "/list";

            alert('WCF Client test');

            $.ajax({
                cache: false,
                type: "GET",
                async: false,
                url: operation,
                data: "{\"entityType\":\"4\",\"entityId\":\"a4830252-6c8e-49e2-9fc0-db595c0db2b4\"}",
                contentType: 'application/json charset UTF-8',
                dataType: "json",
                success: function (data) {
                    alert('good');
                },
                error: function (ex) {
                    alert('error');
                    alert(ex);
                }
            });
        });
    </script>
    </form>
</body>
</html>
