<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PesanMakan.Views.Dashboard" %>
<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head" runat="server">
    <style>
        .header {
            width: 100%;
            padding: 110px 0px;
            background-color : lightcoral;
        }
        .box {
            padding: 37px 30px;
            margin : 10px 20px;
            width: 300px;
            height: 150px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }
        .col-centered {
            display:inline-block;
            float:none;
            /* reset the text-align */
            text-align:left;
            /* inline-block space fix */
            margin-right:-4px;
            text-align: center;
            
        }

        h1 {
            text-align:center;
            margin-bottom: 50px;
            font-weight:bold;
        }



        .status-info {
            color: white;
        }

        div > h1 {
            color: white;
            display: inline;
        }

        div > p {
            color: white;
        }

        @media (max-width: 480px){
            .box {
                position:relative;
                padding: 10px 30px;
                margin : 10px 20px;
                width: 90%;
                height: 100px;
            }

            #icons {
                font-size: 30px;
                display:none;
            }

        }

        


    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="header">
        <h1>SELAMAT DATANG ADMIN</h1>
    </div>  
    <div class="content_db">
        <div class="row">
            <div class="col-xs-12 col-sm-4">
                <div class="col-sm-12 col-centered box bg-info">
                    <div class="title">
                        <div class="row">
                            <div class="col-sm-5">
                                <span class="glyphicon glyphicon-user" class="icons" aria-hidden="true" style="font-size: 75px; color:white; float:left;"></span>
                            </div>
                            <div class="col-sm-7">
                                <h1 id="total_member"></h1>
                                <p>Total Member</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-4">
                <div class="col-sm-12 col-centered box bg-danger">
                    <div class="title">
                        <div class="row">
                            <div class="col-sm-5">
                                <span class="glyphicon glyphicon-shopping-cart" class="icons" aria-hidden="true" style="font-size: 75px; color:white; float:left;"></span>
                            </div>
                            <div class="col-sm-7">
                                <h1 id="total_booking"></h1>
                                <p>Total Booking</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-xs-12 col-sm-4">
                <div class="col-sm-12 col-centered box bg-success">
                    <div class="title">
                        <div class="row">
                            <div class="col-sm-5">
                                <span class="glyphicon glyphicon-thumbs-up" class="icons" aria-hidden="true" style="font-size: 75px; color:white; float:left;"></span>
                            </div>
                            <div class="col-sm-7">
                                <h1 id="total_konfirmasi"></h1>
                                <p>Total Konfirmasi</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
    </div>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Javascript" runat="server">
    <script>
        $(document).ready(function () {
                totalmember();
                totalbooking();
                totalkonfirmasi();
        });

        function totalmember() {
            $.ajax({
                url: "../Handler/GetSetService.asmx/TotalMember",
                type: "POST",
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    document.getElementById("total_member").value = "";
                    var id = data;
                    var idsplit = id.split('{');
                    var idfix = idsplit[0];
                    $("#total_member").text(idfix);
                },

            });
        }

        function totalbooking() {
            $.ajax({
                url: "../Handler/GetSetService.asmx/TotalBooking",
                type: "POST",
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    document.getElementById("total_booking").value = "";
                    var id = data;
                    var idsplit = id.split('{');
                    var idfix = idsplit[0];
                    $("#total_booking").text(idfix);
                },

            });
        }

        function totalkonfirmasi() {
            $.ajax({
                url: "../Handler/GetSetService.asmx/TotalKonfirmasi",
                type: "POST",
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    document.getElementById("total_konfirmasi").value = "";
                    var id = data;
                    var idsplit = id.split('{');
                    var idfix = idsplit[0];
                    $("#total_konfirmasi").text(idfix);
                },

            });
        }



</script>
</asp:Content>