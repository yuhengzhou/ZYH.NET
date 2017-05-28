 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Js_Inherit.aspx.cs" Inherits="Tester_ZYH.NET_V.T.Js_Inherit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Job() {
            this.pays = true;
        }

        Object.prototype.print = function () {
            console.log(this.pays);
        }

        function TechJob(title) {
            Job.call(this)
            this.title = title;
        }

        TechJob.prototype = Object.create(Job.prototype);
        TechJob.prototype.constructor = TechJob;

        var x = new TechJob("pg");
        x.print();




        ////////////////////////////////////////////
        var human = {
            name: 'zyh',
            age: 45,
            sayAge: function () {
                alert(this.age);
            }
        };
        var ks = Object.keys(human);

        function people() {
            this.Name = 'zyh1';
            this.age = 45.5;
            this.sayName = function () {
                alert(this.Name);
            }
        }

        function worker() {
            this.sallary = 4500;
            this.saySallary = function () {
                alert(this.sallary);
            }
        }
        var p = new people();
        var ks = Object.keys(p);
        var dc = Object.keys(document);

        var h = Object.create(human);
        h.age = 5;
        h.sayAge();
        human.sayAge();


        people.prototype = new worker();
        var p1 = new people();
        p1.age = 5;
        p1.sallary = 2000;
        p1.saySallary();
        
        var ks = Object.keys(h);


        var people1 = Object.create(people);
        var ks = Object.keys(people);



    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
