<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ticketing.aspx.cs" Inherits="PesanMakan.Views.Ticketing" %>

<%@ Import Namespace="PesanMakan.Presentation.Component" %>
<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>PT.Bio Farma NFC Reader</title>
    <link rel="icon" href="../../Icon/iconbio-farma.jpg">
    
    
    <link rel="stylesheet" href="../Content/bootstrap.min.css"/>
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <!-- animation CSS -->
    <link href="../Scripts/css/animate.css" rel="stylesheet">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

    <%--<% Response.Write(CSS.GetCoreStyle()); %>--%>
    <style>
         body{
            font-family: "Source Sans Pro", sans-serif;
            background-image: url(../Images/bg4.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            min-height: 87.9vh;
            text-align: center;
        }

         .header{
            padding : 40px 20px 40px 20px;
        }

        h1 {
            font-weight:bold;
            margin: 80px 50px 50px 50px;
            font-family:sans-serif;
            color: #2f3640;
            font-size: 40px;
        }

        h5{
            font-weight:bold;
        }

        .card{
            margin : auto;
            width:300px;    
        }

        .card-header{
            font-family:sans-serif;
            background-color:#2f3640;
            color:#ffffff;
        }

        .btn-custom{
            background-color:#0092aa;
            color:#ffffff;
        }

        button:hover {
            border: 1px solid #00a6c1;
            background-color: #00a6c1;
            color: #ffffff;
        }

        .container-fluid{
            height:100vh;
        }

        .tap-card{
            width:230px;
            height:240px;
            margin-top:30px;
        }

        .login-tap{
            margin : auto;
            width:250px;
            height:300px;
            border:none;
        }

        .zoomOutDown {
            -webkit-animation: zoomOutDown 3s;
            -moz-animation:    zoomOutDown 3s;
            -o-animation:      zoomOutDown 3s;
            animation:         zoomOutDown 3s;
        }

        @media only screen and (max-width: 1360px) {
            body{
                background-image: url(../Images/bg4.png);
                background-repeat: no-repeat;
                background-size: 100% 100%;
                min-height: 130vh;
            }
        }

        /* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            background-color: #fefefe;
            margin: auto;
            padding: 0;
            border: 1px solid #888;
            width: 80%;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.4s
        }

        /* Add Animation */
        @-webkit-keyframes animatetop {
            from {top:-300px; opacity:0} 
            to {top:0; opacity:1}
        }

        @keyframes animatetop {
            from {top:-300px; opacity:0}
            to {top:0; opacity:1}
        }

        /* The Close Button */
        .close {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

        .close:hover,
        .close:focus {
            color: #000;
            text-decoration: none;
            cursor: pointer;
        }

        .modal-header {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }

        .modal-body {padding: 2px 16px;}

        .modal-footer {
            padding: 2px 16px;
            background-color: #5cb85c;
            color: white;
        }
    </style>
       
</head>
<body>
  
    <section id="wrapper" class="error-page">
        <div class="container-fluid">
            <div class="header">
                <h1>Tempelkan Kartu Untuk Print Tiket</h1>
            </div>
            <div class="card shadow-lg login-tap">
                <div class="card-header shadow-sm" align="center">
                    <h5>Tap Card</h5>
                </div>
                <div class="card-body">
                    <img src="../Images/cardd.png" class="tap-card animated zoomOutDown infinite" alt="Tap-Card"/>
                </div>
            </div>
        </div>
        <!--<div class="tap-box">
            <div class="tap-body text-center">
                <h2 style="text-align:center; color: #0094ac">NFC READER PT. BIO FARMA</h2>

                <h2 class="text-uppercase" style="margin-top:80px"><strong>Tap your ID</strong></h2>
                <%--<p class="text-muted m-t-30 m-b-30">YOU SEEM TO BE TRYING TO FIND HIS WAY HOME</p>
                <a href="index.html" class="btn btn-info btn-rounded waves-effect waves-light m-b-40">Back to home</a>--%>
            </div>
            <footer class="footer text-center"> 
                2017-2019 &copy; Layanan Makan <br /> Contact information www.biofarma.co.id
            </footer>
        </div>-->
    </section>

    <!-- The Modal -->
    <div id="myModal" class="modal">
        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <span class="close">&times;</span>
            </div>
            <div style="text-align:center" class="modal-body">
    	        <svg id="barcode" class="barcode">
                </svg>
                <p id="tgl"></p>
            </div>
            <div class="modal-footer">
                <button class="btn btn-default" onclick="myFunction()">Print this page</button>
            </div>
        </div>
    </div>
     
    <!-- The Modal -->
    <div id="myModal2" class="modal">
        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <span class="close">&times;</span>
            </div>
            <div class="modal-body">
    	        <p id="keterangan" style="text-align:center">Tidak Booking</p>
            </div>
            <div class="modal-footer">
            </div>
        </div>
    </div>
    
 </body>
</html>
<!-- jQuery -->
<script src="../Scripts/plugins/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="../Scripts/bootstrap.min.js"></script>
<!-- Custom Theme JavaScript -->
<script src="../Scripts/js/custom.min.js"></script>
<script src='../Scripts/plugins/barcode/JsBarcode.all.min.js'></script>

<%--<% Response.Write(Javascript.GetCoreScript()); %>--%>
<script>
    var modal = document.getElementById('myModal');

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];


    //// When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    $(document).ready(function () {
        var valuebar; //barcode for print
        window.setInterval(function () {
            valuebar = initStart(valuebar);
        }, 3000);
    });



    function initStart(valuebar) {
        var statinit;
        $.ajax({
            url: "../Handler/GetSetService.asmx/startService",
            type: "POST",
            contentType: "application/text; charset=utf-8",
            dataType: "text",

            success: function (data) {
                statinit = data;
                if (statinit == "Tidak Booking") {
                    setTimeout(function () {
                        $('#myModal2').modal('hide')
                    }, 2000);

                    $('#myModal2').modal('show');

                }
                else if (statinit == "Belum Terdaftar") {
                    document.getElementById('keterangan').innerHTML = "Anda Belum Terdaftar Dalam Database Pesan Makan Silahkan Daftar Di Bagian SDM";
                    setTimeout(function () {
                        $('#myModal2').modal('hide')
                    }, 2000);
                    $('#myModal2').modal('show');

                } else if (statinit == "Jam Makan Tutup") {
                    document.getElementById('keterangan').innerHTML = "Jam Makan Tutup, Silahkan Hadir Tepat Waktu";
                    setTimeout(function () {
                        $('#myModal2').modal('hide')
                    }, 2000);
                    $('#myModal2').modal('show');

                } else if (statinit == "Print") {
                    document.getElementById('keterangan').innerHTML = "Tiket Anda Sudah Terprint";
                    setTimeout(function () {
                        $('#myModal2').modal('hide')
                    }, 2000);
                    $('#myModal2').modal('show');

                } else if (statinit == "Tidak Terhubung NFC") {

                } else {
                    valuebar = updateToken(valuebar);

                    setTimeout(function () {
                        $('#myModal').modal('hide');
                        modal.style.display = "none";
                    }, 100);

                    //$('#myModal').modal('show');
                    modal.style.display = "block";
                    window.print();

                    //$("#myModal").modal("hide");
                }

            },

            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
        return valuebar;
    }

    function updateToken(valuebar) {
        $.ajax({
            type: 'GET',
            url: '../Handler/GetSetService.asmx/GetTokenTicketing',
            //data: 'id=' + eID,
            async: false,
            dataType: 'text',
            success: function (d) {

                d = d.replace('{"d":null}', "");
                dt = JSON.parse(d).data;
                if (dt.length > 0) {
                    valuebar = dt;

                    var date = new Date();
                    var day = date.getDate();
                    var h = date.getMonth();
                    var month = h + 1;
                    var yy = date.getFullYear();
                    var hour = date.getHours();
                    var minute = date.getMinutes();
                    var second = date.getSeconds();

                    tgl = day + '-' + month + '-' + yy + ' ' + hour + ':' + minute + ':' + second;

                    JsBarcode("#barcode", valuebar, {
                        format: "CODE128",
                        lineColor: "#000",
                        width: 3,
                        height: 40,
                        displayValue: true
                    });

                    $("#tgl").html(tgl);
                }
            },
            failure: function (d) {
                $.toast({ icon: 'error', heading: 'Warning!', text: 'Ajax request failed', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
            }
        });
        return valuebar;
    }

    function myFunction() {
        window.print();

    }
</script>