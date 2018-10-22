<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="PesanMakan.Views.index" %>
<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Halaman Utama</title>
    
    <link rel="stylesheet" href="../Content/bootstrap.min.css"/>
    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
    <link href="../Scripts/css/animate.css" rel="stylesheet">

    <style type="text/css">
        body {
          padding: 0px;
          margin: 0;
          font-family: "Source Sans Pro", sans-serif;
          -webkit-font-smoothing: antialiased;
          background-image: url(../Images/bg4.png);
          background-repeat: no-repeat;
          background-size: 100% 100%;
          min-height: 87.9vh;
        }

        .container-fluid {
            text-align: center;
        }
        
        h1 {
            font-weight:bold;
            margin: 80px 50px 50px 50px;
            font-family:sans-serif;
            color: #2f3640;
            font-size: 60px;
        }
            
        h5{
            font-family:sans-serif;
            font-weight:bold;
        }

        h6{
            color:#c6c6c6;
        }

        p{
            font-family:sans-serif;
            color: #2f3640;
        }

        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            padding-top: 10px;
            background-color: rgba(47,54,64,0.5);
            color: #f1f1f1;
            text-align: center;
        }

        @import "https://fonts.googleapis.com/css?family=Source+Sans+Pro:700";
        *,
        *::before,
        *::after {
          -webkit-box-sizing: border-box;
          -moz-box-sizing: border-box;
          box-sizing: border-box;
        }

        a.bttn {
          color: #2f3640;
          text-decoration: none;
          -webkit-transition: 0.3s all ease;
          transition: 0.3s ease all;
          margin: 10px 10px 10px 20px;
        }
        a.bttn:hover {
          color: #FFF;
        }
        a.bttn:focus {
          color: #FFF;
        }

        .bttn {
          font-size: 18px;
          letter-spacing: 2px;
          text-transform: uppercase;
          display: inline-block;
          text-align: center;
          width: 270px;
          font-weight: bold;
          padding: 14px 0px;
          border: 3px solid #2f3640;
          border-radius: 2px;
          position: relative;
          box-shadow: 0 2px 10px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.1);
        }
        .bttn:before {
          -webkit-transition: 0.5s all ease;
          transition: 0.5s all ease;
          position: absolute;
          top: 0;
          left: 50%;
          right: 50%;
          bottom: 0;
          opacity: 0;
          content: '';
          background-color: #2f3640;
          z-index: -2;
        }

        .bttn:hover:before {
          -webkit-transition: 0.5s all ease;
          transition: 0.5s all ease;
          left: 0;
          right: 0;
          opacity: 1;
        }

        .bttn:focus:before {
          transition: 0.5s all ease;
          left: 0;
          right: 0;
          opacity: 1;
        }

        @media only screen and (max-width: 800px) {

            h1 {
            font-weight:bold;
            margin: 80px 50px 20px 50px;
            font-family:sans-serif;
            color: #2f3640;
            font-size: 50px;
            }

            .bttn{
                width : 150px;
                font-size: 14px;
            }

            body{
                background-image: url(../Images/bg4.png);
                background-repeat: no-repeat;
                background-size: 100% 100%;
                min-height: 110vh;
            }
        }
        @media only screen and (max-width: 1360px) {
            body{
                background-image: url(../Images/bg4.png);
                background-repeat: no-repeat;
                background-size: 100% 100%;
                min-height: 110vh;
            }
        }

        .fadeInDown {
          -webkit-animation: fadeInDown 2s;
          -moz-animation:    fadeInDown 2s;
          -o-animation:      fadeInDown 2s;
          animation:         fadeInDown 2s;
        }

        .fadeInUp {
          -webkit-animation: fadeInUp 2s;
          -moz-animation:    fadeInUp 2s;
          -o-animation:      fadeInUp 2s;
          animation:         fadeInUp 2s;
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <div class="header">
            <h1 class="animated fadeInDown">SELAMAT DATANG <br /> DI APLIKASI PESAN MAKAN</h1><br />
            <p class="animated fadeInDown"><b>Silahkan pilih jenis login yang diinginkan.</b></p>
            <a href="Login.aspx" class="bttn animated fadeInUp">NFC Card</a>
            <a href="UserLogin.aspx" class="bttn animated fadeInUp">Username</a>
        </div>
    </div>
    <div class="footer">
        <h6>&copy; Copyright 2018 By <b>Bio Farma</b></h6>
    </div>
</body>


<% Response.Write(Javascript.GetCoreScript()); %>
<% Response.Write(Javascript.GetCustomFormScript()); %>
<% Response.Write(Javascript.GetDynamicTableScript()); %>
<% Response.Write(Javascript.GetInitialisationScript()); %>
<% Response.Write(Javascript.GetCharacterValidationInitScript()); %>
</html>
