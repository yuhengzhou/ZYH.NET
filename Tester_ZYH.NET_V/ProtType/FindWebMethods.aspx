<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindWebMethods.aspx.cs"
    Inherits="Tester_ZYH.NET_V.ProtType.FindWebMethods" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery-1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Call_A() {
            var s = "Male";
            var p = { "Name": "xxx", "Sex": s, "Salary": 10000 };
            $.ajax({
                type: "POST",
                url: "FindWebMethods.aspx/A",
                data: '{"person":' + JSON.stringify(p) + ',"Job":"Programming V"}',
                //data: p,
                //data: JSON.stringify(DTO),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                error: OnError
            });
        }

        function Call_B() {
            var p = '{ "Name": "xxx", "Age": 18}';
            $.ajax({
                type: "POST",
                url: "FindWebMethods.aspx/B",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                error: OnError
            });
        }

        function Call_C() {
            var p = '{ "Name": "xxx", "Age": 18}';
            $.ajax({
                type: "POST",
                url: "FindWebMethods.aspx/C",
                data: "{'Name':'x3'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                error: OnError
            });
        }

        function OnSuccess(msg) {
            var Person = msg.d;
        }

        function OnError(e, a, b) {
        }

        function MS_Call_A() {
            var p = { "Name": "xxx", "Age": 18 };
            var person = new Tester_ZYH.NET_V.ProtType.Person();
            person.Name = "x";
            person.Age = 19;
            PageMethods.A(person, OnSuccess, OnError);
        }

        function Add1000Salary() {
            //如果Person不在App_Code中, 则使用方式如:var person = new 命名空间.Person();          
            var person = new Person(); //or var person = new Object();

            person.Name = "Rose Zhao";
            person.Sex = "Female";
            person.Salary = 2000;
            PageMethods.Add_1000_Salary(person, Add_1000_SalarySucceeded);
        }
        function Add_1000_SalarySucceeded(result) {
            var message = String.format("姓名: {0}; 性别: {1}; 工资: {2}", result.Name, result.Sex, result.Salary);
            alert(message);
        }

        function Person() {
            this.Name = "";
            this.Sex = "";
            this.Salary = 0;

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:Call_A();">Call A</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="javascript:Call_B();">Call B</asp:HyperLink>
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="javascript:Call_C();">Call C</asp:HyperLink>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <p>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="javascript:MS_Call_A();">MS Call A</asp:HyperLink>
    </p>
    <input type="button" value="客户端对象传递到服务端" onclick="Add1000Salary()" />
    </form>
</body>
</html>
