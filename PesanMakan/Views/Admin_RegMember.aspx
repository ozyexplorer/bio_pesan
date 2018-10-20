<%@ Page Language="C#" MasterPageFile="PesanMakan.Master" AutoEventWireup="true" CodeBehind="Admin_RegMember.aspx.cs" Inherits="PesanMakan.Views.Admin_RegMember" %>

<%@ Import Namespace="PesanMakan.Presentation.Component" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
<div class="container-fluid">
    <br />
  <!-- start slider -->
  <header id="registration" style="background:#ffffff;">
     <div class="w-container " >
        <div class="w-row row form">

            <div class="form-group" style="margin-bottom:15px;">
                <div class="col-lg-12 col-md-12 col-sm-12">
                    <p style="font-size:30px;color:black;line-height:120%; vertical-align:middle; text-align:center;"  >Form Registrasi Member</p>
                    <hr class="colorgraph">
                </div>
            </div>

            <div class="form-group col-sm-offset-3">
                <div class="row" style="margin-bottom:5px;">
                    <div class="col-sm-2">
                        <label style="font-weight: bold;">NIK</label>
                    </div>
                    <div class="col-sm-6">
                        <input type="text" id="txtNIK" class="form-control m-bot15" >
                    </div>
                </div>

                <div class="row" style="margin-bottom:5px;">
                    <div class="col-sm-2">
                        <label style="font-weight: bold;">Nama</label>
                    </div>
                    <div class="col-sm-6">
                        <input type="text" id="txtNama" class="form-control m-bot15" >
                    </div>
                </div>

                <div class="row" style="margin-bottom:5px;">
                    <div class="col-sm-2">
                        <label style="font-weight: bold;">Card ID</label>
                    </div>
                    <div class="col-sm-6">
                        <input type="text" id="txtCardID" class="form-control m-bot15" >
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-6" style="float:right;">
                        <input type="button" id="btnAdd" onclick="submitData()" class="btn btn btn-primary" value="ADD" />
                    </div>
                </div>
            </div>

            <div runat="server" class="col-sm-12">
                <div class="table-responsive">
                    <table id="tblCard" class="display nowrap" cellspacing="0" width="100%">
                        <thead >
                            <tr>
                                <td>NIK</td>
                                <td>Nama</td>
                                <td>Action</td>
                            </tr>
                        </thead>
                        <tbody>
                           
                        </tbody>
                        
                    </table>
                </div>
            </div>
              
        <div id="result"></div>
     </div>
    </div><!-- end slider -->
  </header>
</div>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Javascript" runat="server">
    <script>

        $(document).ready(function () {
            initTextBox();
            var myTimer = window.setInterval(function () {
                init();
            }, 3000);
            initTable();


        });

        function submitData() {
            var parameters = {};
            parameters.nik = $("#txtNIK").val();
            parameters.nama = $("#txtNama").val();
            parameters.cardID = $("#txtCardID").val();

            $.ajax({

                data: JSON.stringify(parameters),
                dataType: "text",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "../Handler/GetSetService.asmx/RegMember",
                success: function () {
                    initTextBox();
                    $.toast({ icon: 'success', heading: 'Notifikasi!', text: 'Data Berhasil Dimasukan', showHideTransition: 'slide', position: 'bottom-right', hideAfter: 3000 });

                }
            });
            initTable();

        }

        function Edit(r) {

            $('#tblCard tbody').on('click', '.btn-info', function () {
                var data_row = $(this).closest('tr');
                console.log(data_row[0].children[0].textContent);
                $("#txtNIK").val(data_row[0].children[0].textContent);
                $("#txtNama").val(data_row[0].children[1].textContent);

            });

        }

        function initTable() {
            $("#tblCard" + " tbody").empty();
            $("#tblCard" + " tbody").append('<tr><td colspan="12" class="text-center"><i class="fa fa-spin fa-refresh"></i>&nbsp;Loading...</td></tr>');

            var table = $('#tblCard').dataTable({
                "bDestroy": true,
                "dom": '<"html5buttons"B>lTfgitp',
                "buttons": [
                    { extend: 'csv', title: 'Member' },
                    { extend: 'excel', title: 'Member' },
                ],
                "ajax": {
                    "url": "../Handler/GetSetService.asmx/GetNikNamaAct",
                    "type": "POST"
                },
                "columns": [
                    { "data": "NIK" },
                    { "data": "NAMA" },
                    { "data": "ACTION" }
                ],
                "order": [[1, 'desc']]
            });
        }

        function initTextBox() {
            // Clear forms here
            document.getElementById("txtNIK").value = "";
            document.getElementById("txtNama").value = "";
            document.getElementById("txtCardID").value = ""

        }

        function init() {

            $.ajax({
                url: "../Handler/GetSetService.asmx/initCardUID",
                type: "POST",
                contentType: "application/text; charset=utf-8",
                dataType: "text",
                success: function (data) {
                    document.getElementById("txtCardID").value = "";
                    var id = data;
                    var idsplit = id.split('{');
                    var idfix = idsplit[0];
                    $("#txtCardID").val(idfix);
                },

            });

        }

        function back_page() {

            var page = 'index.aspx';
            document.location.href = page;
        }
</script>
</asp:Content>






