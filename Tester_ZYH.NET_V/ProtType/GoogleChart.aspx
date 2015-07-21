<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoogleChart.aspx.cs" Inherits="Tester_ZYH.NET_V.T.GoogleChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">

        google.load('visualization', '1.1', { 'packages': ['corechart', 'bar'] });
        google.setOnLoadCallback(drawChart1);



        function drawChart1() {
            function selectHandler() {
                var selectedItem = chart1.getSelection()[0];
                if (selectedItem) {
                    var topping = data1.getValue(selectedItem.row, 0);
                    alert('The user selected ' + topping);
                }
            }
            var data1 = new google.visualization.DataTable();
            data1.addColumn('string', 'frequency');
            data1.addColumn('number', 'total');
            data1.addRows(d1);
            var options = { 'title': 'Schedule Frequency',
                'width': 600,
                'height': 300
            };
            var chart1 = new google.visualization.PieChart(document.getElementById('D1'));
            google.visualization.events.addListener(chart1, 'select', selectHandler);

            chart1.draw(data1, options);



            var data = new google.visualization.arrayToDataTable(d2);
            var options = {
                width: 900,
                chart: {
                    title: 'Reports Generated per Month',
                    subtitle: ''
                },
                series: {
                    0: { axis: 'distance'} // Bind series 0 to an axis named 'distance'.
                },
                axes: {
                    y: {
                        distance: { label: 'number of total' }
                    }
                }
            };
            var chart = new google.charts.Bar(document.getElementById('D2'));
            chart.draw(data, options);


            var zipListingData = google.visualization.arrayToDataTable(d3);
            var chart = new google.visualization.ColumnChart(
            document.getElementById('D3'));
            chart.draw(zipListingData, { 'isStacked': true, 'legend': 'bottom',
                'vAxis': { 'title': 'Number of Listings' }
            });


            var data = new google.visualization.DataTable();
            data.addColumn('string', 'role');
            data.addColumn('number', 'total');
            data.addRows(d4);
            var options = { 'title': 'buyer vs seller',
                'width': 600,
                'height': 300
            };
            var chart = new google.visualization.PieChart(document.getElementById('D4'));
            chart.draw(data, options);

            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Property type');
            data.addColumn('number', 'total');
            data.addRows(d5);
            var options = { 'title': 'Number of property types',
                'width': 600,
                'height': 300
            };
            var chart = new google.visualization.PieChart(document.getElementById('D5'));
            chart.draw(data, options);


            var data = new google.visualization.arrayToDataTable(d6);
            var options = {
                chart: {
                    title: 'Top Zipcodes for Listing Alert',
                    subtitle: ''
                },
                series: {
                    0: { axis: 'distance'} // Bind series 0 to an axis named 'distance'.
                },
                axes: {
                    y: {
                        distance: { label: 'number of total' }
                    }
                }
            };
            var chart = new google.charts.Bar(document.getElementById('D6'));
            chart.draw(data, options);


            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Property type');
            data.addColumn('number', 'total');
            data.addRows(d7);
            var options = { 'title': 'Listing Alert Event Type',
                'width': 600,
                'height': 300
            };
            var chart = new google.visualization.PieChart(document.getElementById('D7'));
            chart.draw(data, options);
        };

        var d1 = [['stoped', 3], ['1 week', 1], ['2', 1], ['4', 1], ['6', 2], ['8', 1], ['13', 2], ['26', 2]];
        var d2 = [['Year-Month', 'total'], ['2014-1', 40], ['2014-2', 41], ['2014-3', 74], ['2014-4', 92], ['2014-5', 69], ['2014-6', 22], ['2014-7', 80]];
        var d3 = [['Zip', 'A', 'E', 'S', 'P', { role: 'annotation'}], ['98112', 10, 24, 20, 20, ''], ['98113', 16, 22, 23, 10, ''], ['98114', 28, 19, 29, 20, '']];
        var d4 = [["Buy-New-Home", 694], ["Sell-Home", 161]];
        var d5 = [["Single-family", 788], ["Condo", 29], ["Town-Home", 28], ["Condo/Town-Home", 9], ["Multifamily", 1]];
        var d6 = [['zip', 'total'], ['92009', 22], ['94507', 4]];
        var d7 = [["for sale", 22], ["recently sold", 15], ["new listing", 6]];
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="javascript:drawChart1();">draw chart1</a> <a href="javascript:drawChart2();">
            draw chart2</a>
        <div id="D1">
        </div>
        <div id="D2">
        </div>
        <div id="D3">
        </div>
        <div id="D4">
        </div>
        <div id="D5">
        </div>
        <div id="D6">
        </div>
        <div id="D7">
        </div>
    </div>
    </form>
</body>
</html>
