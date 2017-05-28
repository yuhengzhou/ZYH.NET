<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="js_test.aspx.cs" Inherits="Tester_ZYH.NET_V.T.js_test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">





        //////////////////////////////////

        console.log(1 + +"2" + "2");

        //////////////////////////////////

        //var arr1 = "john".split('');
        //var arr2 = arr1.reverse();
        //var arr3 = "jones".split('');
        //arr2.push(arr3);
        //console.log("array 1: length=" + arr1.length + " last=" + arr1.slice(-1));
        //console.log("array 2: length=" + arr2.length + " last=" + arr2.slice(-1));











        ///////////////////////////////////////////

        //function sum1() {
        //    var p1 = arguments[0];
        //    if (arguments.length > 1) { return p1 + arguments[1]; }
        //    return function (p2) {
        //        return p1 + p2;
        //    }
        //}

        //var a = sum1(2)(3);
        //var b = sum1(2,3);

        //var isPalindrome = function (st) {
        //    var L = Math.floor(st.length / 2);
        //    for (var i = 0 ; i <= L; i++) {
        //        if (st[i] !== st[st.length - 1 - i]) return false;
        //    }
        //    return true;
        //}
        //function isPalindrome(str) {
        //    str = str.replace(/\W/g, '').toLowerCase();
        //    return (str == str.split('').reverse().join(''));
        //}

        //var b = isPalindrome('abcba1');


        //////////////////////////////////
        console.log(0.1 + 0.2);
        console.log(0.1 + 0.2 == 0.3);



        //////////////////////////////////////////////
        var o = {}; // Creates a new object

        // Example of an object property added
        // with defineProperty with a data property descriptor
        Object.defineProperty(o, 'a', {
            value: 37,
            writable: false,
            enumerable: false,
            configurable: false
        });
        /////////////////////////////////////////
        function f1() {
            var a = arguments[0];

        }

        f1(1, 2, 3);


        ////////////////////////////////////////////
        //function f1() {
        //    this.p1 = 1;
        //}
        //function f2() {
        //    this.p2 = 2;
        //}

        //f2.prototype = new f1();
        //f2.prototype = Object.create(f1);
        //var o = new f2();

        //////////////////////////////
        //function f1() {
        //    this.p1 = 1;
        //}
        //function f2() {
        //    this.p2 = 2;
        //}
        //function f3(pr) {
        //   return this.p3 = this + pr;
        //}

        //var o = f3.call(2, 3);
        //var o1 = f3.bind(2);
        //o1(3);


        ////////////////////////////////////////////////
        //let dog = new function () {
        //    var sd = 'woof';
        //    this.talk = function () {
        //        console.log(sd);
        //    }
        //};
        //dog.talk();
        //let tf = dog.talk;
        //tf();


        /////////////////////////////////////////////////
        let dog = {
            sd: 'woof',
            talk: function () {
                console.log(this.sd);
            }
        };
        dog.talk();
        let tf = dog.talk;
        let bf = tf.bind(dog);
        bf();

        //////////////////////////////////////
        //var pv = 1;
        //var p = new Promise(function (res, rej) {
        //    setTimeout(function () {
        //        pv = 2;
        //    }, 2000);
        //    //rej('no');

        //});

        //p.then(function (fromRe) { console.log(fromRe + pv); });


        //////////////////////////////////////

        //let ar1 = [1, 2, 3];
        //var ar2 = [5, 6];
        //let ar3;
        //(() => {
        //    ar3 = ar1.concat(ar2);

        //})()

        //function xx() {
        //    var l = arguments.length;
        //}

        //xx(1, 2, 3);



        ///////////////////////////////
        //var ar = [];
        //ar.sort

        (function f(f) {
            return typeof f();
        })(function () { return 1; });


        var f = function g() { return 23; };
        //typeof g();

        (function () {
            return typeof arguments;
        })();


        var cc = {};
        with (cc) length;

        //var bbb = aaa;
        var x = 1;
        if (function f() { }) {
            x += typeof f;
        }
        x;
        var f = (
            function f() {
                return "1";
            },
            function g() {
                return 2;
            }
            )();
        typeof f;

        var foo = {
            bar: function () {
                return foo.baz;
            },
            baz: 1
        };
        (function (xx) {
            return typeof arguments[0]();
        })(foo.bar);


        var y = 1, x = y = typeof x;
        x;


        //"use strict"
        (function (x) {
            delete x;
            return x;
        })(1);

        var o = { name: 'zyh', age: 45 };
        delete o.age;
        console.log(o);

        fa();

        (function () {
            alert('started.')
            var a = b = 3;
        })();

        function fa() {
            window.b1 = 5;
            var a1 = 5;
        }

        console.log("a defined? " + (typeof a !== 'undefined'));
        console.log("b defined? " + (typeof b !== 'undefined'));


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
