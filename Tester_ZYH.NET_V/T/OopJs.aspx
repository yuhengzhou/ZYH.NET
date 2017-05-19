<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OopJs.aspx.cs" Inherits="Tester_ZYH.NET_V.T.OopJs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var cars = function () {
            this.isRunning = false;
            this.wheels = 4;
            this.type = 'auto';
            this.drive = function () {
                isRunning = true;
            }
        };
        var toyota = Object.create(cars);
        toyota.prototype.brand = 'Toyota';


        var t = new cars();
        var b = t.brand;
        b = toyota.brand;

        var ppty = t.hasOwnProperty("wheels");

        for (var p in t) {
            document.write(p + ":" + t[p] + "<br/>");
        }
        console.log(toyota.type);
    </script>
    <script type="text/javascript">
        var Address = {
            Street: 'Non',
            city: 'Su',
            state: 'bc',

            get getAdd() { return this.Street; },

            set setAdd(Add) {
                var ss = Add.toString().split(',');
                this.Street = ss[0];
                this.city = ss[0];
            }
        };


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <a href="javascript:alert(Address.getAdd);">Get Add</a>
        <br />
        <a href="javascript:(function(){Address.setAdd='10661 cherryhill close,surrey';alert('done');})();">Set Add</a>
    </form>
</body>
</html>
