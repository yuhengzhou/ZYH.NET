<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DirectCallPageBehind.aspx.cs"
    Inherits="Tester_ZYH.NET_V.DirectCallPageBehind" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <input type="button" value="调用服务端方法" onclick="ExecuteServerMethod('xxx')" />
        <script language="javascript" type="text/javascript">
            function ExecuteServerMethod(value) {
                PageMethods.ReturnStringServerMethod(value, CallBackResult);
            }

            function CallBackResult(result) {
                alert(result);
            }

        </script>
        <input type="button" value="客户端对象传递到服务端" onclick="Add1000Salary()" />
        <input type="button" value="服务端对象传递到客户端" onclick="GetServerObject()" />
        <script language="javascript" type="text/javascript">
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

            function GetServerObject() {
                PageMethods.GetOnePerson(GetServerObjectSucceeded);
            }
            function GetServerObjectSucceeded(result) {
                for (var key in result) {
                    var message = String.format(
                       "姓名: {0}; 性别: {1}; 工资: {2}", result[key].Name, result[key].Sex, result[key].Salary);
                    alert(message);
                }
            }

            function Person() {
                this.Name = "";
                this.Sex = "";
                this.Salary = 0;

            }
        </script>
    </div>
    </form>
</body>
</html>
