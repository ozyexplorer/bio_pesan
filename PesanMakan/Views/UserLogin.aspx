<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="PesanMakan.Views.UserLogin" %>
<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<!DOCTYPE html> 

<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <%--<link rel="icon" type="image/png" sizes="16x16" href="../Scripts/plugins/images/favicon.png">--%>
    <title>Username Login</title>
    <!-- Bootstrap Core CSS -->
    <link rel="stylesheet" href="../Content/bootstrap.min.css"/>
    <!-- Custom CSS -->
    <!-- color CSS -->
    <!-- toast CSS -->
    <link href="../Scripts/plugins/bower_components/toast-master/css/jquery.toast.css" rel="stylesheet">
    <link href="../Scripts/MaterialDesign-Webfont-master/css/materialdesignicons.css" rel="stylesheet">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style type="text/css">
        body{
            background-image: url(../Images/bg4.png);
            background-repeat: no-repeat;
            background-size: 100% 100%;
            min-height: 87.9vh;
            text-align: center;
        }

        .layer{
            background-color: rgba(0,150,136,0.2);
            height: 100vh;
        }

        .header{
            padding : 40px 20px 40px 20px;
        }

        .card{
            margin : auto;
            width:300px;    
        }

        h5{
                font-weight:bold;
            }

        .input-group-text{
            background-color: rgba(47,54,64,0.9);
            color: #ffffff;
        }

        .card-header{
                font-family:sans-serif;
                background-color:#2f3640;
                color:#ffffff;
            }

        .btn-custom{
            background-color:#2f3640;
            color:#ffffff;
        }

        button:hover {
            border: 1px solid rgba(47,54,64,0.9);
            background-color: rgba(47,54,64,0.9);
            color: #ffffff;
        }

        .container-fluid{
            height:100vh;
        }

        textarea:focus, input:focus, input[type]:focus, .uneditable-input:focus {   
            border-color: rgba(47,54,64,0.9);
            box-shadow: 0 0 0 rgba(47,54,64,0.9) inset, 0 0 8px rgba(47,54,64,0.9);
            outline: 0 none;
        }
    </style>
</head>
<body
    
    <div class="container-fluid layer">
            <div class="header">
                <img src="../Images/logoo.png" alt="Logo" />
            </div>
            <div class="card shadow-lg">
                <div class="card-header" align="center">
                    <h5>Username Login</h5>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <div class="input-group mb-3 input-group-sm">
                            <div class="input-group-prepend">
                                <span class="input-group-text mdi mdi-face mdi-24px"></span>
                            </div>
                            <input type="text" id="txtNIK"  class="form-control" placeholder="Username" data-toggle="popover" data-placement="right" data-content="NIK Terdiri dari 4 karakter" data-trigger="focus">
                        </div>
                        <div class="input-group mb-3 input-group-sm">
                            <div class="input-group-prepend">
                                <span class="input-group-text mdi mdi-lock mdi-24px"></span>
                            </div>
                            <input type="password" id="txtPass"  class="form-control" placeholder="Password" data-toggle="popover" data-placement="right" data-content="Password Terdiri dari 6 karakter" data-trigger="focus">
                        </div>

                        <!-- sementara hidden -->
                        <!--<div class="form-group" hidden="hidden">                                
                            <div class="col-xs-12">                               
                            <input id="txtCardID" class="form-control" type="text" placeholder="ID Name Tag">
                            </div>
                        </div>
                        <div class="input-group mb-3 input-group-sm">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="material-icons">perm_identity</i></span>
                            </div>
                            <input type="text" id="txtName" class="form-control" placeholder="Nama" disabled>
                        </div>
                        <div class="input-group mb-3 input-group-sm">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="material-icons">business_center</i></span>
                            </div>
                            <input type="text" id="txtBagian"  class="form-control" placeholder="Bagian/Divisi" disabled>
                        </div>-->

                    </div>      
                    <center>
                        <button id="btnLogIn"  type="submit" class="btn btn-custom btn-sm" data-toggle="modal" data-target="#myModal">Login</button>
                    </center>
                </div>
            </div>
        </div> 

<!-- jQuery -->
<script src="../Scripts/plugins/bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap Core JavaScript -->
<script src="../Scripts/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- Menu Plugin JavaScript -->
<script src="../Scripts/plugins/bower_components/sidebar-nav/dist/sidebar-nav.min.js"></script>

<!--slimscroll JavaScript -->
<script src="../Scripts/js/jquery.slimscroll.js"></script>
<!--Wave Effects -->
<script src="../Scripts/js/waves.js"></script>
<!-- Custom Theme JavaScript -->
<script src="../Scripts/plugins/bower_components/jquery-sparkline/jquery.sparkline.min.js"></script>
<script src="../Scripts/js/custom.min.js"></script>
<!--Style Switcher -->
<script src="../Scripts/plugins/bower_components/styleswitcher/jQuery.style.switcher.js"></script>
<script src="../Scripts/plugins/bower_components/toast-master/js/jquery.toast.js" ></script>





<script>
    $(document).ready(function () {
        $('[data-toggle="popover"]').popover();
        Init();


        $("#btnLogIn").click(function () {
            if (!$("#txtNIK").val() || !$("#txtPass").val()) {
                $("#txtNIK").parents('.form-group').addClass('has-error');
                $("#txtPass").parents('.form-group').addClass('has-error');
                $.toast({ icon: 'error', heading: 'Notification!', text: 'Registrasi gagal! Isi Username atau Password terlebih dahulu!', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
            }
                //else if (!$("#txtCardID").val()) { $("#txtCardID").parents('.form-group').addClass('has-error'); $.toast({ icon: 'error', heading: 'Notification!', text: 'Registrasi gagal! Name Tag ID harus terisi!', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 }); }
            else {
                //success condition
                username = $("#txtNIK").val();
                pass = $("#txtPass").val();
                //nama = $("#txtName").val();
                //card = $("#txtCardID").val();

                $.ajax({
                    type: 'POST',
                    async: false,
                    url: '../Handler/GetSetService.asmx/loginWithUserName',
                    //(string nik, string nama, string cardid)
                    data: JSON.stringify({ username: username, pass: pass }),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'text',
                    success: function (data) {
                        data = data.replace('{"d":null}', "");
                        if (data != "") {

                            if (data == "0") {
                                $.toast({ icon: 'error', heading: 'Warning!', text: 'User Name belum diregistrasi, Harap registrasi di bagian SDM', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                            } else if (data == "1") {
                                window.location.href = "Dashboard.aspx";

                            }
                        }

                    },
                    failure: function (d) {
                        $.toast({ icon: 'error', heading: 'Warning!', text: 'Ajax request failed', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });
                    }
                });
            }
        });
    })

    function Init() {
        $("#txtNIK, #txtPass").val('');

    }

</script>

</body>
</html>
