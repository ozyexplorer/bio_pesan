<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PesanMakan.Views.Login" %>
<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NFC Card Login</title>

    <link rel="icon" href="../../Icon/iconbio-farma.jpg" />
    <% Response.Write(CSS.GetCoreStyle()); %>
    <link href="../Scripts/css/animate.css" rel="stylesheet">
    <link rel="stylesheet" href="../Content/bootstrap.min.css"/>
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>

    <style type="text/css">
        body{
            background-image: url(../Images/bg4.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            min-height: 87.9vh;
            text-align: center;
        }

        .header{
            padding : 40px 20px 40px 20px;
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
    </style>
</head>
<body>
   <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="header">
                <img src="../Images/logoo.png" alt="Logo" />
            </div>
            <div class="card shadow-lg login-tap">
                <div class="card-header shadow-sm" align="center">
                    <h5>NFC Card Login</h5>
                </div>
                <div class="card-body">
                    <img src="../Images/cardd.png" class="tap-card animated zoomOutDown infinite" alt="Tap-Card"/>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script>
    $(document).ready(function () {
        window.setInterval(function () {
            init();
        }, 3000);
    });
    function init() {

        $.ajax({
            url: "../Handler/GetSetService.asmx/loginWithCardUID",
            type: "POST",
            contentType: "application/text; charset=utf-8",
            dataType: "text",
            success: function (data) {
                //document.getElementById("txtCardID").value = "";
                var value = data;
                if (value != "") {
                    //var valuesplit = value.split(' ');
                    //var statfix = valuesplit[0];
                    //var idfix = valuesplit[1];
                    //var namafix = valuesplit[2];
                    //var rolefix = valuesplit[3];

                    if (value == "0") {
                        alert("Kartu Belum diregistrasi, Harap Registrasi di bagian SDM");
                    } else if (value == "1") {
                        window.location.href = "Dashboard.aspx";

                    }
                }

            },

        });

    }
    function next_page() {
        var page = 'Dashboard.aspx';
        document.location.href = page;
        sessionStorage.clear();
    }
</script>